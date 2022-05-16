using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class DoctorSpecializationRepository : GenericRepository<DoctorSpecialization>
    {
        public static string FileName = "DoctorSpecializationData.json";
        private static DoctorSpecializationRepository instance;
        private DoctorSpecializationRepository() : base(FileName) { }
        public static DoctorSpecializationRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorSpecializationRepository();
                }
                return instance;
            }
        }
    }
}
