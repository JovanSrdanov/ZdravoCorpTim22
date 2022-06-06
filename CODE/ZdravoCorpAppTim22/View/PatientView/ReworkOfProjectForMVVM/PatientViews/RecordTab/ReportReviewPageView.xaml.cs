using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab
{

    public partial class ReportReviewPageView : Page
    {
        public ReportReviewPageView(int id)
        {
            InitializeComponent();

            this.DataContext = new ReportReviewViewModel(id);
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ProfileView profileView = new ProfileView();
            this.NavigationService.Navigate(profileView);
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
