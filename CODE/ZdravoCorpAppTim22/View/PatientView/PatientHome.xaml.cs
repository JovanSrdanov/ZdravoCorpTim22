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

        public static ObservableCollection<MedicalAppointment> MedicalAppointmentList { get; set; }
        public List<MedicalAppointment> medicalAppointments;
        public PatientHome()
        {
            InitializeComponent();

            WelcomePatientLabel.Content = "Dobrodosli! Pacijent: " + PatientSelectionForTemporaryPurpose.LoggedPatient.Name;

            medicalAppointments = PatientController.Instance.GetByID(PatientSelectionForTemporaryPurpose.LoggedPatient.Id).MedicalAppointment;

            MedicalAppointmentList = new ObservableCollection<MedicalAppointment>(medicalAppointments);
            dataGrid.ItemsSource = MedicalAppointmentList;


        }

        private void AddAppointmentPatient_Click(object sender, RoutedEventArgs e)
        {
            MakeAppointment makeAppointment = new MakeAppointment();
            makeAppointment.Show();
        }

        private void RemoveAppointmentPatient_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointment medicalAppointmentTemp = (MedicalAppointment)dataGrid.SelectedItem;
            if (medicalAppointmentTemp == null)
            {
                return;
            }
            MedicalAppointmentController.Instance.DeleteByID(medicalAppointmentTemp.Id);
            MedicalAppointmentList.Remove(medicalAppointmentTemp);


        }
    }
}
