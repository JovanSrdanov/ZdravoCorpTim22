using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class DoctorAppointments : Window
    {
        public static ObservableCollection<MedicalAppointment> CurDocAppointemntsObservable { get; set; }
        public int selectedDoctorId;
        private DoctorHome doctorHome;

        public DoctorAppointments(int id, DoctorHome doctorHome)
        {
            InitializeComponent();
            selectedDoctorId = id;

            Doctor doctor = DoctorController.Instance.GetByID(id);
            List<MedicalAppointment> allMedicalAppointment = doctor.MedicalAppointment;
            CurDocAppointemntsObservable = new ObservableCollection<MedicalAppointment>(allMedicalAppointment);
            appointmentListGrid.ItemsSource = CurDocAppointemntsObservable;

            this.doctorHome = doctorHome;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            DoctorAppointmentCreate appCreate = new DoctorAppointmentCreate(selectedDoctorId, this);
            appCreate.Owner = this;
            appCreate.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            appCreate.Show();

            this.Hide();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MedicalAppointment medicalAppointment = (MedicalAppointment)appointmentListGrid.SelectedItem;
            if (medicalAppointment == null)
            {
                MessageBox.Show("Please select an appointment", "Delete appointment", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MedicalAppointmentController.Instance.DeleteByID(medicalAppointment.Id);
            CurDocAppointemntsObservable.Remove(medicalAppointment);
            MessageBox.Show("Apointment with ID " + medicalAppointment.Id + " deleted");

        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            doctorHome.Show();
            this.Close();
        }

        private void DoctorAppointmentsClose(object sender, System.EventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}