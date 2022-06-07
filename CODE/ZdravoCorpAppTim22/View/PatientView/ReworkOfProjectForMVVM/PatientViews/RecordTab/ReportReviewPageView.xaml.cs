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
            SuccessReportReviewPage successReportReviewPage = new SuccessReportReviewPage();
            this.NavigationService.Navigate(successReportReviewPage);
        }

        private void Button_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
