using Controller;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.DTO;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryEmergency.xaml
    /// </summary>
    public partial class SecretaryEmergency : Window
    {
        private SecretaryHome SecretaryHome;
        private bool Registered = false;

        public SecretaryEmergency(SecretaryHome secretaryHome, bool registered)
        {
            InitializeComponent();
            SecretaryHome = secretaryHome;

            comboBoxDoctorSpecialisation.ItemsSource = DoctorSpecializationController.Instance.GetAll();
            comboBoxExaminationType.ItemsSource = Enum.GetValues(typeof(AppointmentType));
            Registered = registered;
            if (Registered)
            {
                NameTextBox.Visibility = Visibility.Hidden;
                comboBoxPatient.Visibility = Visibility.Visible;
                comboBoxPatient.ItemsSource = PatientController.Instance.GetAll();
                MaleRB.Visibility = Visibility.Hidden;
                FemaleRB.Visibility = Visibility.Hidden;
                OtherRB.Visibility = Visibility.Hidden;
                genderLbl.Visibility = Visibility.Hidden;
            }
            else
            {
                NameTextBox.Visibility = Visibility.Visible;
                comboBoxPatient.Visibility = Visibility.Hidden;
                MaleRB.Visibility = Visibility.Visible;
                FemaleRB.Visibility = Visibility.Visible;
                OtherRB.Visibility = Visibility.Visible;
                genderLbl.Visibility = Visibility.Visible;
            }
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!Registered)
            {
                if (NameTextBox.Text == "")
                {
                    MessageBox.Show("Must enter name!");
                    return;
                }
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

            MessageBoxResult result = MessageBox.Show("Are you sure?", "Create emergency appointment", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:

                    break;
                case MessageBoxResult.No:
                    return;
            }
            Patient patient = new Patient();

            if (Registered)
            {
                if (comboBoxPatient.SelectedItem != null)
                {
                    patient = (Patient)comboBoxPatient.SelectedItem;
                }
                else
                {
                    MessageBox.Show("Must select patient from list!");
                }
            }
            else
            {

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
            }


            AppointmentPreferences appointmentPreferences = GetPrefrences();
            MedicalAppointmentDTOforSuggestions medicalAppointmentStruct = GetMedicalAppointmentStruct();
            if (medicalAppointmentStruct == null)
            {
                MessageBox.Show("NO AVAILABLE APPOINTMENTS");
                SecretaryEmergencyChangeSchedule secretaryEmergencyChangeSchedule = new SecretaryEmergencyChangeSchedule(this, appointmentPreferences);
                try
                {
                    secretaryEmergencyChangeSchedule.ShowDialog();
                }
                catch { }

                return;
            }
            else
            {
                MessageBox.Show("Start: " + medicalAppointmentStruct.Interval.Start + "\n Doctor: " + medicalAppointmentStruct.Doctor.Name + "\n Room: " + medicalAppointmentStruct.Room.Name);
            }

            MedicalAppointment medicalAppointment = new MedicalAppointment(medicalAppointmentStruct);
            if (!Registered)
            {
                PatientController.Instance.Create(patient);
            }
            medicalAppointment.Patient = patient;

            PatientController.Instance.GetPatient(patient).AddMedicalAppointment(medicalAppointment);

            MedicalAppointmentController.Instance.Create(medicalAppointment);
            SecretaryHome.Show();
            this.Close();
        }

        public void TryToSchedule()
        {
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

            AppointmentPreferences appointmentPreferences = GetPrefrences();
            MedicalAppointmentDTOforSuggestions medicalAppointmentStruct = GetMedicalAppointmentStruct();
            if (medicalAppointmentStruct == null)
            {
                return;
            }
            else
            {
                MessageBox.Show("Start: " + medicalAppointmentStruct.Interval.Start + "\n Doctor: " + medicalAppointmentStruct.Doctor.Name + "\n Room: " + medicalAppointmentStruct.Room.Name);
            }

            MedicalAppointment medicalAppointment = new MedicalAppointment(medicalAppointmentStruct);
            PatientController.Instance.Create(patient);
            medicalAppointment.Patient = patient;
            medicalAppointment.isUrgent = true;

            PatientController.Instance.GetPatient(patient).AddMedicalAppointment(medicalAppointment);

            MedicalAppointmentController.Instance.Create(medicalAppointment);
            SecretaryHome.Show();
            this.Close();
        }

        public AppointmentPreferences GetPrefrences()
        {
            AppointmentPreferences appointmentPreferencesTemp = new AppointmentPreferences();
            Doctor doctorTemp = new Doctor();
            doctorTemp.DoctorSpecialization = (DoctorSpecialization)comboBoxDoctorSpecialisation.SelectedItem;
            Patient patientTemp = new Patient();
            appointmentPreferencesTemp.enteredDoctor = doctorTemp;
            appointmentPreferencesTemp.enteredPatient = patientTemp;
            appointmentPreferencesTemp.enteredPriority = AppointemntPriorityEnum.Time;
            appointmentPreferencesTemp.enteredAppointmentType = (AppointmentType)comboBoxExaminationType.SelectedItem;
            appointmentPreferencesTemp.enteredDateTime = System.DateTime.Now;

            return appointmentPreferencesTemp;
        }

        public MedicalAppointmentDTOforSuggestions GetMedicalAppointmentStruct()
        {
            MedicalAppointmentDTOforSuggestions medicalAppointmentStructTemp = null;
            AppointmentPreferences appointmentPreferencesTemp = GetPrefrences();
            ObservableCollection<MedicalAppointmentDTOforSuggestions> tempAppointments = MedicalAppointmentController.Instance.GetSuggestedMedicalAppointments(appointmentPreferencesTemp);
            for (int i = 0; i < tempAppointments.Count; i++)
            {
                if (tempAppointments[i].Interval.Start <= System.DateTime.Now.AddHours(1) && tempAppointments[i].Doctor.DoctorSpecialization == appointmentPreferencesTemp.enteredDoctor.DoctorSpecialization)
                {

                    medicalAppointmentStructTemp = tempAppointments[i];
                    break;
                }
            }


            return medicalAppointmentStructTemp;
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
