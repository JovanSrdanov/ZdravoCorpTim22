using Model;
using System.Windows;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class DoctorHome : Window
    {
        public DoctorViewModel DoctorViewModel;
        public static int selectedDoctorId;
        public static DoctorHome doctorHome;

        public DoctorHome()
        {
            InitializeComponent();
            DoctorViewModel = new DoctorViewModel();
            DataContext = DoctorViewModel;
            SelectDoctorCBOX.ItemsSource = DoctorViewModel.DoctorList;
            SelectDoctorCBOX.SelectedValuePath = "Id";
            SelectDoctorCBOX.SelectedValue = 0;
            //SelectDoctorCBOX.SelectedIndex = 0;
        }

        private void NextButton_Click(object sender, RoutedEventArgs e)
        {
            Doctor doctor = SelectDoctorCBOX.SelectedItem as Doctor;
            selectedDoctorId = doctor.Id;
            doctorHome = this;

            DoctorHomeScreen doctorHomeScreen = new DoctorHomeScreen();
            doctorHomeScreen.Owner = this;
            doctorHomeScreen.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            doctorHomeScreen.Show();

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
