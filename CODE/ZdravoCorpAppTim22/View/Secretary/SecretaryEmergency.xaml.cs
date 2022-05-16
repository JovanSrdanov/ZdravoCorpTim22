using Controller;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
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

            comboBoxDoctorSpecialisation.ItemsSource = DoctorSpecializationController.Instance.GetAll();
            comboBoxExaminationType.ItemsSource = Enum.GetValues(typeof(AppointmentType));
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "")
            {
                MessageBox.Show("Must enter name!");
                return;
            }

            if (comboBoxDoctorSpecialisation.SelectedItem == null)
            {
                MessageBox.Show("Must enter doctor specialisation!");
                return;
            }

            if (comboBoxExaminationType.SelectedItem == null)
            {
                MessageBox.Show("Must enter examination type!");
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

            AppointmentPreferences appointmentPreferences = new AppointmentPreferences();
            Doctor doctorTemp = new Doctor();
            doctorTemp.DoctorSpecialization = (DoctorSpecialization)comboBoxDoctorSpecialisation.SelectedItem;
            Patient patientTemp = new Patient();
            appointmentPreferences.enteredDoctor = doctorTemp;
            appointmentPreferences.enteredPatient = patientTemp;
            appointmentPreferences.enteredPriority = AppointemntPriorityEnum.Time;
            appointmentPreferences.enteredAppointmentType = (AppointmentType)comboBoxExaminationType.SelectedItem;
            appointmentPreferences.enteredDateTime = System.DateTime.Now;
            MedicalAppointmentStruct medicalAppointmentStruct = null;
            ObservableCollection<MedicalAppointmentStruct> tempAppointments = MedicalAppointmentController.Instance.GetSuggestedMedicalAppointments(appointmentPreferences);
            for (int i = 0; i < tempAppointments.Count; i++)
            {
                if (tempAppointments[i].Interval.Start <= System.DateTime.Now.AddHours(1) && tempAppointments[i].Doctor.DoctorSpecialization == appointmentPreferences.enteredDoctor.DoctorSpecialization)
                {

                    medicalAppointmentStruct = tempAppointments[i];
                    break;
                }
            }

            if (medicalAppointmentStruct == null)
            {
                MessageBox.Show("NO APPOINTMENTS");
                return;
            }
            else
            {
                MessageBox.Show("Start: " + medicalAppointmentStruct.Interval.Start + "\n Doctor: " + medicalAppointmentStruct.Doctor.Name + "\n Room: " + medicalAppointmentStruct.Room.Name);
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
