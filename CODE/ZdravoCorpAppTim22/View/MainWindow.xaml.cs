using Controller;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.View.DoctorView;
using ZdravoCorpAppTim22.View.Manager;
using ZdravoCorpAppTim22.View.PatientView;
using ZdravoCorpAppTim22.View.Secretary;

namespace ZdravoCorpAppTim22
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public object Doc { get; private set; }

        public MainWindow()
        {
            var ac = AddressController.Instance.GetAll();
            var rc = RoomController.Instance.GetAllRooms();
            var sc = SecretaryController.Instance.GetAll();
            var mc = ManagerController.Instance.GetAll();
            var pc = PatientController.Instance.GetAll();
            var doctor = DoctorController.Instance.GetAll();
            var ec = EquipmentController.Instance.GetAllEquipment();
            var mm = MedicalAppointmentController.Instance.getAll();
            var man = ManagerController.Instance.GetAll();
            var sec = SecretaryController.Instance.GetAll();



            InitializeComponent();
        }

        private void ManagerBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome managerHome = new ManagerHome();
            managerHome.Show();
            this.Close();
        }



        private void SecretaryBtn_Click(object sender, RoutedEventArgs e)
        {

            SecretaryHome secretaryHome = new SecretaryHome(this);
            secretaryHome.Show();
            this.Hide();
        }

        private void DoctorBtn_Click(object sender, RoutedEventArgs e)
        {
            DoctorHome doctorHome = new DoctorHome();
            doctorHome.Show();
            this.Close();

        }

        private void PatientBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientSelectionForTemporaryPurpose patientSelectionForTemporaryPurpose = new PatientSelectionForTemporaryPurpose();
            patientSelectionForTemporaryPurpose.Show();
            this.Close();
        }
    }
}
