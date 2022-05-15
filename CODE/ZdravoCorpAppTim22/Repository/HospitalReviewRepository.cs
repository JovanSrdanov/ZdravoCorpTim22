using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Model
{
    public class HospitalReviewRepository : GenericRepository<HospitalReview>
    {

        public static string FileName = "HospitalReview.json";
        private static HospitalReviewRepository instance;

        private HospitalReviewRepository() : base(FileName) { }
        public static HospitalReviewRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HospitalReviewRepository();
                }

                return instance;
            }
        }
    }
}