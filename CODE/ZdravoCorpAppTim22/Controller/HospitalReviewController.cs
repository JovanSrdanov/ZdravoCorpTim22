
using System;
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

        internal void SubmitReview(int id, int staffHospitality, int accessibility, int hygiene, int appearance, int application)
        {
            HospitalReview hospitalReview = Instance.GetByID(id);

            hospitalReview.StaffHospitality = staffHospitality;
            hospitalReview.Accessibility = accessibility;
            hospitalReview.Hygiene = staffHospitality;
            hospitalReview.Appearance = appearance;
            hospitalReview.Application = application;

            instance.Update(hospitalReview);

        }
    }
}
