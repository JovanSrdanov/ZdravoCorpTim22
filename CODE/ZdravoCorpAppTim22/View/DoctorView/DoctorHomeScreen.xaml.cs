using System;
using System.Windows;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    /// <summary>
    /// Interaction logic for DoctorHomeScreen.xaml
    /// </summary>
    public partial class DoctorHomeScreen : Window
    {
        public DoctorHomeScreen()
        {
            InitializeComponent();
        }
        private void ScheduleListBtn(object sender, RoutedEventArgs e)
        {
            DoctorAppointments doctorAppointments = new DoctorAppointments(this);
            doctorAppointments.Owner = this;
            doctorAppointments.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            doctorAppointments.Show();
            this.Hide();
        }

        private void DoctorHomeScreenClose(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            DoctorHome.doctorHome.Show();
            this.Close();
        }
    }
}
