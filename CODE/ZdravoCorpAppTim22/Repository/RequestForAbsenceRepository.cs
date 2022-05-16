using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class RequestForAbsenceRepository : GenericRepository<RequestForAbsence>
    {
        public static string FileName = "RequestForAbsenceData.json";
        private static RequestForAbsenceRepository instance;
        private RequestForAbsenceRepository() : base(FileName) { }
        public static RequestForAbsenceRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RequestForAbsenceRepository();
                }
                return instance;
            }
        }
    }
}
