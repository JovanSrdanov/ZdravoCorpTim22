using Controller;
using Model;
using System.Windows;
namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryEmergency.xaml
    /// </summary>
    public partial class SecretaryEmergency : Window
    {
        private SecretaryHome SecretaryHome;

        public SecretaryEmergency(SecretaryHome secretaryHome)
        {
            InitializeComponent();
            SecretaryHome = secretaryHome;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "")
            {
                MessageBox.Show("Must enter name!");
                return;
            }
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Cancel new account", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:

                    break;
                case MessageBoxResult.No:
                    return;
            }
            Patient patient = new Patient();
            patient.Name = NameTextBox.Text;
            if ((bool)MaleRB.IsChecked)
            {
                patient.Gender = Gender.male;
            }
            else if ((bool)FemaleRB.IsChecked)
            {
                patient.Gender = Gender.female;
            }
            else
            {
                patient.Gender = Gender.other;
            }

            PatientController.Instance.Create(patient);
            SecretaryHome.Show();
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Cancel new account", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:

                    SecretaryHome.Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }
    }
}
