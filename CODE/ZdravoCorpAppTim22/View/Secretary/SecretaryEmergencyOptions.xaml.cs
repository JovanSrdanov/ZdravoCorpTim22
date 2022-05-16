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

        }

        private void btnNewAccount_Click(object sender, RoutedEventArgs e)
        {
            SecretaryEmergency secretaryEmergency = new SecretaryEmergency(SecretaryHome);
            secretaryEmergency.Show();
            this.Hide();
        }
    }
}
