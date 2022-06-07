using MVVM1;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.HospitalInformationTab
{
    public class HospitalReviewPageViewModel : BindableBase
    {
        public int Id { get; set; }
        public int StaffHospitality { get; set; }
        public int Accessibility { get; set; }
        public int Hygiene { get; set; }
        public int Appearance { get; set; }
        public int Application { get; set; }
        public MyICommand SubmitReviewCommand { get; set; }
        public bool StaffHospitalityGroup1 { get; set; }
        public bool StaffHospitalityGroup2 { get; set; }
        public bool StaffHospitalityGroup3 { get; set; }
        public bool StaffHospitalityGroup4 { get; set; }
        public bool StaffHospitalityGroup5 { get; set; }
        private void StaffHospitalitySet()
        {
            switch (StaffHospitality)
            {
                case 1:
                    StaffHospitalityGroup1 = true;
                    break;
                case 2:
                    StaffHospitalityGroup2 = true;
                    break;
                case 3:
                    StaffHospitalityGroup3 = true;
                    break;
                case 4:
                    StaffHospitalityGroup4 = true;
                    break;
                case 5:
                    StaffHospitalityGroup5 = true;
                    break;
                default:
                    StaffHospitalityGroup1 = true;
                    break;
            }
        }
        private int StaffHospitalityCheck()
        {
            if (StaffHospitalityGroup1)
            {
                return 1;
            }
            if (StaffHospitalityGroup2)
            {
                return 2;
            }
            if (StaffHospitalityGroup3)
            {
                return 3;
            }
            if (StaffHospitalityGroup4)
            {
                return 4;
            }
            if (StaffHospitalityGroup5)
            {
                return 5;
            }

            return -1;
        }
        public bool AccessibilityGroup1 { get; set; }
        public bool AccessibilityGroup2 { get; set; }
        public bool AccessibilityGroup3 { get; set; }
        public bool AccessibilityGroup4 { get; set; }
        public bool AccessibilityGroup5 { get; set; }
        private void AccessibilitySet()
        {
            switch (Accessibility)
            {
                case 1:
                    AccessibilityGroup1 = true;
                    break;
                case 2:
                    AccessibilityGroup2 = true;
                    break;
                case 3:
                    AccessibilityGroup3 = true;
                    break;
                case 4:
                    AccessibilityGroup4 = true;
                    break;
                case 5:
                    AccessibilityGroup5 = true;
                    break;
                default:
                    AccessibilityGroup1 = true;
                    break;
            }
        }
        private int AccessibilityCheck()
        {
            if (AccessibilityGroup1)
            {
                return 1;
            }
            if (AccessibilityGroup2)
            {
                return 2;
            }
            if (AccessibilityGroup3)
            {
                return 3;
            }
            if (AccessibilityGroup4)
            {
                return 4;
            }
            if (AccessibilityGroup5)
            {
                return 5;
            }

            return -1;
        }
        public bool ApplicationGroup1 { get; set; }
        public bool ApplicationGroup2 { get; set; }
        public bool ApplicationGroup3 { get; set; }
        public bool ApplicationGroup4 { get; set; }
        public bool ApplicationGroup5 { get; set; }
        private void ApplicationSet()
        {
            switch (Application)
            {
                case 1:
                    ApplicationGroup1 = true;
                    break;
                case 2:
                    ApplicationGroup2 = true;
                    break;
                case 3:
                    ApplicationGroup3 = true;
                    break;
                case 4:
                    ApplicationGroup4 = true;
                    break;
                case 5:
                    ApplicationGroup5 = true;
                    break;
                default:
                    ApplicationGroup1 = true;
                    break;
            }
        }
        private int ApplicationCheck()
        {
            if (ApplicationGroup1)
            {
                return 1;
            }
            if (ApplicationGroup2)
            {
                return 2;
            }
            if (ApplicationGroup3)
            {
                return 3;
            }
            if (ApplicationGroup4)
            {
                return 4;
            }
            if (ApplicationGroup5)
            {
                return 5;
            }

            return -1;
        }
        public bool HygieneGroup1 { get; set; }
        public bool HygieneGroup2 { get; set; }
        public bool HygieneGroup3 { get; set; }
        public bool HygieneGroup4 { get; set; }
        public bool HygieneGroup5 { get; set; }
        private int HygieneCheck()
        {
            if (HygieneGroup1)
            {
                return 1;
            }
            if (HygieneGroup2)
            {
                return 2;
            }
            if (HygieneGroup3)
            {
                return 3;
            }
            if (HygieneGroup4)
            {
                return 4;
            }
            if (HygieneGroup5)
            {
                return 5;
            }

            return -1;
        }
        private void HygieneSet()
        {
            switch (Hygiene)
            {
                case 1:
                    HygieneGroup1 = true;
                    break;
                case 2:
                    HygieneGroup2 = true;
                    break;
                case 3:
                    HygieneGroup3 = true;
                    break;
                case 4:
                    HygieneGroup4 = true;
                    break;
                case 5:
                    HygieneGroup5 = true;
                    break;
                default:
                    HygieneGroup1 = true;
                    break;
            }
        }
        public bool AppearanceGroup1 { get; set; }
        public bool AppearanceGroup2 { get; set; }
        public bool AppearanceGroup3 { get; set; }
        public bool AppearanceGroup4 { get; set; }
        public bool AppearanceGroup5 { get; set; }
        private int AppearanceCheck()
        {
            if (AppearanceGroup1)
            {
                return 1;
            }
            if (AppearanceGroup2)
            {
                return 2;
            }
            if (AppearanceGroup3)
            {
                return 3;
            }
            if (AppearanceGroup4)
            {
                return 4;
            }
            if (AppearanceGroup5)
            {
                return 5;
            }

            return -1;
        }
        private void AppearanceSet()
        {
            switch (Appearance)
            {
                case 1:
                    AppearanceGroup1 = true;
                    break;
                case 2:
                    AppearanceGroup2 = true;
                    break;
                case 3:
                    AppearanceGroup3 = true;
                    break;
                case 4:
                    AppearanceGroup4 = true;
                    break;
                case 5:
                    AppearanceGroup5 = true;
                    break;
                default:
                    AppearanceGroup1 = true;
                    break;
            }
        }
        public HospitalReviewPageViewModel(int id)
        {
            SubmitReviewCommand = new MyICommand(SubmitReview);

            SetStartValues(id);


            SetAllRadioButtons();


        }
        private void SetStartValues(int id)
        {
            HospitalReview hospitalReview = HospitalReviewController.Instance.GetByID(id);

            Id = hospitalReview.Id;
            StaffHospitality = hospitalReview.StaffHospitality;
            Accessibility = hospitalReview.Accessibility;
            Hygiene = hospitalReview.Hygiene;
            Appearance = hospitalReview.Appearance;
            Application = hospitalReview.Application;
        }
        public void SubmitReview()
        {
            SaveSelectedGrades();

            HospitalReviewController.Instance.SubmitReview(Id, StaffHospitality, Accessibility, Hygiene, Appearance,
                Application);

          


        }
        private void SaveSelectedGrades()
        {
            StaffHospitality = StaffHospitalityCheck();
            Accessibility = AccessibilityCheck();
            Hygiene = HygieneCheck();
            Appearance = AppearanceCheck();
            Application = ApplicationCheck();
        }
        private void SetAllRadioButtons()
        {
            StaffHospitalitySet();
            AccessibilitySet();
            ApplicationSet();
            AppearanceSet();
            HygieneSet();
        }

    }
}
