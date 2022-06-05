using Model;
using Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class MedicalReportService : GenericService<MedicalReportRepository, MedicalReport>
    {
        private MedicalReportService() : base(MedicalReportRepository.Instance) { }
        public static MedicalReportService Instance
        {
            get
            {
                return new MedicalReportService();
            }
        }
    }
}
