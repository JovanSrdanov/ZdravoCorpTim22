using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class MedicalRecordView : Window
    {
        public static Patient selectedPatient;
        private DoctorAppointments doctorAppointments;
        private MedicalRecordsScreen medicalRecordsScreen;

        public static ObservableCollection<MedicalReport> medRepList { get; set; }
        public static ObservableCollection<Medicine> medicineObservableList { get; set; }

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

            MedicalRecord medRec = selectedPatient.MedicalRecord;

            if (medRec == null)     //pacijent nema medicinskki karton
            {
                medRec = new MedicalRecord(-1, BloodType.A_PLUS, selectedPatient,
                    new ObservableCollection<String>(), new ObservableCollection<String>());
                MedicalRecordController.Instance.Create(medRec);
                selectedPatient.MedicalRecord = medRec;
            }

            this.doctorAppointments = doctorAppointments;
            this.medicalRecordsScreen = medicalRecordsScreen;

            NameSurnameBlock.Text = selectedPatient.Name + " " + selectedPatient.Surname;
            GenderBlock.Text = selectedPatient.Gender.ToString();
            DateBirthBlock.Text = selectedPatient.Birthday.Date.ToShortDateString();
            JMBGBlock.Text = selectedPatient.Jmbg;

            ProblemsListBox.ItemsSource = selectedPatient.MedicalRecord.ConditionList;

            AllergiesListBox.ItemsSource = selectedPatient.MedicalRecord.AllergiesList;
            medicineObservableList = new ObservableCollection<Medicine>();
            AllergiesListBox.ItemsSource = selectedPatient.medicalRecord.AllergiesList;

            List<Medicine> medicines = new List<Medicine>();

            foreach (MedicalReceipt medicalReceipt in selectedPatient.MedicalRecord.MedicalReceipt)
            {
                medicines.AddRange(medicalReceipt.Medicine);
            }
            medicineObservableList = new ObservableCollection<Medicine>(medicines);
            MedicationsListBox.ItemsSource = medicineObservableList;

            medRepList = new ObservableCollection<MedicalReport>(selectedPatient.MedicalRecord.MedicalReport);
            PastReportsListBox.ItemsSource = medRepList; 
        }

        private void CancelAndCloseRemove()
        {
            if (newlyCreatedReports.Count > 0)
            {
                foreach (MedicalReport medicalReport in newlyCreatedReports)
                {
                    foreach(Medicine medicine in medicalReport.MedicalReceipt.Medicine)
                    {
                        medicineObservableList.Remove(medicine);
                    }
                    selectedPatient.medicalRecord.MedicalReceipt.Remove(medicalReport.MedicalReceipt);
                    selectedPatient.medicalRecord.RemoveMedicalReport(medicalReport);
                    MedicalReceiptController.Instance.DeleteByID(medicalReport.MedicalReceipt.Id);
                    MedicalReportController.Instance.DeleteByID(medicalReport.Id);     
                }

                newlyCreatedReports.Clear();
            }

            if (newlyCreatedDiagnosis.Count > 0)
            {
                foreach (string diagnosis in newlyCreatedDiagnosis)
                {
                    selectedPatient.MedicalRecord.ConditionList.Remove(diagnosis);
                }

                MedicalRecordController.Instance.Update(selectedPatient.MedicalRecord);
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
            PatientController.Instance.Update(selectedPatient);
            int isContained = 0;        //da li doktor vec ima karton kod sebe
            Doctor selectedDoctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);

            foreach (MedicalRecord temp in selectedDoctor.MedicalRecord)
            {
                if (temp.Id == selectedPatient.MedicalRecord.Id)
                {
                    isContained = 1;
                    break;
                }
            }

            if (isContained == 0)
            {
                selectedDoctor.AddMedicalRecord(selectedPatient.MedicalRecord);
            }
            
            MedicalAppointmentController.Instance.DeleteByID(DoctorAppointments.medicalAppointment.Id);
            selectedDoctor.MedicalAppointment.Remove(DoctorAppointments.medicalAppointment);
            DoctorAppointments.CurDocAppointemntsObservable.Remove(DoctorAppointments.medicalAppointment);

            DoctorController.Instance.Update(selectedDoctor);
         
            doctorAppointments.Show();
            this.Close();
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            DoctorHome.doctorHome.Show();
            this.Close();
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}
