using Controller;
using Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class OpenReport : Window
    {
        private MedicalReport selectedMedicalReport;
        private MedicalRecordView medicalRecordView;

        private string oldDiagnosis;
        Medicine selectedMedicine;

        public OpenReport(MedicalReport medicalReport, MedicalRecordView medicalRecordView)
        {
            InitializeComponent();
            selectedMedicalReport = medicalReport;
            this.medicalRecordView = medicalRecordView; 

            selectedMedicine = MedicalReceiptController.Instance.GetAll().Where
                (r => r.Id == selectedMedicalReport.MedicalReceipt.Id).FirstOrDefault().Medicine[0];
            
            setWPFDisplayText(selectedMedicine);
            setAbilityToChangeReport();
        }

        private void setWPFDisplayText(Medicine medicine)       //ne pomeraj
        {
            DateBlock.Text = selectedMedicalReport.ReportDate.ToShortDateString();
            NameBlock.Text = selectedMedicalReport.MedicalRecord.Patient.Name;
            SurnameBlock.Text = selectedMedicalReport.MedicalRecord.Patient.Surname;
            DiagnosisBox.Text = selectedMedicalReport.Diagnosis;
            AnamnesisBox.Text = selectedMedicalReport.Anamnesis;
            AmountComboBox.Text = medicine.Amount.ToString();
            setItemSources();
            EndDateDatePicker.Text = selectedMedicalReport.MedicalReceipt.EndDate.ToString();
            TimeComboBox.Text = selectedMedicalReport.MedicalReceipt.Time;
            AdditionalInstructionsTextBox.Text = selectedMedicalReport.MedicalReceipt.AdditionalInstructions;
            PurposeComboBox.Text = selectedMedicalReport.MedicalReceipt.TherapyPurpose;
            oldDiagnosis = DiagnosisBox.Text;

        }

        private void setItemSources()       //ne pomeraj
        {
            MedicationComboBox.ItemsSource = MedicineDataController.Instance.GetAllApproved();
            MedicationComboBox.SelectedValuePath = "Id";
            if (selectedMedicalReport.MedicalReceipt.Medicine.Count > 0)
                MedicationComboBox.SelectedValue = selectedMedicalReport.MedicalReceipt.Medicine[0].MedicineData.Id;
        }

        private void setAbilityToChangeReport()     //ne pomeraj
        {
            if (selectedMedicalReport.DoctorID != DoctorHomeScreen.LoggedInDoctor.Id)
            {
                ChangeReportBtn.IsEnabled = false;
                ChangeReportBtn.Foreground = new SolidColorBrush(Colors.Black);
                DiagnosisBox.IsEnabled = false;
                AnamnesisBox.IsEnabled = false;
                MedicationComboBox.IsEnabled = false;
                EndDateDatePicker.IsEnabled = false;
                TimeComboBox.IsEnabled = false;
                AdditionalInstructionsTextBox.IsEnabled = false;
                PurposeComboBox.IsEnabled = false;
                AmountComboBox.IsEnabled = false;
            }
        }

        private void ChangeReportClick(object sender, RoutedEventArgs e)        //ne pomeraj
        {
            if (!(isDateAndTimeValid())) return;

            MedicineData selectedMedicineData = MedicationComboBox.SelectedItem as MedicineData;
            selectedMedicine.MedicineData = selectedMedicineData;
            if (!(isMedicineAmountValid())) return;

            Medicine medicineInStorage = MedicineController.Instance.GetAllFree().
                Where(r => r.MedicineData.Id == selectedMedicine.MedicineData.Id).FirstOrDefault();
            if (!(hasStorageEnoughMedicine(medicineInStorage))) return;
            else
            {
                medicineInStorage.Amount -= selectedMedicine.Amount;            //izmena leka u skladistu
                MedicineController.Instance.Update(medicineInStorage);
            }

            MedicalRecord selectedPatientMedicalRecord = MedicalRecordView.selectedPatient.MedicalRecord;
            updateMedicalReport();

            selectedMedicine.MedicalReceipt = selectedMedicalReport.MedicalReceipt;
            MedicineController.Instance.Update(selectedMedicine);
            MedicalReceiptController.Instance.Update(selectedMedicalReport.MedicalReceipt);
            MedicalReportController.Instance.Update(selectedMedicalReport);

            updateDiagnosis(selectedPatientMedicalRecord);

            MedicalRecordView.selectedPatientMedicineHistory[MedicalRecordView.selectedPatientReportHistory.IndexOf(selectedMedicalReport)] =
            selectedMedicine.MedicineData;

            medicalRecordView.Show();
            this.Close();
        }

        private bool isDateAndTimeValid()       //ne pomeraj
        {
            bool returnValue = true;
            if (EndDateDatePicker.SelectedDate == null || TimeComboBox.Text == null)
            {
                MessageBox.Show("Please fill out Medication, End date and Time fields", "Open report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            return returnValue;
        }

        private bool isMedicineAmountValid()        //ne pomeraj
        {
            bool returnValue = true;
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                selectedMedicine.Amount = Int32.Parse(AmountComboBox.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The field 'Amount' can only be a number!", "Open report",
                MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
#pragma warning restore CS0168 // Variable is declared but never used
            return returnValue;
        }

        private bool hasStorageEnoughMedicine(Medicine medicineInStorage)       //pomerio
        {
            bool returnValue = true;
            if (!(MedicineController.Instance.hasStorageEnoughMedicine(medicineInStorage, selectedMedicine)))
            {
                MessageBox.Show("Selected amount excedes the amount located in the werehouse", "Open report",
                MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            return returnValue;
        }

        private void updateMedicalReport()      //ne pomeraj
        {
            selectedMedicalReport.Anamnesis = AnamnesisBox.Text;
            selectedMedicalReport.Diagnosis = DiagnosisBox.Text;
            selectedMedicalReport.MedicalReceipt.AdditionalInstructions = AdditionalInstructionsTextBox.Text;
            selectedMedicalReport.MedicalReceipt.TherapyPurpose = PurposeComboBox.Text;
            selectedMedicalReport.MedicalReceipt.Medicine[0] = selectedMedicine;
            selectedMedicalReport.MedicalReceipt.EndDate = (DateTime)EndDateDatePicker.SelectedDate;
            selectedMedicalReport.MedicalReceipt.Time = TimeComboBox.Text;
            selectedMedicalReport.MedicalReceipt.TherapyPurpose = PurposeComboBox.Text;
        }

        private void updateDiagnosis(MedicalRecord medicalRecord)          //ne pomeraj
        {
            foreach (string diagnosis in medicalRecord.ConditionList)
            {
                if (diagnosis == oldDiagnosis)  
                {
                    medicalRecord.ConditionList[medicalRecord.ConditionList.IndexOf(diagnosis)] = DiagnosisBox.Text;
                    MedicalRecordController.Instance.Update(medicalRecord);
                    MedicalRecordView.selectedPatientConditionHistory
                        [MedicalRecordView.selectedPatientConditionHistory.IndexOf(diagnosis)] = DiagnosisBox.Text;
                    break;
                }
            }
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)       //ne pomeraj
        {
            MessageBoxResult result = MessageBox.Show("Close window without saving?", "Create appointment", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    medicalRecordView.Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:

                    break;
            }
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)        //ne pomeraj
        {
            //DoctorHome.doctorHome.Show();
            Application.Current.MainWindow.Show();
            foreach (Window item in App.Current.Windows)
            {
                if (item != Application.Current.MainWindow)
                {
                    item.Close();
                }
            }
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)      //ne pomeraj
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}
