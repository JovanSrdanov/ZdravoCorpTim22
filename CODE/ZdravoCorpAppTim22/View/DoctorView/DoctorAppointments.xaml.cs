using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class DoctorAppointments : Window
    {
        public static ObservableCollection<MedicalAppointment> CurDocAppointemntsObservable { get; set; }
        public static MedicalAppointment medicalAppointment;

        private DoctorHomeScreen doctorHomeScreen;
        private Doctor doctor;

        public DoctorAppointments(DoctorHomeScreen doctorHomeScreen)
        {
            InitializeComponent();
            doctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);
            List<MedicalAppointment> allMedicalAppointment = doctor.MedicalAppointment;
            CurDocAppointemntsObservable = new ObservableCollection<MedicalAppointment>(allMedicalAppointment);
            appointmentListGrid.ItemsSource = CurDocAppointemntsObservable;

            this.doctorHomeScreen = doctorHomeScreen;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            DoctorAppointmentCreate appCreate = new DoctorAppointmentCreate(this);
            appCreate.Owner = this;
            appCreate.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            appCreate.Show();

            this.Hide();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            medicalAppointment = (MedicalAppointment)appointmentListGrid.SelectedItem;
            if (medicalAppointment == null)
            {
                MessageBox.Show("Please select an appointment", "Delete appointment", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MedicalAppointmentController.Instance.DeleteByID(medicalAppointment.Id);
            doctor.MedicalAppointment.Remove(medicalAppointment);
            DoctorController.Instance.Update(doctor);
            CurDocAppointemntsObservable.Remove(medicalAppointment);
            MessageBox.Show("Apointment with ID " + medicalAppointment.Id + " deleted");

        }

        private void ViewRecordBtn(object sender, RoutedEventArgs e)
        {
            medicalAppointment = (MedicalAppointment)appointmentListGrid.SelectedItem;
            if (medicalAppointment == null)
            {
                MessageBox.Show("Please select an appointment", "View medical record", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //DODAO
            Patient selectedPatient = PatientController.Instance.GetByID(medicalAppointment.Patient.Id);
            if (selectedPatient.MedicalRecord == null)
            {
                MessageBox.Show("Selected patient does not have a medical record", "View record", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MedicalRecordView medicalRecordView = new MedicalRecordView(-1, medicalAppointment.Patient.Id, this);
            medicalRecordView.Owner = this;
            medicalRecordView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            medicalRecordView.Show();

            this.Hide();

        }

        private void BeginAppointmentClick(object sender, RoutedEventArgs e)
        {
            medicalAppointment = (MedicalAppointment)appointmentListGrid.SelectedItem;
            if (medicalAppointment == null)
            {
                MessageBox.Show("Please select an appointment", "View medical record", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MedicalRecordView medicalRecordView = new MedicalRecordView(1, medicalAppointment.Patient.Id, this);
            medicalRecordView.Owner = this;
            medicalRecordView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            medicalRecordView.Show();

            this.Hide();
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            doctorHomeScreen.Show();
            this.Close();
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            DoctorHome.doctorHome.Show();
            this.Close();
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}