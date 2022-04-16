using Model;
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
    public class RenovationController : GenericController<RenovationService, Renovation>
    {
        private static RenovationController instance;
        private RenovationController() : base(RenovationService.Instance) { }
        public static RenovationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenovationController();
                }
                return instance;
            }
        }
    }
}