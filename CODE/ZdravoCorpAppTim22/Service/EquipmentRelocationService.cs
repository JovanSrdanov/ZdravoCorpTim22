using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public override void DeleteByID(int id)
        {
            EquipmentRelocation rel = GetByID(id);
            List<Equipment> eqToMove = new List<Equipment>(rel.Equipment);
            foreach (Equipment eq in eqToMove)
            {
                eq.Room = null;
                EquipmentService.Instance.DeleteByID(eq.Id);
                EquipmentService.Instance.AddWarehouseEquipment(eq);
            }
            rel.SourceRoom = null;
            rel.DestinationRoom = null;
            base.DeleteByID(id);
        }
        public void DeleteMany(List<EquipmentRelocation> list)
        {
            foreach (EquipmentRelocation rel in list)
            {
                DeleteByID(rel.Id);
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
            if (App.Current != null)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    foreach (var item in list)
                    {
                        Room destination = item.DestinationRoom;
                        if (destination == null)
                        {
                            foreach (Equipment eq in item.Equipment)
                            {
                                item.SourceRoom = null;
                                EquipmentService.Instance.AddWarehouseEquipment(eq);
                            }
                        }
                        else
                        {
                            foreach (Equipment eq in item.Equipment)
                            {
                                item.DestinationRoom = null;
                                item.SourceRoom = null;
                                EquipmentService.Instance.AddRoomEquipment(destination, eq);
                            }
                        }
                        foreach (Equipment eq in item.Equipment)
                        {
                            EquipmentService.Instance.DeleteByID(eq.Id);
                        }
                        item.RemoveAllEquipment();
                        DeleteByID(item.Id);
                    }
                });
            }
        }
    }
}