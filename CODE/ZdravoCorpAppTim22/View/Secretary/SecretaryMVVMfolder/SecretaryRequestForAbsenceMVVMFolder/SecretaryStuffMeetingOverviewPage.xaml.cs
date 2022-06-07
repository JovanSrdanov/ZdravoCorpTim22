using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder.SecretaryRequestForAbsenceMVVMFolder
{
    /// <summary>
    /// Interaction logic for SecretaryStuffMeetingOverviewPage.xaml
    /// </summary>
    public partial class SecretaryStuffMeetingOverviewPage : Page
    {
        SecretaryStuffMeetingOverviewPageViewModel secretaryStuffMeetingOverviewPageViewModel = new SecretaryStuffMeetingOverviewPageViewModel();
        public SecretaryStuffMeetingOverviewPage()
        {
            InitializeComponent();
            this.DataContext = secretaryStuffMeetingOverviewPageViewModel;
        }


        private void btnNew_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SecretaryNewStuffMeetingPage secretaryNewStuffMeetingPage = new SecretaryNewStuffMeetingPage();
            NavigationService.Navigate(secretaryNewStuffMeetingPage);
        }

        private void btnEdit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            SecretaryStuffMeetingDialogPage secretaryStuffMeetingDialogPage = new SecretaryStuffMeetingDialogPage();
            NavigationService.Navigate(secretaryStuffMeetingDialogPage);
        }

        private void btnDelete_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete this meeting", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                secretaryStuffMeetingOverviewPageViewModel.StuffMeetings.Remove(secretaryStuffMeetingOverviewPageViewModel.SelectedStuffMeeting);
            }
        }
    }
}
