using Model;
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
    public class RenovationService : GenericService<RenovationRepository, Renovation>
    {
        private static RenovationService instance;
        private RenovationService() : base(RenovationRepository.Instance) { }
        public static RenovationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenovationService();
                }

                return instance;
            }
        }
    }
}