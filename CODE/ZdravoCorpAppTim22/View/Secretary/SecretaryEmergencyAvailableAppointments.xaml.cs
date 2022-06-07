using Controller;
using Model;
using System.Windows;
using ZdravoCorpAppTim22.DTO;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryEmergencyAvailableAppointments.xaml
    /// </summary>
    public partial class SecretaryEmergencyAvailableAppointments : Window
    {
        MedicalAppointment MedicalAppointment;
        SecretaryEmergencyChangeSchedule SecretaryEmergencyChangeSchedule;
        public SecretaryEmergencyAvailableAppointments(MedicalAppointment medicalAppointment, SecretaryEmergencyChangeSchedule secretaryEmergencyChangeSchedule)
        {
            InitializeComponent();
            SecretaryEmergencyChangeSchedule = secretaryEmergencyChangeSchedule;
            MedicalAppointment = medicalAppointment;
            AppointmentPreferences appointmentPreferences = new AppointmentPreferences();
            appointmentPreferences.enteredDateTime = System.DateTime.Now;
            appointmentPreferences.enteredPatient = medicalAppointment.patient;
            appointmentPreferences.enteredDoctor = medicalAppointment.Doctor;
            appointmentPreferences.enteredPriority = AppointemntPriorityEnum.Time;
            appointmentPreferences.enteredAppointmentType = medicalAppointment.Type;
            dataGridSuggestedMedicalAppointments.ItemsSource = MedicalAppointmentController.Instance.GetSuggestedMedicalAppointments(appointmentPreferences);
            if (MedicalAppointmentController.Instance.GetSuggestedMedicalAppointments(appointmentPreferences).Count == 0)
            {
                MessageBox.Show("No available appointments!");
                this.Close();
            }
        }

        private void btnMoveToThisAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSuggestedMedicalAppointments.SelectedItem == null)
            {
                MessageBox.Show("Must choose appointment from list!");
            }
            else
            {
                MedicalAppointment.Interval = ((MedicalAppointmentDTOforSuggestions)dataGridSuggestedMedicalAppointments.SelectedItem).Interval;
                MedicalAppointment.doctor = ((MedicalAppointmentDTOforSuggestions)dataGridSuggestedMedicalAppointments.SelectedItem).Doctor;
                MedicalAppointment.Room = ((MedicalAppointmentDTOforSuggestions)dataGridSuggestedMedicalAppointments.SelectedItem).Room;
                MedicalAppointmentController.Instance.Update(MedicalAppointment);
                MessageBox.Show("Appointment moved to: " + MedicalAppointment.Interval.Start + "\n Doctor: " + MedicalAppointment.Doctor + "\n Room:" + MedicalAppointment.room);
                SecretaryEmergencyChangeSchedule.Close();
                this.Close();
            }
        }
    }
}
