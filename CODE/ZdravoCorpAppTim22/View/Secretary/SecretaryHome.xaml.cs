using System.Windows;

namespace ZdravoCorpAppTim22.View.Secretary
{
    public partial class SecretaryHome : Window
    {
        private MainWindow mainWindow;

        public SecretaryHome(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void AccountsBtn_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAccounts secretaryAccounts = new SecretaryAccounts(this);
            secretaryAccounts.Show();
            this.Hide();
        }

        private void EmergencyBtn_Click(object sender, RoutedEventArgs e)
        {
            SecretaryEmergency secretaryEmergency = new SecretaryEmergency(this);
            secretaryEmergency.Show();
            this.Hide();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            mainWindow.Show();
        }

        private void ScheduleBtn_Click(object sender, RoutedEventArgs e)
        {
            SecretarySchedule secretarySchedule = new SecretarySchedule(this);
            secretarySchedule.Show();
            this.Hide();
        }
    }
}
