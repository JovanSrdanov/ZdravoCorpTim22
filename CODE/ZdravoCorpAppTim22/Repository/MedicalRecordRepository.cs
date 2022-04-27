using Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
   public class MedicalRecordRepository : GenericRepository<MedicalRecord>
    {
        public static string FileName = "MedicalRecordData.json";
        private static MedicalRecordRepository instance;
        private MedicalRecordRepository() : base(FileName) { }
        public static MedicalRecordRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalRecordRepository();
                }

                return instance;
            }
        }
   }
}