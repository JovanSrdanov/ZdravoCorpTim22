using Controller;
using System.Threading;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.View.DoctorView;
using ZdravoCorpAppTim22.View.Manager;
using ZdravoCorpAppTim22.View.PatientView;
using ZdravoCorpAppTim22.View.Secretary;

namespace ZdravoCorpAppTim22
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoadData();
            InitializeComponent();
        }

        public void LoadData()
        {
            AddressController.Instance.Load();
            RoomController.Instance.Load();
            EquipmentRelocationController.Instance.Load();
            SecretaryController.Instance.Load();
            ManagerController.Instance.Load();
            
            //dodao
            MedicalRecordController.Instance.Load();
            MedicalReceiptController.Instance.Load();

            IngredientDataController.Instance.Load();
            MedicineDataController.Instance.Load();
            MedicineController.Instance.Load();
            IngredientController.Instance.Load();

            ReplacementController.Instance.Load();

            MedicalReportController.Instance.Load();
            PatientController.Instance.Load();

            DoctorController.Instance.Load();
            EquipmentDataController.Instance.Load();
            EquipmentController.Instance.Load();
            MedicalAppointmentController.Instance.Load();
            ManagerController.Instance.Load();
            SecretaryController.Instance.Load();
            RenovationController.Instance.Load();
            RoomMergeController.Instance.Load();

            ThreadPool.QueueUserWorkItem(DaemonThread);
        }

        public void DaemonThread(object stateInfo)
        {
            while (true)
            {
                Thread.Sleep(1000);
                RenovationController.Instance.BackgroundTask();
                EquipmentRelocationController.Instance.BackgroundTask();
                PatientController.Instance.DeamonMethod();
                RoomMergeController.Instance.BackgroundTask();
            }
        }

        private void ManagerBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome managerHome = new ManagerHome(this);
            managerHome.Show();
            this.Hide();
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
            doctorHome.Owner = this;
            doctorHome.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            doctorHome.Show();

            this.Hide();

        }

        private void PatientBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientSelectionForTemporaryPurpose patientSelectionForTemporaryPurpose = new PatientSelectionForTemporaryPurpose();
            patientSelectionForTemporaryPurpose.Show();
            this.Close();
        }
    }
}
