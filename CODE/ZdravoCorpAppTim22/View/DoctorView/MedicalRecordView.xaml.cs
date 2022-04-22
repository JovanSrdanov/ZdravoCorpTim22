using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    /// <summary>
    /// Interaction logic for MedicalRecordView.xaml
    /// </summary>
    public partial class MedicalRecordView : Window
    {
        private Patient selectedPatient;
        private DoctorAppointments doctorAppointments;
        public MedicalRecordView(int selectedPatientID, DoctorAppointments doctorAppointments)
        {
            InitializeComponent();
            selectedPatient = PatientController.Instance.GetByID(selectedPatientID);
            this.doctorAppointments = doctorAppointments;

            NameSurnameBlock.Text = selectedPatient.Name + " " + selectedPatient.Surname;
            GenderBlock.Text = selectedPatient.Gender.ToString();
            DateBirthBlock.Text = selectedPatient.Birthday.Date.ToShortDateString();
            JMBGBlock.Text = selectedPatient.Jmbg;

        }

        private void MedRecClosed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void BackBtn(object sender, RoutedEventArgs e)
        {
            doctorAppointments.Show();
            this.Close();
        }
    }
}
