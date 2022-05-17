using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class DoctorSpecializationService : GenericService<DoctorSpecializationRepository, DoctorSpecialization>
    {
        private static DoctorSpecializationService instance;
        private DoctorSpecializationService() : base(DoctorSpecializationRepository.Instance) { }
        public static DoctorSpecializationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorSpecializationService();
                }
                return instance;
            }
        }
    }
}
