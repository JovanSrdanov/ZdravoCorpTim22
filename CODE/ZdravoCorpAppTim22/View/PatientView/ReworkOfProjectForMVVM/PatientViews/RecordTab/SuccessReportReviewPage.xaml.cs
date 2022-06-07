using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab
{
  
    public partial class SuccessReportReviewPage : Page
    {
        public SuccessReportReviewPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ProfileView profileView = new ProfileView();
            this.NavigationService.Navigate(profileView);
        }
    }
}
