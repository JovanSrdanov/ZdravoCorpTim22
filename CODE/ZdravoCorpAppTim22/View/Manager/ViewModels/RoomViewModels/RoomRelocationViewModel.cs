using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.DataModel;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels
{
    public class RoomRelocationViewModel
    {
        public ObservableCollection<Room> RoomList { get; set; }
        public ObservableCollection<EquipmentDataModel> EquipmentList { get; set; }
        public Interval Interval { get; set; }
        public Room SourceRoom { get; set; }
        public Room DestinationRoom { get; set; }

        public RelayCommand RelocateCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }

        public RoomRelocationViewModel(List<Equipment> eq, Room room)
        {
            SourceRoom = room;
            RelocateCommand = new RelayCommand(Relocate, CanRelocate);
            NavigateBackCommand = new RelayCommand(NavigateBack, null);

            List<EquipmentDataModel> temp = new List<EquipmentDataModel>();
            foreach (Equipment eqItem in eq)
            {
                temp.Add(new EquipmentDataModel(eqItem.Amount, eqItem));
            }
            EquipmentList = new ObservableCollection<EquipmentDataModel>(temp);
            RoomList = new RoomViewModel(room).RoomList;
        }
        
        public void Relocate(object obj)
        {
            RoomToRoom(SourceRoom, DestinationRoom, Interval);
            ManagerHome.NavigationService.Navigate(new RoomDetailsView(SourceRoom));
        }
        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new RoomDetailsView(SourceRoom));
        }

        private bool CanRelocate(object obj)
        {
            bool valid = true;
            foreach (EquipmentDataModel eq in EquipmentList)
            {
                if (!eq.IsAmountValid())
                {
                    valid = false;
                }
            }
            return valid;
        }
        
        private void RoomToRoom(Room source, Room destination, Interval interval)
        {
            if (!source.IsAvailable(interval) || !destination.IsAvailable(interval))
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
                    eq.Equipment.Room = null;
                    EquipmentController.Instance.DeleteByID(eq.Equipment.Id);
                }

                if (interval.End <= DateTime.Now || temp.EquipmentData.Type == EquipmentType.consumable)
                {
                    EquipmentController.Instance.AddRoomEquipment(destination, temp);
                }
                else
                {
                    equipment.Add(temp);
                }
            }
            EquipmentRelocationController.Instance.Create(source, destination, interval, equipment);
        }
    }
}
