using Model;
using System.Windows;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class DoctorHome : Window
    {
        public DoctorViewModel DoctorViewModel;

        public DoctorHome()
        {
            InitializeComponent();
            DoctorViewModel = new DoctorViewModel();
            DataContext = DoctorViewModel;
            SelectDoctorCBOX.ItemsSource = DoctorViewModel.DoctorList;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Doctor doctor = SelectDoctorCBOX.SelectedItem as Doctor;
            int selectedDoctorId = doctor.Id;

            DoctorAppointments doctorAppointments = new DoctorAppointments(selectedDoctorId, this);
            doctorAppointments.Owner = this;
            doctorAppointments.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            doctorAppointments.Show();

            this.Hide();
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void DoctorHomeClose(object sender, System.EventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }
    }
}
