using Controller;
using Model;
using System.Windows;
namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryEmergencyChangeSchedule.xaml
    /// </summary>
    public partial class SecretaryEmergencyChangeSchedule : Window
    {
        SecretaryEmergency SecretaryEmergency;
        AppointmentPreferences AppointmentPreferences;
        public SecretaryEmergencyChangeSchedule(SecretaryEmergency secretaryEmergency, AppointmentPreferences appointmentPreferences)
        {
            InitializeComponent();
            SecretaryEmergency = secretaryEmergency;
            AppointmentPreferences = appointmentPreferences;
            dataGridSuggestedMedicalAppointments.ItemsSource = MedicalAppointmentController.Instance.GetUnavailableMedicalAppointmentsInNextHour(AppointmentPreferences);
            if (dataGridSuggestedMedicalAppointments.Items.Count == 0)
            {
                MessageBox.Show("No appointments in next hour that can be moved!" +
                    "Our prayers are with you!");
                this.Close();
            }
        }

        private void btnMoveThisAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSuggestedMedicalAppointments.SelectedItem == null)
            {
                MessageBox.Show("Must select appointment from table!");
            }
            else
            {
                SecretaryEmergencyAvailableAppointments secretaryEmergencyAvailableAppointments = new SecretaryEmergencyAvailableAppointments((MedicalAppointment)dataGridSuggestedMedicalAppointments.SelectedItem, this);
                secretaryEmergencyAvailableAppointments.ShowDialog();
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            SecretaryEmergency.TryToSchedule();
        }
    }
}
