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

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            secretaryHome.Show();
            this.Close();
        }

        private void Accounts_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAccounts secretaryAccounts = new SecretaryAccounts(secretaryHome);
            secretaryAccounts.Show();
            this.Close();
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            SecretaryScheduleOptions secretaryScheduleOptions = new SecretaryScheduleOptions(secretaryHome);
            secretaryScheduleOptions.Show();
            this.Close();
        }

        private void Medical_order_Click(object sender, RoutedEventArgs e)
        {
            SecretaryMedicalOrder secretaryMedicalOrder = new SecretaryMedicalOrder(secretaryHome);
            secretaryMedicalOrder.Show();
            this.Close();
        }

        private void Emergency_Click(object sender, RoutedEventArgs e)
        {
            SecretaryEmergencyOptions secretaryEmergency = new SecretaryEmergencyOptions(secretaryHome);
            secretaryEmergency.Show();
            this.Close();
        }

        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            SecretaryMVVMfolder.SecretaryMVVMwindow secretaryMVVMwindow = new SecretaryMVVMfolder.SecretaryMVVMwindow(secretaryHome, true);
            secretaryMVVMwindow.Show();
            this.Close();
        }
    }
}
