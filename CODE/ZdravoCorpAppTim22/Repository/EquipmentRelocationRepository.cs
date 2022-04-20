using ZdravoCorpAppTim22.Repository.Generic;
using ZdravoCorpAppTim22.Model;

namespace Repository
{
    public class EquipmentRelocationRepository : GenericRepository<EquipmentRelocation>
    {
        public static string FileName = "EquipmentRelocationData.json";
        private static EquipmentRelocationRepository instance;
        private EquipmentRelocationRepository() : base(FileName) { }
        public static EquipmentRelocationRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentRelocationRepository();

                }
                return instance;
            }
        }
    }
}