using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class ApprovalController : GenericController<ApprovalService, Approval>
    {
        private static ApprovalController instance;
        private ApprovalController() : base(ApprovalService.Instance) { }
        public static ApprovalController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ApprovalController();
                }

                return instance;
            }
        }
    }
}
