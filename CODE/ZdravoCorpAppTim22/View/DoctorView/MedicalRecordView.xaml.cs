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
    public partial class MedicalRecordView : Window
    {
        private Patient selectedPatient;
        private DoctorAppointments doctorAppointments;
        private MedicalRecordsScreen medicalRecordsScreen;
        private int canCreateReport;        //ako doktor iz rasporeda gleda karton prosledjuje -1, u ostalim slucajevima moze proslediti bilo koji drugi broj
        public MedicalRecordView(int canCreateReport, int selectedPatientID, DoctorAppointments doctorAppointments = null, 
            MedicalRecordsScreen medicalRecordsScreen = null)
        {
            InitializeComponent();
            
            selectedPatient = PatientController.Instance.GetByID(selectedPatientID);
            this.canCreateReport = canCreateReport;
            if (canCreateReport == -1)                                                
            {
                CreateReportBtn.IsEnabled = false;
                CreateReportBtn.Foreground = new SolidColorBrush(Colors.Black);
                FinishReportBtn.Visibility = Visibility.Hidden;
            }

            //privremeni karton
            MedicalRecord medRecordTemp = new MedicalRecord(1, BloodType.B_MINUS, selectedPatient);
            selectedPatient.medicalRecord = medRecordTemp;

            this.doctorAppointments = doctorAppointments;
            this.medicalRecordsScreen = medicalRecordsScreen;

            NameSurnameBlock.Text = selectedPatient.Name + " " + selectedPatient.Surname;
            GenderBlock.Text = selectedPatient.Gender.ToString();
            DateBirthBlock.Text = selectedPatient.Birthday.Date.ToShortDateString();
            JMBGBlock.Text = selectedPatient.Jmbg;

            ProblemsListBox.ItemsSource = medRecordTemp.ConditionList;
            AllergiesListBox.ItemsSource = medRecordTemp.AllergiesList;
            PastReportsListBox.ItemsSource = selectedPatient.medicalRecord.medicalReport;
            //MedicationsListBox.ItemsSource = selectedPatient.medicalRecord.medicalReport.
        }

        private void MedRecClosed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void BackBtn(object sender, RoutedEventArgs e)
        {
            if (doctorAppointments == null)
            {
                medicalRecordsScreen.Show();
            }
            else
            {
                doctorAppointments.Show();
            }
            this.Close();
        }

        private void CreateReportBtnClick(object sender, RoutedEventArgs e)
        {
            CreateReport createReport = new CreateReport(selectedPatient, this);
            createReport.Owner = this;
            createReport.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            createReport.Show();

            this.Hide();
        }

        private void OpenReportBtnClick(object sender, RoutedEventArgs e)
        {
            MedicalReport medicalReport = PastReportsListBox.SelectedItem as MedicalReport;
            if (medicalReport == null)
            {
                MessageBox.Show("Please select a report", "Open report", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                OpenReport openReport = new OpenReport(medicalReport, this);
                openReport.Owner = this;
                openReport.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                openReport.Show();

                this.Hide();
            }
        }

        private void FinishReportClick(object sender, RoutedEventArgs e)
        {
            DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId).AddMedicalRecord(selectedPatient.medicalRecord);
            MedicalAppointmentController.Instance.DeleteByID(DoctorAppointments.medicalAppointment.Id);
            DoctorAppointments.CurDocAppointemntsObservable.Remove(DoctorAppointments.medicalAppointment);

            doctorAppointments.Show();
            this.Close();
        }
    }
}
