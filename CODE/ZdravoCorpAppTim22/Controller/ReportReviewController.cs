using Model;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class ReportReviewController : GenericController<ReportReviewService, ReportReview>
    {
        private static ReportReviewController instance;
        private ReportReviewController() : base(ReportReviewService.Instance) { }
        public static ReportReviewController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReportReviewController();
                }

                return instance;
            }
        }
    }
}