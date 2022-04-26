using Model;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    internal class MedicalReportController : GenericController<MedicalReportService, MedicalReport>
    {
        private static MedicalReportController instance;
        private MedicalReportController() : base(MedicalReportService.Instance) { }
        public static MedicalReportController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalReportController();
                }
                return instance;
            }
        }
    }
}
