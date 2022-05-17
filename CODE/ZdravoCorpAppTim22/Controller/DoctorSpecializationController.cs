using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class DoctorSpecializationController : GenericController<DoctorSpecializationService, DoctorSpecialization>
    {
        private static DoctorSpecializationController instance;
        private DoctorSpecializationController() : base(DoctorSpecializationService.Instance) { }
        public static DoctorSpecializationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorSpecializationController();
                }
                return instance;
            }
        }
    }
}
