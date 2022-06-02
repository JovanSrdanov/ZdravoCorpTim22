using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static ObservableCollection<string> selectedPatientConditionHistory { get; set; }
        public static ObservableCollection<string> selectedPatientAlergyHistory { get; set; }


        public static List<MedicalReport> newlyCreatedReports;           
        public static List<string> newlyCreatedDiagnosis;
        public static List<MedicalAppointment> newlyCreatedAppointments;

        public int canCreateReport;     //ako doktor iz rasporeda gleda karton prosledjuje -1,
                                        //u ostalim slucajevima moze proslediti bilo koji drugi broj
        public MedicalRecordView(int canCreateReport, int selectedPatientID, DoctorAppointments doctorAppointmentsView = null, 
            MedicalRecordsScreen medicalRecordsScreenView = null)
        {
            InitializeComponent();
            initializeListsForNewlyCreatedData();

            this.doctorAppointmentsView = doctorAppointmentsView;
            this.medicalRecordsScreenView = medicalRecordsScreenView;

            selectedPatient = PatientController.Instance.GetByID(selectedPatientID);
            this.canCreateReport = canCreateReport;

            checkIfCanCreateReport();
            setWPFDisplayText();

            PatientController.Instance.checkIfPatientHasMedicalRecord(selectedPatient);

            initializePatientReportDataHistory();
            setItemSources();

        }
        private void initializeListsForNewlyCreatedData()       //ne pomeraj
        {
            newlyCreatedReports = new List<MedicalReport>();
            newlyCreatedDiagnosis = new List<string>();
            newlyCreatedAppointments = new List<MedicalAppointment>();
        }
        private void checkIfCanCreateReport()   //ne pomeraj
        {
            if (canCreateReport == -1)
            {
                CreateReportBtn.IsEnabled = false;
                CreateReportBtn.Foreground = new SolidColorBrush(Colors.Black);
                FinishReportBtn.IsEnabled = false;
            }

        }
        private void setWPFDisplayText()        //ne pomeraj
        {
            NameSurnameBlock.Text = selectedPatient.Name + " " + selectedPatient.Surname;
            GenderBlock.Text = selectedPatient.Gender.ToString();
            DateBirthBlock.Text = selectedPatient.Birthday.Date.ToShortDateString();
            JMBGBlock.Text = selectedPatient.Jmbg;
        }
        private void initializePatientReportDataHistory()       //ne pomeraj
        {
            List<Medicine> patientMedicine = getSelectedPatientMedicine();
            selectedPatientMedicineHistory =
                new ObservableCollection<MedicineData>(getSelectedPatientMedicineData(patientMedicine));
            selectedPatientReportHistory = new ObservableCollection<MedicalReport>(selectedPatient.MedicalRecord.MedicalReport);
            selectedPatientConditionHistory = new ObservableCollection<string>(selectedPatient.MedicalRecord.ConditionList);
            selectedPatientAlergyHistory = new ObservableCollection<string>(selectedPatient.MedicalRecord.AllergiesList);
        }
        private List<Medicine> getSelectedPatientMedicine()     //mozda?
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
            List<MedicineData> patientMedicineData = new List<MedicineData>();      //mozda?
            foreach (Medicine medicine in selectedPatientMedicineHistory)
            {
                patientMedicineData.Add(medicine.MedicineData);
            }
            return patientMedicineData;
        }
        private void setItemSources()       //ne pomeraj
        {
            ProblemsListBox.ItemsSource = selectedPatientConditionHistory;
            AllergiesListBox.ItemsSource = selectedPatientAlergyHistory;
            MedicationsListBox.ItemsSource = selectedPatientMedicineHistory;
            PastReportsListBox.ItemsSource = selectedPatientReportHistory;
        }

        private void CreateReportBtnClick(object sender, RoutedEventArgs e)     //ne pomeraj
        {
            CreateReport createReportView = new CreateReport(selectedPatient, this);
            createReportView.Owner = this;
            createReportView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            createReportView.Show();

            this.Hide();
        }

        private void OpenReportBtnClick(object sender, RoutedEventArgs e)       //ne pomeraj
        {
            MedicalReport medicalReport = PastReportsListBox.SelectedItem as MedicalReport;
            if (!(isReportSelected(medicalReport))) return;

            OpenReport openReportView = new OpenReport(medicalReport, this);
            openReportView.Owner = this;
            openReportView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            openReportView.Show();

            this.Hide();
        }
        private bool isReportSelected(MedicalReport medicalReport)      //ne pomeraj
        {
            bool returnValue = true;
            if (medicalReport == null)
            {
                MessageBox.Show("Please select a report", "Open report", MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            return returnValue;
        }

        private void FinishReportClick(object sender, RoutedEventArgs e)        //ne pomeraj
        {
            PatientController.Instance.Update(selectedPatient);
            Doctor selectedDoctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);

            if (!(DoctorController.Instance.doctorHasMedicalRecord(selectedDoctor, selectedPatient)))
                selectedDoctor.AddMedicalRecord(selectedPatient.MedicalRecord);

            MedicalAppointmentController.Instance.DeleteByID(DoctorAppointments.medicalAppointment.Id);
            selectedDoctor.MedicalAppointment.Remove(DoctorAppointments.medicalAppointment);
            DoctorAppointments.CurDocAppointemntsObservable.Remove(DoctorAppointments.medicalAppointment);

            DoctorController.Instance.Update(selectedDoctor);

            doctorAppointmentsView.Show();
            this.Close();
        }
        private void BackBtn(object sender, RoutedEventArgs e)      //ne pomeraj
        {
            CancelAndCloseRemove();

            if (doctorAppointmentsView == null)
                medicalRecordsScreenView.Show();
            else
                doctorAppointmentsView.Show();
            this.Close();
        }

        private void CancelAndCloseRemove()     //ne pomeraj
        {
            if (newlyCreatedReports.Count > 0) clearCreatedReports(newlyCreatedReports);
            if (newlyCreatedAppointments.Count > 0) clearCreatedAppoinments(newlyCreatedAppointments);
            if (newlyCreatedDiagnosis.Count > 0) clearCreatedDiagnosis(newlyCreatedDiagnosis);
        }

        private void clearCreatedReports(List<MedicalReport> newlyCreatedReports)       //mozda?
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

        private void clearMedicineFromCreatedReport(ObservableCollection<Medicine> reportMedicine)      //mozda?
        {
            foreach (Medicine medicine in reportMedicine)
            {
                selectedPatientMedicineHistory.Remove(medicine.MedicineData);
                MedicineController.Instance.DeleteByID(medicine.Id);
            }
        }

        private void clearMedicalReceiptFromCreatedReport(MedicalReport medicalReport)      //mozda?
        {
            selectedPatient.medicalRecord.MedicalReceipt.Remove(medicalReport.MedicalReceipt);
            MedicalReceiptController.Instance.DeleteByID(medicalReport.MedicalReceipt.Id);
        }

        private void clearCreatedAppoinments(List<MedicalAppointment> newlyCreatedAppointments)     //mozda?
        {
            foreach (MedicalAppointment medicalAppointment in newlyCreatedAppointments)
            {
                MedicalAppointmentController.Instance.DeleteByID(medicalAppointment.Id);
                medicalAppointment.Doctor.MedicalAppointment.Remove(medicalAppointment);
                DoctorController.Instance.Update(medicalAppointment.Doctor);
            }
            newlyCreatedAppointments.Clear();
        }

        private void clearCreatedDiagnosis(List<string> newlyCreatedDiagnosis)      //mozda?
        {
            foreach (string diagnosis in newlyCreatedDiagnosis)
            {
                selectedPatient.MedicalRecord.ConditionList.Remove(diagnosis);
            }

            MedicalRecordController.Instance.Update(selectedPatient.MedicalRecord);
            newlyCreatedDiagnosis.Clear();
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)        //ne pomeraj
        {
            DoctorHome.doctorHome.Show();
            this.Close();
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)      //ne pomeraj
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}
