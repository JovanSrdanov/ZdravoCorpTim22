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
        private DoctorAppointments doctorAppointmentsView;
        private MedicalRecordsScreen medicalRecordsScreenView;

        public static ObservableCollection<MedicalReport> selectedPatientReportHistory { get; set; }
        public static ObservableCollection<MedicineData> selectedPatientMedicineHistory { get; set; } 

        public static List<MedicalReport> newlyCreatedReports;           
        public static List<string> newlyCreatedDiagnosis;
        public static List<MedicalAppointment> newlyCreatedAppointments;

        public int canCreateReport;     //ako doktor iz rasporeda gleda karton prosledjuje -1, u ostalim slucajevima moze proslediti bilo koji drugi broj
        public MedicalRecordView(int canCreateReport, int selectedPatientID, DoctorAppointments doctorAppointmentsView = null, 
            MedicalRecordsScreen medicalRecordsScreenView = null)
        {
            InitializeComponent();

            newlyCreatedReports = new List<MedicalReport>();
            newlyCreatedDiagnosis = new List<string>();
            newlyCreatedAppointments = new List<MedicalAppointment>();
            this.doctorAppointmentsView = doctorAppointmentsView;
            this.medicalRecordsScreenView = medicalRecordsScreenView;
            selectedPatient = PatientController.Instance.GetByID(selectedPatientID);
            this.canCreateReport = canCreateReport;

            PatientController.Instance.checkIfPatientHasMedicalRecord(selectedPatient);
            List<Medicine> patientMedicine = getSelectedPatientMedicine();
            selectedPatientMedicineHistory = 
                new ObservableCollection<MedicineData>(getSelectedPatientMedicineData(patientMedicine));

            checkIfCanCreateReport();
            setWPFDisplayText();

            selectedPatientReportHistory = new ObservableCollection<MedicalReport> (selectedPatient.MedicalRecord.MedicalReport);
            setItemSources();

        }
        private void checkIfCanCreateReport()
        {
            if (canCreateReport == -1)
            {
                CreateReportBtn.IsEnabled = false;
                CreateReportBtn.Foreground = new SolidColorBrush(Colors.Black);
                FinishReportBtn.IsEnabled = false;
            }

        }
        private void setWPFDisplayText()
        {
            NameSurnameBlock.Text = selectedPatient.Name + " " + selectedPatient.Surname;
            GenderBlock.Text = selectedPatient.Gender.ToString();
            DateBirthBlock.Text = selectedPatient.Birthday.Date.ToShortDateString();
            JMBGBlock.Text = selectedPatient.Jmbg;
        }
        private void setItemSources()
        {
            ProblemsListBox.ItemsSource = selectedPatient.MedicalRecord.ConditionList;
            AllergiesListBox.ItemsSource = new ObservableCollection<string> (selectedPatient.MedicalRecord.AllergiesList);
            AllergiesListBox.ItemsSource = new ObservableCollection<string> (selectedPatient.medicalRecord.AllergiesList);
            MedicationsListBox.ItemsSource = selectedPatientMedicineHistory;
            PastReportsListBox.ItemsSource = selectedPatientReportHistory;
        }
        private List<Medicine> getSelectedPatientMedicine()
        {
            List<Medicine> patientMedicine = new List<Medicine>();

            foreach (MedicalReceipt medicalReceipt in selectedPatient.MedicalRecord.MedicalReceipt)
            {
                patientMedicine.AddRange(medicalReceipt.Medicine);
            }
            return patientMedicine;
        }

        private List<MedicineData> getSelectedPatientMedicineData(List<Medicine> selectedPatientMedicineHistory)
        {
            List<MedicineData>  patientMedicineData = new List<MedicineData>();
            foreach (Medicine medicine in selectedPatientMedicineHistory)
            {
                patientMedicineData.Add(medicine.MedicineData);
            }
            return patientMedicineData;
        }
      
        private void CancelAndCloseRemove()
        {
            if (newlyCreatedReports.Count > 0) clearCreatedReports(newlyCreatedReports);

            if (newlyCreatedAppointments.Count > 0) clearCreatedAppoinments(newlyCreatedAppointments);

            if (newlyCreatedDiagnosis.Count > 0) clearCreatedDiagnosis(newlyCreatedDiagnosis);
        }

        private void clearCreatedReports(List<MedicalReport> newlyCreatedReports)
        {
            foreach (MedicalReport medicalReport in newlyCreatedReports)
            {
                clearMedicineFromCreatedReport(medicalReport.MedicalReceipt.Medicine);
                clearMedicalReceiptFromCreatedReport(medicalReport);

                selectedPatient.medicalRecord.RemoveMedicalReport(medicalReport);
                MedicalReportController.Instance.DeleteByID(medicalReport.Id);
            }
            newlyCreatedReports.Clear();
        }

        private void clearMedicineFromCreatedReport(ObservableCollection<Medicine> reportMedicine)
        {
            foreach (Medicine medicine in reportMedicine)
            {
                selectedPatientMedicineHistory.Remove(medicine.MedicineData);
                MedicineController.Instance.DeleteByID(medicine.Id);
            }
        }

        private void clearMedicalReceiptFromCreatedReport(MedicalReport medicalReport)
        {
            selectedPatient.medicalRecord.MedicalReceipt.Remove(medicalReport.MedicalReceipt);
            MedicalReceiptController.Instance.DeleteByID(medicalReport.MedicalReceipt.Id);
        }

        private void clearCreatedAppoinments(List<MedicalAppointment> newlyCreatedAppointments)
        {
            foreach (MedicalAppointment medicalAppointment in newlyCreatedAppointments)
            {
                MedicalAppointmentController.Instance.DeleteByID(medicalAppointment.Id);
                medicalAppointment.Doctor.MedicalAppointment.Remove(medicalAppointment);
                DoctorController.Instance.Update(medicalAppointment.Doctor);
            }
            newlyCreatedAppointments.Clear();
        }

        private void clearCreatedDiagnosis(List<string> newlyCreatedDiagnosis)
        {
            foreach (string diagnosis in newlyCreatedDiagnosis)
            {
                selectedPatient.MedicalRecord.ConditionList.Remove(diagnosis);
            }

            MedicalRecordController.Instance.Update(selectedPatient.MedicalRecord);
            newlyCreatedDiagnosis.Clear();
        }

        private void BackBtn(object sender, RoutedEventArgs e)
        {
            CancelAndCloseRemove();

            if (doctorAppointmentsView == null)
            {
                medicalRecordsScreenView.Show();
            }
            else
            {
                doctorAppointmentsView.Show();
            }
            this.Close();
        }

        private void CreateReportBtnClick(object sender, RoutedEventArgs e)
        {
            CreateReport createReportView = new CreateReport(selectedPatient, this);
            createReportView.Owner = this;
            createReportView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            createReportView.Show();

            this.Hide();
        }

        private void OpenReportBtnClick(object sender, RoutedEventArgs e)
        {
            MedicalReport medicalReport = PastReportsListBox.SelectedItem as MedicalReport;
            if (!(isReportSelected(medicalReport))) return;

            OpenReport openReportView = new OpenReport(medicalReport, this, canCreateReport);
            openReportView.Owner = this;
            openReportView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            openReportView.Show();

            this.Hide();
        }

        private bool isReportSelected(MedicalReport medicalReport)
        {
            bool returnValue = true;
            if (medicalReport == null)
            {
                MessageBox.Show("Please select a report", "Open report", MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            return returnValue;
        }

        private void FinishReportClick(object sender, RoutedEventArgs e)
        {
            PatientController.Instance.Update(selectedPatient);
            Doctor selectedDoctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);

            if (!(DoctorController.Instance.doctorHasMedicalRecord(selectedDoctor, selectedPatient))) {
                selectedDoctor.AddMedicalRecord(selectedPatient.MedicalRecord);
            }
            
            MedicalAppointmentController.Instance.DeleteByID(DoctorAppointments.medicalAppointment.Id);
            selectedDoctor.MedicalAppointment.Remove(DoctorAppointments.medicalAppointment);
            DoctorAppointments.CurDocAppointemntsObservable.Remove(DoctorAppointments.medicalAppointment);

            DoctorController.Instance.Update(selectedDoctor);
         
            doctorAppointmentsView.Show();
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
