using Model;
using Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
    public class DoctorService : GenericService<DoctorRepository, Doctor>
    {
        private static DoctorService instance;
        private DoctorService() : base(DoctorRepository.Instance) { }
        public static DoctorService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorService();
                }

                return instance;
            }
        }
    }
}