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
    }
}
