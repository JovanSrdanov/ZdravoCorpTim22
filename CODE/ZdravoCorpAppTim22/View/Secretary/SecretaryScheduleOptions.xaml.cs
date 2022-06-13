using System.Windows;
using ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder;

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryScheduleOptions.xaml
    /// </summary>
    public partial class SecretaryScheduleOptions : Window
    {
        SecretaryHome secretaryHome;
        public SecretaryScheduleOptions(SecretaryHome secretaryHome)
        {
            InitializeComponent();
            this.secretaryHome = secretaryHome;
        }

        private void btnNew_Click(object sender, RoutedEventArgs e)
        {
            SecretarySchedule secretarySchedule = new SecretarySchedule(this);
            secretarySchedule.Show();
            this.Hide();
        }

        private void BtnChangeSchedule_Click(object sender, RoutedEventArgs e)
        {
            SecretaryChangeSchedule secretaryScheduleChange = new SecretaryChangeSchedule(this);
            secretaryScheduleChange.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            secretaryHome.Show();
            this.Close();
        }

        private void btnNewStuffMeeting_Click(object sender, RoutedEventArgs e)
        {
            SecretaryMVVMwindow secretaryMVVMwindow = new SecretaryMVVMwindow(secretaryHome, false);
            secretaryMVVMwindow.Show();
            this.Hide();
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

        private void gtnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            SecretaryGenerateReport secretaryGenerateReport = new SecretaryGenerateReport(secretaryHome);
            secretaryGenerateReport.Show();
            this.Close();
        }
    }
}
