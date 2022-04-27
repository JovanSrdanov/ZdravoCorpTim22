using Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class MedicalReportRepository : GenericRepository<MedicalReport>
    {
        public static string FileName = "MedicalReportData.json";
        private static MedicalReportRepository instance;
        private MedicalReportRepository() : base(FileName){ }
        public static MedicalReportRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalReportRepository();
                }

                return instance;
            }
        }
    }
}
