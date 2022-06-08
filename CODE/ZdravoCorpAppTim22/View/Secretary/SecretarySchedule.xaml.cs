using Controller;
using Model;
using System;
using System.Windows;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretarySchedule.xaml
    /// </summary>
    public partial class SecretarySchedule : Window
    {
        private SecretaryScheduleOptions secretaryScheduleOptions;
        public SecretarySchedule(SecretaryScheduleOptions secretaryScheduleOptions)
        {
            InitializeComponent();
            this.secretaryScheduleOptions = secretaryScheduleOptions;
            datumPiker.DisplayDateStart = System.DateTime.Today;
            comboBoxDoctor.ItemsSource = DoctorController.Instance.GetAll();
            comboBoxPatient.ItemsSource = PatientController.Instance.GetAll();
            comboBoxAppointmentType.ItemsSource = Enum.GetValues(typeof(AppointmentType));
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxAppointmentType.SelectedItem != null && comboBoxDoctor.SelectedItem != null && comboBoxPatient.SelectedItem != null && datumPiker.SelectedDate != null)
            {
                AppointmentPreferences appointmentPreferences = new AppointmentPreferences();
                //Appointment preferences
                {
                    appointmentPreferences.enteredDoctor = (Doctor)comboBoxDoctor.SelectedItem;
                    appointmentPreferences.enteredPatient = (Patient)comboBoxPatient.SelectedItem;
                    appointmentPreferences.enteredDateTime = (DateTime)datumPiker.SelectedDate;
                    appointmentPreferences.enteredAppointmentType = (AppointmentType)comboBoxAppointmentType.SelectedItem;
                    if (TimeRadioButton.IsChecked == true)
                    {
                        appointmentPreferences.enteredPriority = AppointemntPriorityEnum.Time;
                    }
                    else
                    {
                        appointmentPreferences.enteredPriority = AppointemntPriorityEnum.Doctor;
                    }
                }

                SecretaryChooseAppointment secretaryChooseAppointment = new SecretaryChooseAppointment(appointmentPreferences);
                secretaryChooseAppointment.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sva polja moraju biti popunjena!");
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            secretaryScheduleOptions.Show();
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            secretaryScheduleOptions.Show();
        }
    }
}
