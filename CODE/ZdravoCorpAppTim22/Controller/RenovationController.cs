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
        public void BackgroundTask()
        {
            RenovationService.Instance.BackgroundTask();
        }
    }
}