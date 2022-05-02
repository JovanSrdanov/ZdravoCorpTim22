using Model;
using System;
using System.Windows;

public enum AppointemntPriorityEnum
{
    Doctor = 0,
    Time
}

public struct AppointmentPreferences
{
    public Doctor enteredDoctor;
    public Patient enteredPatient;
    public DateTime enteredDateTime;
    public AppointmentType enteredAppointmentType;
    public AppointemntPriorityEnum enteredPriority;
}

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryChooseAppointment.xaml
    /// </summary>
    public partial class SecretaryChooseAppointment : Window
    {
        AppointmentPreferences appointmentPreferences;
        public SecretaryChooseAppointment(AppointmentPreferences appointmentPreferences)
        {
            InitializeComponent();
            this.appointmentPreferences = appointmentPreferences;
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
