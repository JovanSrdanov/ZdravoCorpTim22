using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Service;
using ZdravoCorpAppTim22.View.Manager.DataModel;

namespace Controller
{
   public class EquipmentRelocationController : GenericController<EquipmentRelocationService, EquipmentRelocation>
    {
        private static EquipmentRelocationController instance;
        private EquipmentRelocationController() : base(EquipmentRelocationService.Instance) { }
        public static EquipmentRelocationController Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentRelocationController();
                }
                return instance;
            }
        }
        public void Create(Room source, Room destination, Interval interval, List<Equipment> equipment)
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
                Create(equipmentRelocation);
            }
        }
        public void BackgroundTask()
        {
            EquipmentRelocationService.Instance.BackgroundTask();
        }

        public void MoveRoomToWarehouse(Room source, List<EquipmentDataModel> list, Interval interval)
        {
            List<Equipment> equipment = new List<Equipment>();
            foreach (EquipmentDataModel eq in list)
            {
                Equipment temp = EquipmentController.Instance.GetEquipmentAndUpdateSource(eq.Equipment, eq.Amount, true);
                if (interval.End <= DateTime.Now || temp.EquipmentData.Type == EquipmentType.consumable)
                {
                    temp.room = null;
                    EquipmentController.Instance.AddWarehouseEquipment(temp);
                }
                else
                {
                    equipment.Add(temp);
                }
            }
            Create(source, null, interval, equipment);
        }

        public void MoveWarehouseToRoom(Room destination, List<EquipmentDataModel> list, Interval interval)
        {
            List<Equipment> equipment = new List<Equipment>();
            foreach (EquipmentDataModel eq in list)
            {
                Equipment temp = EquipmentController.Instance.GetEquipmentAndUpdateSource(eq.Equipment, eq.Amount, false);

                if (interval.End <= DateTime.Now || temp.EquipmentData.Type == EquipmentType.consumable)
                {
                    EquipmentController.Instance.AddRoomEquipment(destination, temp);
                }
                else
                {
                    equipment.Add(temp);
                }
            }
            Create(null, destination, interval, equipment);
        }

        public void MoveRoomToRoom(Room source, Room destination, List<EquipmentDataModel> list, Interval interval)
        {
            List<Equipment> equipment = new List<Equipment>();
            foreach (EquipmentDataModel eq in list)
            {
                Equipment temp = EquipmentController.Instance.GetEquipmentAndUpdateSource(eq.Equipment, eq.Amount, true);

                if (interval.End <= DateTime.Now || temp.EquipmentData.Type == EquipmentType.consumable)
                {
                    EquipmentController.Instance.AddRoomEquipment(destination, temp);
                }
                else
                {
                    equipment.Add(temp);
                }
            }
            Create(source, destination, interval, equipment);
        }
    }
}