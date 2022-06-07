using Model;
using System.Windows;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class DoctorHomeScreen : Window
    {
        public static DoctorHomeScreen doctorHomeScreen;
        public static Doctor LoggedInDoctor { get; set; }
        public DoctorHomeScreen(Doctor loggedInDoctor)
        {
            InitializeComponent();
            doctorHomeScreen = this;
            LoggedInDoctor = loggedInDoctor;
        }
        private void ScheduleListBtn(object sender, RoutedEventArgs e)
        {
            DoctorAppointments doctorAppointments = new DoctorAppointments(this);
            doctorAppointments.Owner = this;
            doctorAppointments.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            doctorAppointments.Show();
            this.Hide();
        }

        private void ViewMedRecordsClick(object sender, RoutedEventArgs e)
        {
            MedicalRecordsScreen medicalRecordsScreen = new MedicalRecordsScreen(this);
            medicalRecordsScreen.Owner = this;
            medicalRecordsScreen.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            medicalRecordsScreen.Show();

            this.Hide();
        }

        private void RequestForAbsenceBtnClick(object sender, RoutedEventArgs e)
        {
            RequestForLeaveView requestForLeaveView = new RequestForLeaveView();
            requestForLeaveView.Owner = this;
            requestForLeaveView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            requestForLeaveView.Show();
            this.Hide();
        }

        private void DrugsBtnClick(object sender, RoutedEventArgs e)
        {
            DrugsView drugsView = new DrugsView();
            drugsView.Owner = this;
            drugsView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            drugsView.Show();

            this.Hide();
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
    }
}
