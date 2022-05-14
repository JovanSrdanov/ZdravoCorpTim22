using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class HospitalReviewService : GenericService<HospitalReviewRepository, HospitalReview>
    {
        private static HospitalReviewService instance;

        private HospitalReviewService() : base(HospitalReviewRepository.Instance) { }

        public static HospitalReviewService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new HospitalReviewService();
                }

                return instance;
            }
        }
    }
}