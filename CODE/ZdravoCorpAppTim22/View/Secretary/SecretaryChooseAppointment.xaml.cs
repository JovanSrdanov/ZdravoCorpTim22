using Controller;
using Model;
using System;
using System.Windows;
using ZdravoCorpAppTim22.DTO;
using ZdravoCorpAppTim22.Model;

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

            dataGridSuggestedMedicalAppointments.ItemsSource = MedicalAppointmentController.Instance.GetSuggestedMedicalAppointments(this.appointmentPreferences);
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridSuggestedMedicalAppointments.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Edit this appointment", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MedicalAppointment medicalAppointmentTemp = new MedicalAppointment((MedicalAppointmentDTOforSuggestions)dataGridSuggestedMedicalAppointments.SelectedItem);
                        MedicalAppointmentController.Instance.Create(medicalAppointmentTemp);
                        this.Close();
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }
            else
            {
                MessageBox.Show("Must select appointment from list!");
            }
        }
    }
}
