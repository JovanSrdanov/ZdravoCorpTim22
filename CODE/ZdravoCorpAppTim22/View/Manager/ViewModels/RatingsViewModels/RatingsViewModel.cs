using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.RatingsViewModels
{
    public class RatingsViewModel
    {
        public List<int> StaffGrades = new List<int>();
        public List<int> AccessibilityGrades = new List<int>();
        public List<int> HygieneGrades = new List<int>();
        public List<int> AppearanceGrades = new List<int>();
        public List<int> ApplicationGrades = new List<int>();

        public RatingsViewModel()
        {
            foreach(HospitalReview review in HospitalReviewController.Instance.GetAll())
            {
                StaffGrades.Add(review.StaffHospitality);
                AccessibilityGrades.Add(review.Accessibility);
                HygieneGrades.Add(review.Hygiene);
                AppearanceGrades.Add(review.Appearance);
                ApplicationGrades.Add(review.Application);
            }
        }
    }
}
