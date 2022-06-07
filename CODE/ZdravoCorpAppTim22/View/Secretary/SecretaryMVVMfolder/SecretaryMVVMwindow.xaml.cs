using System;
using System.Windows;

namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder
{
    /// <summary>
    /// Interaction logic for SecretaryMVVMwindow.xaml
    /// </summary>
    public partial class SecretaryMVVMwindow : Window
    {
        private SecretaryHome secretaryHome;

        public SecretaryMVVMwindow(SecretaryHome secretaryHome, bool RequestForAbsence)
        {
            InitializeComponent();
            this.secretaryHome = secretaryHome;
            if (RequestForAbsence)
            {
                SecretaryRequestForAbsence.Source =
                    new Uri("SecretaryRequestForAbsenceMVVMFolder/SecretaryRequestForAbsencePage.xaml", UriKind.Relative);
            }
            else
            {
                SecretaryRequestForAbsence.Source =
                    new Uri("SecretaryRequestForAbsenceMVVMFolder/SecretaryStuffMeetingOverviewPage.xaml", UriKind.Relative);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            secretaryHome.Show();
            this.Close();
        }
    }
}
