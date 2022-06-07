using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.HospitalInformationTab
{
    public partial class HospitalReviewPageView : Page
    {
        public HospitalReviewPageView(HospitalReviewPageViewModel hospitalReviewPageViewModel)
        {
            this.DataContext = hospitalReviewPageViewModel;
            InitializeComponent();
        }
        private void CancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

            GeneralInformationView generalInformationView = new GeneralInformationView();
            this.NavigationService.Navigate(generalInformationView);
        }
        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            GeneralInformationView generalInformationView = new GeneralInformationView();
            this.NavigationService.Navigate(generalInformationView);

        }
    }
}
