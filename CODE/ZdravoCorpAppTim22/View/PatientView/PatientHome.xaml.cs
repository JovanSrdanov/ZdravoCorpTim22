using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZdravoCorpAppTim22.View.PatientView
{
    /// <summary>
    /// Interaction logic for PatientHome.xaml
    /// </summary>
    public partial class PatientHome : Window
    {

        public ObservableCollection<Patient> PatientList{ get; set; }
        public static PatientController patientController;
        public List<Patient> patients;
        public PatientHome()
        {
            InitializeComponent();

            WelcomePatientLabel.Content = "Dobrodosli! Pacijent: " + PatientSelectionForTemporaryPurpose.LoggedPatient.Name;

            patientController = new PatientController();
            patients = patientController.GetAll();
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
            patientController.DeleteByID(patient.ID);
            PatientList.Remove(patient);


        }
    }
}
