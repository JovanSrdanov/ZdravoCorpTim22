using Controller;
using Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class DoctorAppointments : Window
    {
        public static ObservableCollection<MedicalAppointment> CurDocAppointemntsObservable { get; set; }
        public static MedicalAppointment medicalAppointment;

        private readonly DoctorHomeScreen _doctorHomeScreen;
        private readonly Doctor _loggedInDoctor;

        public DoctorAppointments(DoctorHomeScreen doctorHomeScreen)
        {
            InitializeComponent();
            _loggedInDoctor = DoctorController.Instance.GetByID(DoctorHomeScreen.LoggedInDoctor.Id);
            var allDoctorMedicalAppointments = _loggedInDoctor.MedicalAppointment;
            CurDocAppointemntsObservable = new ObservableCollection<MedicalAppointment>(allDoctorMedicalAppointments);
            appointmentListGrid.ItemsSource = CurDocAppointemntsObservable;

            this._doctorHomeScreen = doctorHomeScreen;
        }

        private bool isMedicalAppointmentSelected()
        {
            bool returnValue = true;
            medicalAppointment = (MedicalAppointment)appointmentListGrid.SelectedItem;
            if (medicalAppointment == null)
            {
                MessageBox.Show("Please select an appointment", "Delete appointment",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false; ;
            }
            return returnValue;
        }

        private bool patientHasMedicalRecord()
        {
            bool returnValue = true;
            var medicalRecord = PatientController.Instance.GetByID(medicalAppointment.Patient.Id).medicalRecord;
            if (medicalRecord == null)
            {
                MessageBox.Show("Selected patient does not have a medical record", "View record", 
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            return returnValue;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            var appCreate = new DoctorAppointmentCreate(this)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            appCreate.Show();

            this.Hide();
        }

        private void BeginAppointmentClick(object sender, RoutedEventArgs e)
        {
            if(!(isMedicalAppointmentSelected())) return;

            var medicalRecordView = new MedicalRecordView(1, medicalAppointment.Patient.Id, this)   //prosledjujem 1 ako doktor sme
            {                                                                                   //da napravi novi izvestaj, u suprotnom -1
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            medicalRecordView.Show();

            this.Hide();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (!(isMedicalAppointmentSelected())) return;

            MedicalAppointmentController.Instance.DeleteByID(medicalAppointment.Id);
            _loggedInDoctor.MedicalAppointment.Remove(medicalAppointment);
            DoctorController.Instance.Update(_loggedInDoctor);
            CurDocAppointemntsObservable.Remove(medicalAppointment);
            MessageBox.Show("Appointment with ID " + medicalAppointment.Id + " deleted");

        }

        private void ViewRecordBtn(object sender, RoutedEventArgs e)
        {
            if (!(isMedicalAppointmentSelected())) return;
            if(!(patientHasMedicalRecord())) return;

            var medicalRecordView = new MedicalRecordView(-1, medicalAppointment.Patient.Id, this)
            {
                Owner = this,
                WindowStartupLocation = WindowStartupLocation.CenterOwner
            };
            medicalRecordView.Show();

            this.Hide();

        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            _doctorHomeScreen.Show();
            this.Close();
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
            foreach (Window item in App.Current.Windows)
            {
                if (item != Application.Current.MainWindow)
                {
                    item.Close();
                }
            }
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}