using Controller;
using Model;
using System;
using System.Windows;

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretarySchedule.xaml
    /// </summary>
    public partial class SecretarySchedule : Window
    {
        private SecretaryHome SecretaryHome;
        public SecretarySchedule(SecretaryHome secretaryHome)
        {
            InitializeComponent();
            SecretaryHome = secretaryHome;
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
            SecretaryHome.Show();
            this.Close();
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            SecretaryHome.Show();
        }
    }
}
