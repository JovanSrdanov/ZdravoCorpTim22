using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.FileHandlers;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class RenovationRepository : GenericRepository<Renovation>
    {
        public static string FileName = "RenovationData.json";
        private static RenovationRepository instance;
        private RenovationRepository() : base(FileName) { }
        public static RenovationRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenovationRepository();
                }

                return instance;
            }
        }
    }
}
