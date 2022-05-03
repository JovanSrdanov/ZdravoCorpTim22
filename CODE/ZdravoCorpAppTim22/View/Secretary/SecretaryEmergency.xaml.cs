using Controller;
using Model;
using System.Windows;
using ZdravoCorpAppTim22.Model;

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
            AppointmentPreferences appointmentPreferences = new AppointmentPreferences();
            appointmentPreferences.enteredAppointmentType = AppointmentType.Examination;
            appointmentPreferences.enteredDateTime = System.DateTime.Now;
            Doctor doctorTemp = new Doctor();
            Patient patientTemp = new Patient();
            appointmentPreferences.enteredDoctor = doctorTemp;
            appointmentPreferences.enteredPatient = patientTemp;
            appointmentPreferences.enteredPriority = AppointemntPriorityEnum.Time;
            dataGridSuggestedMedicalAppointments.ItemsSource = MedicalAppointmentController.Instance.GetSuggestedMedicalAppointments(appointmentPreferences);
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointmentStruct medicalAppointmentStruct = null;
            if (NameTextBox.Text == "")
            {
                MessageBox.Show("Must enter name!");
                return;
            }

            if (dataGridSuggestedMedicalAppointments.SelectedItem != null)
            {
                try
                {
                    medicalAppointmentStruct = (MedicalAppointmentStruct)dataGridSuggestedMedicalAppointments.SelectedItem;
                }
                catch
                {
                    MessageBox.Show("Select appointment for emergencyy");
                    return;
                }
            }
            else
            {
                MessageBox.Show("Select appointment for emergency");
                return;
            }

            MessageBoxResult result = MessageBox.Show("Are you sure?", "Create emergency account", MessageBoxButton.YesNo);
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

            MedicalAppointment medicalAppointment = new MedicalAppointment(medicalAppointmentStruct);
            PatientController.Instance.Create(patient);
            medicalAppointment.Patient = patient;

            PatientController.Instance.GetPatient(patient).AddMedicalAppointment(medicalAppointment);

            MedicalAppointmentController.Instance.Create(medicalAppointment);
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

        private void Window_Closed(object sender, System.EventArgs e)
        {
            SecretaryHome.Show();
        }
    }
}
