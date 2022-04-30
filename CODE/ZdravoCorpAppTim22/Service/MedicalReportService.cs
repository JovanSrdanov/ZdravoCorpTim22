using Model;
using Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class MedicalReportService : GenericService<MedicalReportRepository, MedicalReport>
    {
        private static MedicalReportService instance;
        private MedicalReportService() : base(MedicalReportRepository.Instance) { }
        public static MedicalReportService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalReportService();
                }
                return instance;
            }
        }
    }
}
