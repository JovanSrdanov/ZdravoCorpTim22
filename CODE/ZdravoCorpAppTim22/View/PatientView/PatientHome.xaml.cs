using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ZdravoCorpAppTim22.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientHome.xaml
    /// </summary>
    public partial class PatientHome : Window
    {

        public ObservableCollection<Patient> PatientList { get; set; }
        public List<Patient> patients;
        public PatientHome()
        {
            InitializeComponent();

            WelcomePatientLabel.Content = "Dobrodosli! Pacijent: " + PatientSelectionForTemporaryPurpose.LoggedPatient.Name;
            patients = PatientController.Instance.GetAll();
            PatientList = new ObservableCollection<Patient>(patients);
            dataGrid.ItemsSource = PatientList;


        }

        private void AddAppointmentPatient_Click(object sender, RoutedEventArgs e)
        {
            MakeAppointment makeAppointment = new MakeAppointment();
            makeAppointment.Show();
        }

        private void RemoveAppointmentPatient_Click(object sender, RoutedEventArgs e)
        {
            Patient patient = (Patient)dataGrid.SelectedItem;
            if (patient == null)
            {
                return;
            }
            MessageBox.Show("Obrisan je pacijent sa Id: " + patient.ID);
            PatientController.Instance.DeleteByID(patient.ID);
            PatientList.Remove(patient);


        }
    }
}
