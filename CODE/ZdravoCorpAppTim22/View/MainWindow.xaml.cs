using Controller;
using System.ComponentModel;
using System.Diagnostics;
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
            HospitalReviewController.Instance.Load();
            AddressController.Instance.Load();
            RoomController.Instance.Load();
            EquipmentRelocationController.Instance.Load();
            SecretaryController.Instance.Load();
            ManagerController.Instance.Load();
            
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
                RoomDivergeController.Instance.BackgroundTask();
            }
        }

        

        private void ManagerBtn_Click(object sender, RoutedEventArgs e)
        {
            ManagerHome managerHome = new ManagerHome();
            managerHome.Show();
            Hide();
        }

        private void SecretaryBtn_Click(object sender, RoutedEventArgs e)
        {

            SecretaryHome secretaryHome = new SecretaryHome();
            secretaryHome.Show();
            Hide();
        }

        private void DoctorBtn_Click(object sender, RoutedEventArgs e)
        {
            DoctorHome doctorHome = new DoctorHome();
            doctorHome.Show();
            Hide();
        }

        private void PatientBtn_Click(object sender, RoutedEventArgs e)
        {
            PatientSelectionForTemporaryPurpose patientSelectionForTemporaryPurpose = new PatientSelectionForTemporaryPurpose();
            patientSelectionForTemporaryPurpose.Show();
            Close();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = null;
            string email = EmailInput.Text;
            string password = PasswordInput.Password;



            switch (email)
            {
                case "m":
                    window = new ManagerHome();
                    break;
                case "p": 
                    window = new PatientSelectionForTemporaryPurpose();
                    break;
                case "d":
                    window = new DoctorHome();
                    break;
                case "s":
                    window = new SecretaryHome();
                    break;
            }
            if(window != null)
            {
                window.Show();
                Hide();
            }
        }
    }
}
