using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ZdravoCorpAppTim22;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class EquipmentRelocationService : GenericService<EquipmentRelocationRepository, EquipmentRelocation>
    {
        private static EquipmentRelocationService instance;
        private EquipmentRelocationService() : base(EquipmentRelocationRepository.Instance) { }
        public static EquipmentRelocationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentRelocationService();
                }
                return instance;
            }
        }
        public void DaemonMethod()
        {
            List<EquipmentRelocation> list = new List<EquipmentRelocation>();
            lock (EquipmentRelocationRepository.Instance._lock)
            {
                foreach (var item in GetAll())
                {
                    if (item.Interval.End <= DateTime.Now)
                    {
                        list.Add(item);
                    }
                }
            }
            App.Current.Dispatcher.Invoke(delegate
            {
                foreach (var item in list)
                {
                    Room destination = item.DestinationRoom;
                    if(destination == null)
                    {
                        foreach (Equipment eq in item.Equipment)
                        {
                            eq.Room = null;
                            item.SourceRoom = null;
                            EquipmentService.Instance.AddWarehouseEquipment(eq);
                        }
                    }
                    else
                    {
                        foreach (Equipment eq in item.Equipment)
                        {
                            eq.Room = destination;
                            item.DestinationRoom = null;
                            item.SourceRoom = null;
                            EquipmentService.Instance.AddRoomEquipment(eq);
                        }
                    }
                    foreach (Equipment eq in item.Equipment)
                    {
                        EquipmentService.Instance.DeleteByID(eq.Id);
                    }
                    DeleteByID(item.Id);
                }
            });
        }
    }
}