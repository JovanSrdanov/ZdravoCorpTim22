using Model;
using System.Windows;
using ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder;

namespace ZdravoCorpAppTim22.View.Secretary
{
    public partial class SecretaryHome : Window
    {
        private MainWindow mainWindow;

        public SecretaryHome(SecretaryClass secretary)
        {
            InitializeComponent();
            this.mainWindow = (MainWindow)Application.Current.MainWindow;
        }

        private void AccountsBtn_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAccounts secretaryAccounts = new SecretaryAccounts(this);
            secretaryAccounts.Show();
            this.Hide();
        }

        private void EmergencyBtn_Click(object sender, RoutedEventArgs e)
        {
            SecretaryEmergencyOptions secretaryEmergencyOptions = new SecretaryEmergencyOptions(this);
            secretaryEmergencyOptions.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            mainWindow.Show();
        }

        private void ScheduleBtn_Click(object sender, RoutedEventArgs e)
        {
            SecretaryScheduleOptions secretarySchedule = new SecretaryScheduleOptions(this);
            secretarySchedule.Show();
            this.Hide();
        }

        private void MedicalOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            SecretaryMedicalOrder secretarySchedule = new SecretaryMedicalOrder(this);
            secretarySchedule.Show();
            this.Hide();
        }

        private void RequestsBtn_Click(object sender, RoutedEventArgs e)
        {
            SecretaryMVVMwindow secretaryMVVMwindow = new SecretaryMVVMwindow(this);
            secretaryMVVMwindow.Show();
            this.Hide();
        }
    }
}
