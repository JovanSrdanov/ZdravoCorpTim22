using Controller;
using Model;
using MVVM1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.Views;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class CreateReportViewModel : ViewModel
    {
        //POLJA
        private readonly NavigationService navigationService;

        //PROPERTY
        public Patient SelectedPatient { get; set;  }
        public string DateBlockText { get; set; }
        public string AmountText { get; set; }
        public static string AnamnesisText { get; set; }
        public static bool IsReferButtonEnabled { get; set; }
        public string DiagnosisText { get; set; }
        public string AdditionalInstructionsText { get; set; }
        public string PurposeText { get; set; }

        private DateTime endDate;
        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                IsDateValid(endDate, EndTime);
            }
        }
        private string endTime;
        public string EndTime
        {
            get { return endTime; }
            set
            {
                endTime = value;
                IsDateValid(EndDate, endTime);
            }
        }
        public DateTime EndDateTime { get; set; }
        public bool IsDateTimeValid { get; set; }

        private bool isVisible;
        public bool IsVisible
        {
            get { return isVisible; }
            set
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
                if (ConfirmCommand != null) ConfirmCommand.RaiseCanExecuteChanged();
            }
        }

        public PatientViewModel PatientViewModel { get; set; }
        public ObservableCollection<DrugsViewModel> ApprovedMedicineList { get; set; }

        private DrugsViewModel selectedMedicine;
        public DrugsViewModel SelectedMedicineFromStorage
        {
            get { return selectedMedicine; }
            set
            {
                selectedMedicine = value;
                ChangeAlergicVisibility();
            }
        }

        //KONSTRUKTOR
        public CreateReportViewModel(System.Windows.Navigation.NavigationService navigationService, Model.Patient selectedPatient)
        {
            this.navigationService = navigationService;
            SelectedPatient = selectedPatient;
            PatientViewModel = new PatientViewModel(SelectedPatient);
            DateBlockText = DateTime.Now.ToShortDateString();
            ApprovedMedicineList = new ObservableCollection<DrugsViewModel>(GetApprovedMedicine());
            if (ApprovedMedicineList.Count() > 0) SelectedMedicineFromStorage = ApprovedMedicineList[0];
            AmountText = "0";
            AnamnesisText = "";
            DiagnosisText = "";
            AdditionalInstructionsText = "";
            PurposeText = "";
            EndDate = DateTime.Now;
            EndTime = "10:00";
            IsReferButtonEnabled = true;

            ConfirmCommand = new MyICommand(ExecuteConfirmCommand, CanExecuteConfirmCommand);
            ReferToSpecialistCommand = new MyICommand(ExecuteReferToSpecialistCommand);
            CancelCommand = new MyICommand(ExecuteCancelCommand);
        }

        private List<DrugsViewModel> GetApprovedMedicine()
        {
            List<DrugsViewModel> tempList = new List<DrugsViewModel>();
            foreach (var medicine in MedicineController.Instance.GetAllApproved())
            {
                tempList.Add(new DrugsViewModel(medicine));
            }
            return tempList;
        }

        //POMOCNE FUNKCIJE
        private void IsDateValid(DateTime endDate, string endTime)
        {
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                if (endTime == null) IsDateTimeValid = false;
                else if (endTime.Equals("")) IsDateTimeValid = false;
                else
                {
                    EndDateTime = DateTime.Parse(endDate.ToShortDateString() + " " + endTime);
                    IsDateTimeValid = true;
                }

                if (ConfirmCommand != null) ConfirmCommand.RaiseCanExecuteChanged();
            }
            catch (Exception ex)
            {
                IsDateTimeValid = false;
                if (ConfirmCommand != null) ConfirmCommand.RaiseCanExecuteChanged();
            }
#pragma warning restore CS0168 // Variable is declared but never used
        }
        private bool IsAmountValid(Medicine requestedMedicine)
        {
            bool returnValue = true;
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                requestedMedicine.Amount = Int32.Parse(AmountText);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The field 'Amount' is not filled in correctly!", "Create report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
#pragma warning restore CS0168 // Variable is declared but never used
            return returnValue;
        }
        private bool hasStorageEnoughMedicine(DrugsViewModel selectedMedicineFromStorage, Medicine requestedMedicine)
        {
            bool returnValue = true;
            if (!(MedicineController.Instance.hasStorageEnoughMedicine(selectedMedicineFromStorage.Medicine, requestedMedicine)))
            {
                MessageBox.Show("Selected amount excedes the amount located in the warehouse", "Create report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            else
            {
                selectedMedicineFromStorage.Amount -= requestedMedicine.Amount;
                selectedMedicineFromStorage.Medicine.Amount = selectedMedicineFromStorage.Amount;
            }
            return returnValue;
        }

        private void addDiagnosisToMedicalRecord(MedicalRecord selectedPatientMedicalRecord)
        {
            if (!DiagnosisText.Equals(""))
            {
                selectedPatientMedicalRecord.ConditionList.Add(DiagnosisText);
            }
        }
        private void updateMedicalRecordView(DrugsViewModel requestedMedicine, MedicalReportViewModel newReport)
        {
            RecordViewModel.SelectedPatientMedicineHistory.Add(requestedMedicine);
            RecordViewModel.newlyCreatedReports.Add(newReport);
            RecordViewModel.SelectedPatientReportHistory.Add(newReport);
            RecordViewModel.newlyCreatedDiagnosis.Add(DiagnosisText);
            RecordViewModel.SelectedPatientConditionHistory.Add(DiagnosisText);
        }
        private void ChangeAlergicVisibility()
        {
            if (MedicineDataController.Instance.isPatientAlergic
                (PatientViewModel.Patient, SelectedMedicineFromStorage.Medicine))
            {
                IsVisible = true;
            }
            else
            {
                IsVisible = false;
            }
        }

        //KOMANDE
        public MyICommand ConfirmCommand { get; set; }
        private void ExecuteConfirmCommand()
        {
            Medicine requestedMedicine = new Medicine();
            requestedMedicine.MedicineData = SelectedMedicineFromStorage.MedicineData;
            if (!(IsAmountValid(requestedMedicine))) return;

            MedicalRecord selectedPatientMedicalRecord = PatientViewModel.Patient.MedicalRecord;

            MedicalReport newReport = new MedicalReport(-1, AnamnesisText, DiagnosisText, DateTime.Now,
                selectedPatientMedicalRecord);
            //da bih prepoznao koji doktor je kreirao koji izvestaj, da bih kontrolisao ko moze da ga menja
            newReport.DoctorId = (AuthenticationController.Instance.GetLoggedUser() as Doctor).Id;

            MedicalReceipt newReportMedicalReceipt = new MedicalReceipt(EndDate,
                EndTime, requestedMedicine,
                AdditionalInstructionsText, PurposeText, selectedPatientMedicalRecord);
            newReportMedicalReceipt.MedicalRecord = selectedPatientMedicalRecord;
            MedicalReceiptController.Instance.Create(newReportMedicalReceipt);

            requestedMedicine.MedicalReceipt = newReportMedicalReceipt;
            MedicineController.Instance.Create(requestedMedicine);

            if (!(hasStorageEnoughMedicine(SelectedMedicineFromStorage, requestedMedicine))) return;
            MedicineController.Instance.Update(SelectedMedicineFromStorage.Medicine);

            newReport.MedicalReceipt = newReportMedicalReceipt;
            newReport.MedicalReceipt.Medicine[0] = requestedMedicine;

            newReportMedicalReceipt.Medicine[0] = requestedMedicine;
            MedicalReceiptController.Instance.Update(newReportMedicalReceipt);

            MedicalReportController.Instance.Create(newReport);

            addDiagnosisToMedicalRecord(selectedPatientMedicalRecord);
            selectedPatientMedicalRecord.MedicalReport.Add(newReport);
            MedicalRecordController.Instance.Update(selectedPatientMedicalRecord);
            
            updateMedicalRecordView(new DrugsViewModel(requestedMedicine), new MedicalReportViewModel(newReport));

            this.navigationService.GoBack();
        }

        private bool CanExecuteConfirmCommand()
        {
            if (IsVisible || (!(IsDateTimeValid)))
            {
                return false;
            }
            return true;
        }

        public MyICommand ReferToSpecialistCommand { get; set; }
        private void ExecuteReferToSpecialistCommand()
        {
            ReferViewModel viewModel = new ReferViewModel(navigationService, SelectedPatient);
            ReferPage referPage = new ReferPage(viewModel);
            this.navigationService.Navigate(referPage);
        }

        public MyICommand CancelCommand { get; set; }
        private void ExecuteCancelCommand()
        {
            MessageBoxResult result = MessageBox.Show("Close window without saving?", "Create report",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.navigationService.GoBack();
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }
    }
}
