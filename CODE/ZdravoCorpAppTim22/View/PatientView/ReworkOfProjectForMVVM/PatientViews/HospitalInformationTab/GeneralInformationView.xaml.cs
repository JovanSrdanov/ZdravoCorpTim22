using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.HospitalInformationTab
{
    /// <summary>
    /// Interaction logic for TestV1.xaml
    /// </summary>
    public partial class GeneralInformationView : Page
    {
        public GeneralInformationView()
        {
            InitializeComponent();
        }

        private void RateZdravoCorp_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            HospitalReviewView hospitalReviewView = new HospitalReviewView();
            this.NavigationService.Navigate(hospitalReviewView);

        }
    }
}
