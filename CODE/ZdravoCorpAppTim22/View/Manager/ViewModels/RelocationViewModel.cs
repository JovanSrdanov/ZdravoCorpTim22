using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.DataModel;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class RelocationViewModel
    {
        public ObservableCollection<Room> RoomList { get; set; }
        public ObservableCollection<EquipmentDataModel> EquipmentList { get; set; }
        public RelocationViewModel(List<Equipment> eq)
        {
            List<EquipmentDataModel> temp = new List<EquipmentDataModel>();
            foreach (Equipment eqItem in eq)
            {
                temp.Add(new EquipmentDataModel(eqItem.Amount, eqItem));
            }
            EquipmentList = new ObservableCollection<EquipmentDataModel>(temp);
            RoomList = new RoomViewModel().RoomList;
        }
        public RelocationViewModel(List<Equipment> eq, Room room)
        {
            List<EquipmentDataModel> temp = new List<EquipmentDataModel>();
            foreach (Equipment eqItem in eq)
            {
                temp.Add(new EquipmentDataModel(eqItem.Amount, eqItem));
            }
            EquipmentList = new ObservableCollection<EquipmentDataModel>(temp);
            RoomList = new RoomViewModel(room).RoomList;
        }

        public void WarehouseToRoom(Room destination, Interval interval)
        {
            if (!destination.IsAvailable(interval))
            {
                return;
            }
            List<Equipment> equipment = new List<Equipment>();
            foreach (EquipmentDataModel eq in EquipmentList)
            {
                Equipment temp = new Equipment(eq.Equipment)
                {
                    Amount = eq.Amount
                };
                eq.Equipment.Amount -= temp.Amount;
                EquipmentController.Instance.Update(eq.Equipment);

                if (interval.End.Date == DateTime.Now.Date || temp.EquipmentData.Type == EquipmentType.consumable)
                {
                    temp.room = destination;
                    EquipmentController.Instance.AddRoomEquipment(temp);
                }
                else
                {
                    equipment.Add(temp);
                }
            }
            CreateRelocation(null, destination, interval, equipment);
        }
        public void RoomToWarehouse(Room source, Interval interval)
        {
            if (!source.IsAvailable(interval))
            {
                return;
            }
            List<Equipment> equipment = new List<Equipment>();
            foreach (EquipmentDataModel eq in EquipmentList)
            {
                Equipment temp = new Equipment(eq.Equipment)
                {
                    Amount = eq.Amount
                };
                eq.Equipment.Amount -= eq.Amount;
                if(eq.Equipment.Amount > 0)
                {
                    EquipmentController.Instance.Update(eq.Equipment);
                }
                else
                {
                    EquipmentController.Instance.DeleteByID(eq.Equipment.Id);
                }
                if (interval.End.Date == DateTime.Now.Date || temp.EquipmentData.Type == EquipmentType.consumable)
                {
                    temp.room = null;
                    EquipmentController.Instance.AddWarehouseEquipment(temp);
                }
                else
                {
                    equipment.Add(temp);
                }
            }
            CreateRelocation(source, null, interval, equipment);
        }
        public void RoomToRoom(Room source, Room destination, Interval interval)
        {
            if(!source.IsAvailable(interval) || !destination.IsAvailable(interval))
            {
                return;
            }
            List<Equipment> equipment = new List<Equipment>();
            foreach (EquipmentDataModel eq in EquipmentList)
            {
                Equipment temp = new Equipment(eq.Equipment)
                {
                    Amount = eq.Amount
                };
                eq.Equipment.Amount -= temp.Amount;
                if (eq.Equipment.Amount > 0)
                {
                    EquipmentController.Instance.Update(eq.Equipment);
                }
                else
                {
                    EquipmentController.Instance.DeleteByID(eq.Equipment.Id);
                }

                if (interval.End.Date == DateTime.Now.Date || temp.EquipmentData.Type == EquipmentType.consumable)
                {
                    temp.room = destination;
                    EquipmentController.Instance.AddRoomEquipment(temp);
                }
                else
                {
                    equipment.Add(temp);
                }
            }
            CreateRelocation(source, destination, interval, equipment);
        }

        private void CreateRelocation(Room source, Room destination, Interval interval, List<Equipment> equipment)
        {
            if (equipment.Count > 0)
            {
                EquipmentRelocation equipmentRelocation = new EquipmentRelocation
                {
                    SourceRoom = source,
                    DestinationRoom = destination,
                    Interval = interval,
                    Equipment = equipment
                };
                foreach (Equipment eq in equipment)
                {
                    EquipmentController.Instance.Create(eq);
                }
                EquipmentRelocationController.Instance.Create(equipmentRelocation);
            }
        }
        public string GetEquipmentErrors()
        {
            bool valid = true;
            string message = "";
            foreach (EquipmentDataModel eq in EquipmentList)
            {
                if (!eq.IsAmountValid())
                {
                    message += eq.Equipment.EquipmentData.Name + "'s amount must be within (0, " + eq.Equipment.Amount + "] interval\n";
                    valid = false;
                }
            }
            return valid ? null : message;
        }
    }
}
