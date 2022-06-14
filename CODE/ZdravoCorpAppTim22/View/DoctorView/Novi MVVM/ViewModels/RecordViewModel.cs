using Controller;
using Model;
using MVVM1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.Views;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class RecordViewModel : ViewModel
    {
        //DODAO ZA ISPRAVAK
        public static CreateReportPage createReportPage { get; set; }

        //POLJA
        private readonly NavigationService navigationService;

        //PROPERTY
        public static Patient SelectedPatient { get; set; }
        private MedicalReportViewModel selectedMedicalReportViewModel;
        public MedicalReportViewModel SelectedMedicalReportViewModel
        {
            get { return selectedMedicalReportViewModel; }
            set
            {
                selectedMedicalReportViewModel = value;
                OpenReportCommand.RaiseCanExecuteChanged();
            }
        }

        public static ObservableCollection<MedicalReportViewModel> SelectedPatientReportHistory { get; set; }
        public static ObservableCollection<DrugsViewModel> SelectedPatientMedicineHistory { get; set; }
        public static ObservableCollection<string> SelectedPatientConditionHistory { get; set; }
        public static ObservableCollection<string> SelectedPatientAlergyHistory { get; set; }


        public static List<MedicalReportViewModel> newlyCreatedReports;
        public static List<string> newlyCreatedDiagnosis;
        public static List<MedicalAppointmentViewModel> newlyCreatedAppointments;

        public int canCreateReport;     //ako doktor iz rasporeda gleda karton prosledjuje -1,
                                        //u ostalim slucajevima moze proslediti bilo koji drugi broj

        //KONSTRUKTOR
        public RecordViewModel(int canCreateReport, Patient selectedPatient, 
            System.Windows.Navigation.NavigationService navigationService)
        {
            initializeListsForNewlyCreatedData();
            SelectedPatient = selectedPatient;
            this.navigationService = navigationService;
            this.canCreateReport = canCreateReport;
            PatientController.Instance.checkIfPatientHasMedicalRecord(selectedPatient);
            initializePatientReportDataHistory();

            CreateReportCommand = new MyICommand(ExecuteCreateReportCommand, CanExecuteCreateRportCommand);
            OpenReportCommand = new MyICommand(ExecuteOpenReportCommand, CanExecuteOpenReportCommand);
            FinishReportCommand = new MyICommand(ExecuteFinishReportCommand, CanExecuteFinishReportCommand);
            BackCommand = new MyICommand(ExecuteBackCommand);
        }

        private void initializeListsForNewlyCreatedData()
        {
            newlyCreatedReports = new List<MedicalReportViewModel>();
            newlyCreatedDiagnosis = new List<string>();
            newlyCreatedAppointments = new List<MedicalAppointmentViewModel>();
        }
        private void initializePatientReportDataHistory()
        {
            PatientHistoryDataViewModel patientHistoryDataViewModel = new PatientHistoryDataViewModel(SelectedPatient);

            List<Medicine> patientMedicine = getSelectedPatientMedicine();
            SelectedPatientMedicineHistory =
                new ObservableCollection<DrugsViewModel>(getSelectedPatientMedicineData(patientMedicine));
            SelectedPatientReportHistory = 
                new ObservableCollection<MedicalReportViewModel>(getMedicalReport(patientHistoryDataViewModel.MedicalReport));
            SelectedPatientConditionHistory = new ObservableCollection<string>(patientHistoryDataViewModel.ConditionList);
            SelectedPatientAlergyHistory = new ObservableCollection<string>(patientHistoryDataViewModel.AllergiesList);
        }

        private List<MedicalReportViewModel> getMedicalReport(List<MedicalReport> medicalReport)
        {
            List<MedicalReportViewModel> tempList = new List<MedicalReportViewModel>();
            foreach (var item in medicalReport)
            {
                tempList.Add(new MedicalReportViewModel(item));
            }
            return tempList;
        }

        private List<Medicine> getSelectedPatientMedicine()
        {
            List<Medicine> patientMedicine = new List<Medicine>();

            foreach (MedicalReceipt medicalReceipt in SelectedPatient.MedicalRecord.MedicalReceipt)
            {
                patientMedicine.AddRange(medicalReceipt.Medicine);
            }
            return patientMedicine;
        }
        private List<DrugsViewModel> getSelectedPatientMedicineData(List<Medicine> selectedPatientMedicineHistory)
        {
            List<DrugsViewModel> patientMedicineData = new List<DrugsViewModel>();
            foreach (Medicine medicine in selectedPatientMedicineHistory)
            {
                patientMedicineData.Add(new DrugsViewModel(medicine));
            }
            return patientMedicineData;
        }

        //KOMANDE
        public MyICommand BackCommand { get; set; }
        private void ExecuteBackCommand()
        {
            CancelAndCloseRemove();
            this.navigationService.GoBack();
        }

        public MyICommand CreateReportCommand { get; set; }
        private void ExecuteCreateReportCommand()
        {
            CreateReportViewModel viewModel = new CreateReportViewModel(navigationService, SelectedPatient);
            createReportPage = new CreateReportPage(viewModel);
            this.navigationService.Navigate(createReportPage);
        }

        private bool CanExecuteCreateRportCommand()
        {
            return canCreateReport == 1;
        }
        
        public MyICommand OpenReportCommand { get; set; }
        private void ExecuteOpenReportCommand()
        {
            OpenReportViewModel viewModel = new OpenReportViewModel(navigationService, SelectedMedicalReportViewModel);
            OpenReportPage openReportPage = new OpenReportPage(viewModel);
            this.navigationService.Navigate(openReportPage);
        }

        private bool CanExecuteOpenReportCommand()
        {
            return SelectedMedicalReportViewModel != null;
        }

        public MyICommand FinishReportCommand { get; set; }
        private void ExecuteFinishReportCommand()
        {
            PatientController.Instance.Update(SelectedPatient);
            Doctor selectedDoctor = AuthenticationController.Instance.GetLoggedUser() as Doctor;

            if (!(DoctorController.Instance.doctorHasMedicalRecord(selectedDoctor, SelectedPatient)))
                selectedDoctor.AddMedicalRecord(SelectedPatient.MedicalRecord);

            MedicalAppointmentController.Instance.DeleteByID(ListAppointmentViewModel.MedAppBackup.Id);
            selectedDoctor.MedicalAppointment.Remove(ListAppointmentViewModel.MedAppBackup.MedicalAppointment);
            ListAppointmentViewModel.CurDocAppointmentsObservable.Remove(ListAppointmentViewModel.MedAppBackup);

            DoctorController.Instance.Update(selectedDoctor);

            this.navigationService.GoBack();
        }
        private bool CanExecuteFinishReportCommand()
        {
            return canCreateReport == 1;
        }

        //CISCENJE POSLE BACK
        private void CancelAndCloseRemove()
        {
            if (newlyCreatedReports.Count > 0) clearCreatedReports(newlyCreatedReports);
            if (newlyCreatedAppointments.Count > 0) clearCreatedAppoinments(newlyCreatedAppointments);
            if (newlyCreatedDiagnosis.Count > 0) clearCreatedDiagnosis(newlyCreatedDiagnosis);
        }
        private void clearCreatedReports(List<MedicalReportViewModel> newlyCreatedReports)
        {
            foreach (MedicalReportViewModel medicalReport in newlyCreatedReports)
            {
                clearMedicineFromCreatedReport(medicalReport.MedicalReceipt.Medicine);
                clearMedicalReceiptFromCreatedReport(medicalReport);

                SelectedPatient.medicalRecord.RemoveMedicalReport(medicalReport.MedicalReport);
                MedicalReportController.Instance.DeleteByID(medicalReport.Id);
            }
            newlyCreatedReports.Clear();
        }
        private void clearMedicineFromCreatedReport(ObservableCollection<Medicine> reportMedicine)
        {
            foreach (Medicine medicine in reportMedicine)
            {
                foreach (var drugs in SelectedPatientMedicineHistory)
                {
                    if (medicine.MedicineData.Id == drugs.MedicineData.Id)
                    {
                        SelectedPatientMedicineHistory.Remove(drugs);
                        break;
                    }
                }
                MedicineController.Instance.DeleteByID(medicine.Id);
            }
        }

        private void clearMedicalReceiptFromCreatedReport(MedicalReportViewModel medicalReport)
        {
            SelectedPatient.medicalRecord.MedicalReceipt.Remove(medicalReport.MedicalReceipt);
            MedicalReceiptController.Instance.DeleteByID(medicalReport.MedicalReceipt.Id);
        }

        private void clearCreatedAppoinments(List<MedicalAppointmentViewModel> newlyCreatedAppointments)
        {
            foreach (var medicalAppointment in newlyCreatedAppointments)
            {
                MedicalAppointmentController.Instance.DeleteByID(medicalAppointment.Id);
                medicalAppointment.Doctor.MedicalAppointment.Remove(medicalAppointment.MedicalAppointment);
                DoctorController.Instance.Update(medicalAppointment.Doctor);
            }
            newlyCreatedAppointments.Clear();
        }
        private void clearCreatedDiagnosis(List<string> newlyCreatedDiagnosis)
        {
            foreach (string diagnosis in newlyCreatedDiagnosis)
            {
                SelectedPatient.MedicalRecord.ConditionList.Remove(diagnosis);
            }

            MedicalRecordController.Instance.Update(SelectedPatient.MedicalRecord);
            newlyCreatedDiagnosis.Clear();
        }

    }
}
