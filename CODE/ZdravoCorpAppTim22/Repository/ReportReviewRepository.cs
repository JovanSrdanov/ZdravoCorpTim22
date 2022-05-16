using Model;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class ReportReviewRepository : GenericRepository<ReportReview>
    {
        public static string FileName = "ReportReview.json";
        private static ReportReviewRepository instance;

        private ReportReviewRepository() : base(FileName) { }
        public static ReportReviewRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReportReviewRepository();
                }

                return instance;
            }
        }
    }
}
