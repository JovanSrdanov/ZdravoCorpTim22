using Model;
using Repository;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class EquipmentRelocationService : GenericService<EquipmentRelocationRepository, EquipmentRelocation>
    {
        private EquipmentRelocationService() : base(EquipmentRelocationRepository.Instance) { }
        public static EquipmentRelocationService Instance
        {
            get
            {
                return new EquipmentRelocationService();
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
        public void DeleteByList(List<EquipmentRelocation> list)
        {
            foreach (EquipmentRelocation rel in list)
            {
                DeleteByID(rel.Id);
            }
        }

        //Method that's being run every second from the background thread
        public void BackgroundTask()
        {
            List<EquipmentRelocation> list = GetAllExpiredRelocations();
            if(list.Count > 0)
            {
                App.Current?.Dispatcher?.Invoke(DoExpiredRelocations);
            }
        }

        #region private

        private void DoExpiredRelocations()
        {
            List<EquipmentRelocation> list = GetAllExpiredRelocations();
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
                DeleteExpiredRelocation(item);
            }
        }
        private List<EquipmentRelocation> GetAllExpiredRelocations()
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
            return list;
        }
        private void DeleteExpiredRelocation(EquipmentRelocation relocation)
        {
            foreach (Equipment eq in relocation.Equipment)
            {
                EquipmentService.Instance.DeleteByID(eq.Id);
            }
            relocation.RemoveAllEquipment();
            DeleteByID(relocation.Id);
        }
        #endregion
    }
}