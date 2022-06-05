using Model;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.HospitalInformationTab
{
    public partial class GeneralInformationView : Page
    {
        public GeneralInformationView()
        {
            InitializeComponent();
        }
        private void RateZdravoCorp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Patient patient = (Patient)AuthenticationController.Instance.GetLoggedUser();
            HospitalReviewPageViewModel hospitalReviewPageViewModel = new HospitalReviewPageViewModel(patient.hospitalReview.Id);
            HospitalReviewPageView hospitalReviewPageView = new HospitalReviewPageView(hospitalReviewPageViewModel);
            this.NavigationService.Navigate(hospitalReviewPageView);
        }
    }
}
