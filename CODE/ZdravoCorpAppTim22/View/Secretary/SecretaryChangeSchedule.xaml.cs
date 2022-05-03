using Controller;
using Model;
using System.Windows;

namespace ZdravoCorpAppTim22.View.Secretary
{
    /// <summary>
    /// Interaction logic for SecretaryChangeSchedule.xaml
    /// </summary>
    public partial class SecretaryChangeSchedule : Window
    {
        SecretaryScheduleOptions secretaryOptions;
        public SecretaryChangeSchedule(SecretaryScheduleOptions secretaryOptions)
        {
            InitializeComponent();
            this.secretaryOptions = secretaryOptions;
            PatientComboBox.ItemsSource = PatientController.Instance.GetAll();
        }

        private void PatientComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            dataGridMedicalAppointments.ItemsSource = ((Patient)PatientComboBox.SelectedItem).MedicalAppointment;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMedicalAppointments.SelectedItem != null)
            {
                MedicalAppointment medicalAppointment = null;
                try
                {
                    medicalAppointment = (MedicalAppointment)dataGridMedicalAppointments.SelectedItem;
                }
                catch
                {
                    MessageBox.Show("Select appointment to edit");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure?", "Edit this appointment", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        AppointmentPreferences appointmentPreferences = new AppointmentPreferences();
                        {
                            appointmentPreferences.enteredAppointmentType = medicalAppointment.Type;
                            appointmentPreferences.enteredDateTime = medicalAppointment.Interval.Start.Date;
                            appointmentPreferences.enteredDoctor = medicalAppointment.doctor;
                            appointmentPreferences.enteredPatient = medicalAppointment.patient;
                            appointmentPreferences.enteredPriority = AppointemntPriorityEnum.Time;
                        }

                        MedicalAppointmentController.Instance.DeleteByID(medicalAppointment.Id);
                        RefreshTable();
                        SecretaryChooseAppointment secretaryChooseAppointment = new SecretaryChooseAppointment(appointmentPreferences);
                        secretaryChooseAppointment.ShowDialog();
                        RefreshTable();
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }
            else
            {
                MessageBox.Show("Select appointment to edit");
            }
        }

        private void RefreshTable()
        {
            dataGridMedicalAppointments.ItemsSource = null;
            if (PatientComboBox.SelectedItem != null)
            {
                try
                {
                    dataGridMedicalAppointments.ItemsSource = ((Patient)PatientComboBox.SelectedItem).MedicalAppointment;
                }
                catch { }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridMedicalAppointments.SelectedItem != null)
            {
                MedicalAppointment medicalAppointment = null;
                try
                {
                    medicalAppointment = (MedicalAppointment)dataGridMedicalAppointments.SelectedItem;
                }
                catch
                {
                    MessageBox.Show("Select appointment to delete");
                    return;
                }

                MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete this appointment", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:


                        MedicalAppointmentController.Instance.DeleteByID(medicalAppointment.Id);
                        dataGridMedicalAppointments.ItemsSource = null;
                        dataGridMedicalAppointments.ItemsSource = ((Patient)PatientComboBox.SelectedItem).MedicalAppointment;

                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }
            else
            {
                MessageBox.Show("Select appointment to delete");
            }
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            secretaryOptions.Show();
        }

    }
}
