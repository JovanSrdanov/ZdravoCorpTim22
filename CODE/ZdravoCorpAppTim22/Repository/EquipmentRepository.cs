using Model;
using System.Collections.Generic;
using System.Diagnostics;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class EquipmentRepository : GenericRepository<Equipment>
    {
        public static string FileName = "EquipmentData.json";
        private static EquipmentRepository instance;
        private EquipmentRepository() : base(FileName) { }
        public static EquipmentRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentRepository();
                }

                return instance;
            }
        }

        public List<Equipment> GetWarehouseEquipment()
        {
            List<Equipment> equipmentList = new List<Equipment>();
            foreach (Equipment eq in List)
            {
                if (eq.Room == null && eq.EquipmentRelocation == null)
                {
                    equipmentList.Add(eq);
                }
            }
            return equipmentList;
        }

        public List<Equipment> GetRoomEquipment(int id)
        {
            List<Equipment> equipmentList = new List<Equipment>();
            foreach (Equipment eq in List)
            {
                if (eq.Room != null && eq.Room.Id == id && eq.EquipmentRelocation == null)
                {
                    equipmentList.Add(eq);
                }
            }
            return equipmentList;
        }
    }
}