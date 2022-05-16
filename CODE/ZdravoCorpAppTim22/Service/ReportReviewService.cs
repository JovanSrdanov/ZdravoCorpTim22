using Model;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class ReportReviewService : GenericService<ReportReviewRepository, ReportReview>
    {
        private static ReportReviewService instance;

        private ReportReviewService() : base(ReportReviewRepository.Instance) { }

        public static ReportReviewService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReportReviewService();
                }

                return instance;
            }
        }
    }
}