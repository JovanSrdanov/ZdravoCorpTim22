using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class MedicineDataRepository : GenericRepository<MedicineData>
    {
        public static string FileName = "MedicineInstanceData.Json";
        private static MedicineDataRepository instance;
        private MedicineDataRepository() : base(FileName) { }
        public static MedicineDataRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineDataRepository();
                }
                return instance;
            }
        }
    }
}
