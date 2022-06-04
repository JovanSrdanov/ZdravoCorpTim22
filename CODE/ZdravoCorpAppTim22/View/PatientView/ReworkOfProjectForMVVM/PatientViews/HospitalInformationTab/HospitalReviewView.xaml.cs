using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.HospitalInformationTab
{

    public partial class HospitalReviewView : Page
    {
        public HospitalReviewView()
        {
            InitializeComponent();
        }

        private void CancleButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            GeneralInformationView generalInformationView = new GeneralInformationView();
            this.NavigationService.Navigate(generalInformationView);
        }
    }
}
