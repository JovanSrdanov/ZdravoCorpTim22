using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class DoctorSpecializationService : GenericService<DoctorSpecializationRepository, DoctorSpecialization>
    {
        private DoctorSpecializationService() : base(DoctorSpecializationRepository.Instance) { }
        public static DoctorSpecializationService Instance
        {
            get
            {
                return new DoctorSpecializationService();
            }
        }
    }
}
