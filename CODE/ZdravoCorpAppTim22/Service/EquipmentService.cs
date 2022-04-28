using Model;
using System.Collections.Generic;
using System.Diagnostics;
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
        public List<Equipment> GetRoomEquipment(int id)
        {
            return EquipmentRepository.Instance.GetRoomEquipment(id);
        }

        public void AddWarehouseEquipment(Equipment eq)
        {
            if(eq.Room == null)
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
        }

        public void AddRoomEquipment(Equipment eq)
        {
            if(eq.Room != null)
            {
                List<Equipment> roomEq = GetRoomEquipment(eq.Room.Id);
                
                foreach (Equipment eqItem in roomEq)
                {
                    if (eqItem.EquipmentData.Id == eq.EquipmentData.Id)
                    {
                        eqItem.Amount += eq.Amount;
                        Update(eqItem);
                        return;
                    }
                }
                Equipment newEq = new Equipment(eq)
                {
                    Room = eq.Room,
                    EquipmentRelocation = null
                };
                Create(newEq);
            }
        }
    }
}