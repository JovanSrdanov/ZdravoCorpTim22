using MVVM1;
using System.Windows;
using Controller;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.PatientView.ViewModelForMVVM
{
    class HospitalReviewViewModel : BindableBase
    {

        public int StaffHospitality { get; set; }
        public int Accessibility { get; set; }
        public int Hygiene { get; set; }
        public int Appearance { get; set; }
        public int Application { get; set; }
        public MyICommand SubmitReviewCommand { get; set; }
        public HospitalReview SelectedHospitalReview { get; set; }

        public HospitalReviewViewModel()
        {
            if (PatientSelectionForTemporaryPurpose.LoggedPatient.HospitalReview == null)
            {
                SelectedHospitalReview = new HospitalReview();
                PatientSelectionForTemporaryPurpose.LoggedPatient.HospitalReview = SelectedHospitalReview;
                HospitalReviewController.Instance.Create(SelectedHospitalReview);
                PatientController.Instance.Update(PatientSelectionForTemporaryPurpose.LoggedPatient);

            }

            else
            {
                SelectedHospitalReview = PatientSelectionForTemporaryPurpose.LoggedPatient.HospitalReview;
            }

            SubmitReviewCommand = new MyICommand(SubmitReview);
        }

        public void SubmitReview()
        {
            SelectedHospitalReview.StaffHospitality = 2;
            SelectedHospitalReview.Accessibility = 2;
            SelectedHospitalReview.Hygiene = 2;
            SelectedHospitalReview.Appearance = 6;
            SelectedHospitalReview.Application = 6;

            PatientSelectionForTemporaryPurpose.LoggedPatient.hospitalReview = SelectedHospitalReview;
            HospitalReviewController.Instance.Update(SelectedHospitalReview);
            PatientController.Instance.Update(PatientSelectionForTemporaryPurpose.LoggedPatient);

            MessageBox.Show("Uspesno ocenjena bolnica");

        }


    }
}
