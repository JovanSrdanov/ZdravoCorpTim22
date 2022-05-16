using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
