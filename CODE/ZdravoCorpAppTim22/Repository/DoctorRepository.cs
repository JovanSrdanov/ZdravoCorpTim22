using Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
    public class DoctorRepository : GenericRepository<Doctor>
    {
        public static string FileName = "DoctorData.json";
        private static DoctorRepository instance;
        private DoctorRepository() : base(FileName) { }
        public static DoctorRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorRepository();

                }

                return instance;
            }
        }
    }
}