using System.Windows;

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryEmergencyOptions.xaml
    /// </summary>
    public partial class SecretaryEmergencyOptions : Window
    {
        SecretaryHome SecretaryHome;
        public SecretaryEmergencyOptions(SecretaryHome secretaryHome)
        {
            InitializeComponent();
            this.SecretaryHome = secretaryHome;
        }

        private void btnRegisteredAccount_Click(object sender, RoutedEventArgs e)
        {
            SecretaryEmergency secretaryEmergency = new SecretaryEmergency(SecretaryHome, true);
            secretaryEmergency.Show();
            this.Hide();
        }

        private void btnNewAccount_Click(object sender, RoutedEventArgs e)
        {
            SecretaryEmergency secretaryEmergency = new SecretaryEmergency(SecretaryHome, false);
            secretaryEmergency.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            SecretaryHome.Show();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            SecretaryHome.Show();
            this.Close();
        }

        private void Accounts_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAccounts secretaryAccounts = new SecretaryAccounts(SecretaryHome);
            secretaryAccounts.Show();
            this.Close();
        }

        private void Schedule_Click(object sender, RoutedEventArgs e)
        {
            SecretaryScheduleOptions secretaryScheduleOptions = new SecretaryScheduleOptions(SecretaryHome);
            secretaryScheduleOptions.Show();
            this.Close();
        }

        private void Medical_order_Click(object sender, RoutedEventArgs e)
        {
            SecretaryMedicalOrder secretaryMedicalOrder = new SecretaryMedicalOrder(SecretaryHome);
            secretaryMedicalOrder.Show();
            this.Close();
        }

        private void Emergency_Click(object sender, RoutedEventArgs e)
        {
            SecretaryEmergencyOptions secretaryEmergency = new SecretaryEmergencyOptions(SecretaryHome);
            secretaryEmergency.Show();
            this.Close();
        }

        private void Requests_Click(object sender, RoutedEventArgs e)
        {
            SecretaryMVVMfolder.SecretaryMVVMwindow secretaryMVVMwindow = new SecretaryMVVMfolder.SecretaryMVVMwindow(SecretaryHome, true);
            secretaryMVVMwindow.Show();
            this.Close();
        }
    }
}
