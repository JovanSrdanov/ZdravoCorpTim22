using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.HospitalInformationTab
{
   
    public partial class SuccesHospitalReviewPage : Page
    {
        public SuccesHospitalReviewPage()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            GeneralInformationView generalInformationView = new GeneralInformationView();
            this.NavigationService.Navigate(generalInformationView);
        }
    }
}
