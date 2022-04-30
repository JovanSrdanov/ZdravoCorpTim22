using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class MedicalRecordView : Window
    {
        public static Patient selectedPatient;
        private DoctorAppointments doctorAppointments;
        private MedicalRecordsScreen medicalRecordsScreen;

        public static ObservableCollection<MedicalReport> medRepList { get; set; }

        public static List<MedicalReport> newlyCreatedReports;                //ako idem na back svi kreirani izvestaji i dijagnoze se brisu
        public static List<string> newlyCreatedDiagnosis;

        public int canCreateReport;        //ako doktor iz rasporeda gleda karton prosledjuje -1, u ostalim slucajevima moze proslediti bilo koji drugi broj
        public MedicalRecordView(int canCreateReport, int selectedPatientID, DoctorAppointments doctorAppointments = null, 
            MedicalRecordsScreen medicalRecordsScreen = null)
        {
            InitializeComponent();

            newlyCreatedReports = new List<MedicalReport>();
            newlyCreatedDiagnosis = new List<string>();


            selectedPatient = PatientController.Instance.GetByID(selectedPatientID);
            this.canCreateReport = canCreateReport;
            if (canCreateReport == -1)                                                
            {
                CreateReportBtn.IsEnabled = false;
                CreateReportBtn.Foreground = new SolidColorBrush(Colors.Black);
                FinishReportBtn.Visibility = Visibility.Hidden;
            }

            //int medRecID = MedicalRecordController.Instance.GetAll().FindIndex(r => r.Patient.Id == selectedPatientID);
            MedicalRecord medRec = MedicalRecordController.Instance.GetAll().Where(r => r.Patient.Id == selectedPatientID).FirstOrDefault();

            if (medRec == null)     //pacijent nema medicinskki karton
            {
                MedicalRecord newMedRecord = new MedicalRecord(-1, BloodType.A_PLUS, selectedPatient,
                    new ObservableCollection<String>(), new ObservableCollection<String>());
                MedicalRecordController.Instance.Create(newMedRecord);
            }

            //selectedPatient.medicalRecord = MedicalRecordController.Instance.GetByID(MedicalRecordController.Instance.GetAll().FindIndex(r => r.Patient.Id == selectedPatientID));
            selectedPatient.medicalRecord = medRec;


            this.doctorAppointments = doctorAppointments;
            this.medicalRecordsScreen = medicalRecordsScreen;

            NameSurnameBlock.Text = selectedPatient.Name + " " + selectedPatient.Surname;
            GenderBlock.Text = selectedPatient.Gender.ToString();
            DateBirthBlock.Text = selectedPatient.Birthday.Date.ToShortDateString();
            JMBGBlock.Text = selectedPatient.Jmbg;

            ProblemsListBox.ItemsSource = selectedPatient.medicalRecord.ConditionList;

            AllergiesListBox.ItemsSource = selectedPatient.medicalRecord.AllergiesList;

            medRepList = new ObservableCollection<MedicalReport>(selectedPatient.medicalRecord.MedicalReport);
            PastReportsListBox.ItemsSource = medRepList; 
            //MedicationsListBox.ItemsSource = selectedPatient.medicalRecord.medicalReport.
        }

        private void MedRecClosed(object sender, EventArgs e)
        {
            //CancelAndCloseRemove();
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void CancelAndCloseRemove()
        {
            if (newlyCreatedReports.Count > 0)
            {
                foreach (MedicalReport medicalReport in newlyCreatedReports)
                {
                    MedicalReportController.Instance.DeleteByID(medicalReport.Id);
                    selectedPatient.medicalRecord.RemoveMedicalReport(medicalReport);
                }

                newlyCreatedReports.Clear();
            }

            if (newlyCreatedDiagnosis.Count > 0)
            {
                foreach (string diagnosis in newlyCreatedDiagnosis)
                {
                    selectedPatient.medicalRecord.ConditionList.Remove(diagnosis);
                }

                MedicalRecordController.Instance.Update(selectedPatient.medicalRecord);
                newlyCreatedDiagnosis.Clear();
            }
        }

        private void BackBtn(object sender, RoutedEventArgs e)
        {
            CancelAndCloseRemove();

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
                OpenReport openReport = new OpenReport(medicalReport, this, canCreateReport);
                openReport.Owner = this;
                openReport.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                openReport.Show();

                this.Hide();
            }
        }

        private void FinishReportClick(object sender, RoutedEventArgs e)
        {
            int isContained = 0;        //da li doktor vec ima karton kod sebe
            Doctor selectedDoctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);

            foreach (MedicalRecord temp in selectedDoctor.MedicalRecord)
            {
                if (temp.Id == selectedPatient.medicalRecord.Id)
                {
                    isContained = 1;
                    break;
                }
            }

            if (isContained == 0)
            {
                selectedDoctor.AddMedicalRecord(selectedPatient.medicalRecord);
            }
            
            MedicalAppointmentController.Instance.DeleteByID(DoctorAppointments.medicalAppointment.Id);
            selectedDoctor.MedicalAppointment.Remove(DoctorAppointments.medicalAppointment);
            DoctorAppointments.CurDocAppointemntsObservable.Remove(DoctorAppointments.medicalAppointment);

            DoctorController.Instance.Update(selectedDoctor);

            doctorAppointments.Show();
            this.Close();
        }
    }
}
