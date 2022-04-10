using System.Windows;
using ZdravoCorpAppTim22.View.Doctor;
using ZdravoCorpAppTim22.View.Manager;
using ZdravoCorpAppTim22.View.Patient;
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
            PatientHome patientHome = new PatientHome();
            patientHome.Show();
            this.Close();
        }
    }
}
