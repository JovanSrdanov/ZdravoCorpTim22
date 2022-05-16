using Controller;
using Model;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
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
            ReportReviewController.Instance.Load();
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
            //dodao
            DoctorSpecializationController.Instance.Load();
            DoctorController.Instance.Load();
            RequestForAbsenceController.Instance.Load();
            //dodao
            EquipmentDataController.Instance.Load();
            EquipmentController.Instance.Load();
            MedicalAppointmentController.Instance.Load();
            ManagerController.Instance.Load();
            SecretaryController.Instance.Load();
            RenovationController.Instance.Load();
            RoomMergeController.Instance.Load();
            ApprovalController.Instance.Load();
            
            AuthenticationController.Instance.Load();

            ThreadPool.QueueUserWorkItem(DaemonThread);
        }

        public void DaemonThread(object stateInfo)
        {
            while (true)
            {
                Thread.Sleep(1000);
                RenovationController.Instance.BackgroundTask();
                EquipmentRelocationController.Instance.BackgroundTask();
                PatientController.Instance.TherapyNotification();
                RoomMergeController.Instance.BackgroundTask();
                RoomDivergeController.Instance.BackgroundTask();
            }
        }

        private void Clear()
        {
            EmailInput.Text = "";
            PasswordInput.Password = "";
            ErrorTextBlock.Text = "";
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Window window = null;
            string email = EmailInput.Text;
            string password = PasswordInput.Password;

            User user = AuthenticationController.Instance.Login(email, password);
            if (user == null)
            {
                ErrorTextBlock.Text = "Login failed";
                return;
            }

            if(user.GetType() == typeof(ManagerClass))
            {
                window = new ManagerHome((ManagerClass)user);
            }
            else if (user.GetType() == typeof(SecretaryClass))
            {
                window = new SecretaryHome((SecretaryClass)user);
            }
            else if(user.GetType() == typeof(Doctor))
            {
                window = new DoctorHome((Doctor)user);
            }
            else if(user.GetType() == typeof(Patient))
            {
                Patient patientCheck = (Patient)user;
                if (patientCheck.Blocked)
                {
                    ErrorTextBlock.Text = "Korisnik je blokiran";
                    return;
                }

                window = new ZdravoCorpTabs((Patient)user);
            }

            if (window != null)
            {
                window.Show();
                Hide();
                Clear();
            }
            else
            {
                ErrorTextBlock.Text = "Login failed";
            }
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void EmailInput_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            ErrorTextBlock.Text = "";
        }

        private void PasswordInput_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";
        }
    }
}
