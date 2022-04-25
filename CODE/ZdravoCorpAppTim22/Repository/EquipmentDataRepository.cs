using Model;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class EquipmentDataRepository : GenericRepository<EquipmentData>
    {
        public static string FileName = "EquipmentInstanceData.json";
        private static EquipmentDataRepository instance;
        private EquipmentDataRepository() : base(FileName) { }
        public static EquipmentDataRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentDataRepository();
                }

                return instance;
            }
        }
    }
}