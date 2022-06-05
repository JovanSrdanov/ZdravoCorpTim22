using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class HospitalReviewService : GenericService<HospitalReviewRepository, HospitalReview>
    {
        private HospitalReviewService() : base(HospitalReviewRepository.Instance) { }
        public static HospitalReviewService Instance
        {
            get
            {
                return new HospitalReviewService();
            }
        }
    }
}