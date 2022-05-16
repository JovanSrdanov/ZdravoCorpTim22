﻿using Model;
using System.Collections.Generic;
using System.Linq;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class EquipmentService : GenericService<EquipmentRepository, Equipment>
    {
        private static EquipmentService instance;
        private EquipmentService() : base(EquipmentRepository.Instance) { }
        public static EquipmentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentService();
                }

                return instance;
            }
        }

        public List<Equipment> GetWarehouseEquipment()
        {
            List<Equipment> equipmentList = new List<Equipment>();
            foreach (Equipment eq in GetAll())
            {
                if (eq.Room == null && eq.EquipmentRelocation == null)
                {
                    equipmentList.Add(eq);
                }
            }
            return equipmentList;
        }
        public void AddWarehouseEquipment(Equipment eq)
        {
            List<Equipment> warehouseEq = GetWarehouseEquipment();
            foreach(Equipment eqItem in warehouseEq)
            {
                if(eqItem.EquipmentData.Id == eq.EquipmentData.Id)
                {
                    eqItem.Amount += eq.Amount;
                    Update(eqItem);
                    return;
                }
            }
            Equipment newEq = new Equipment(eq)
            {
                Room = null,
                EquipmentRelocation = null
            };
            Create(newEq);
        }
        public void AddRoomEquipment(Room destination, Equipment eq)
        {
            if(destination != null && eq != null)
            {
                Equipment roomEq = destination.Equipment.Where(r => r.EquipmentData.Id == eq.EquipmentData.Id).FirstOrDefault();
                if (roomEq != null)
                {
                    roomEq.Amount += eq.Amount;
                    int index = destination.Equipment.IndexOf(roomEq);
                    destination.Equipment.RemoveAt(index);
                    destination.Equipment.Insert(index, roomEq);
                    Update(roomEq);
                }else
                {
                    Equipment newEq = new Equipment(eq)
                    {
                        Room = destination,
                        EquipmentRelocation = null
                    };
                    Create(newEq);
                }
            }
        }
        public Equipment GetEquipmentAndUpdateSource(Equipment source, int amount, bool deleteIfEmpty)
        {
            Equipment temp = new Equipment(source)
            {
                Amount = amount
            };
            source.Amount -= amount;
            if (deleteIfEmpty)
            {
                if (source.Amount > 0)
                {
                    Update(source);
                }
                else
                {
                    source.Room = null;
                    DeleteByID(source.Id);
                }
            }
            else
            {
                Update(source);
            }
            return temp;
        }
    }
}