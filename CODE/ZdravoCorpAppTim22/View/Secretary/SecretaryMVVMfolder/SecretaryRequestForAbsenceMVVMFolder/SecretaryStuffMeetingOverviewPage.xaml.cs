using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder.SecretaryRequestForAbsenceMVVMFolder
{
    /// <summary>
    /// Interaction logic for SecretaryStuffMeetingOverviewPage.xaml
    /// </summary>
    public partial class SecretaryStuffMeetingOverviewPage : Page
    {
        public SecretaryStuffMeetingOverviewPage()
        {
            InitializeComponent();
            SecretaryStuffMeetingOverviewPageViewModel secretaryStuffMeetingOverviewPageViewModel = new SecretaryStuffMeetingOverviewPageViewModel();
            this.DataContext = secretaryStuffMeetingOverviewPageViewModel;
        }
    }
}
