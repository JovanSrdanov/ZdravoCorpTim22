using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
            return EquipmentRepository.Instance.GetWarehouseEquipment();
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
    }
}