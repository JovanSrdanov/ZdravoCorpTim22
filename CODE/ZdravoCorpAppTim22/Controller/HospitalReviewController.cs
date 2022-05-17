
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class HospitalReviewController: GenericController<HospitalReviewService, HospitalReview>
    {
        private static HospitalReviewController instance;
        private HospitalReviewController() : base(HospitalReviewService.Instance) { }
        public static HospitalReviewController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HospitalReviewController();
                }

                return instance;
            }
        }
    }
}
