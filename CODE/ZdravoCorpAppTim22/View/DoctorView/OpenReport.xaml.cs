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
    public partial class OpenReport : Window
    {
        private MedicalReport selectedMedicalReport;
        private MedicalRecordView medicalRecordView;

        private string oldDiagnosis;
        private int canCreateRecord;        //ako ne pravim novi izvestaj cuvam promene kod dijagnoze
        Medicine selectedMedicine;
        private int oldMedcineAmount;

        public OpenReport(MedicalReport medicalReport, MedicalRecordView medicalRecordView, int canCreateRecord)
        {
            InitializeComponent();
            selectedMedicalReport = medicalReport;
            this.medicalRecordView = medicalRecordView;

            DateBlock.Text = selectedMedicalReport.ReportDate.ToShortDateString();
            NameBlock.Text = selectedMedicalReport.MedicalRecord.Patient.Name;
            SurnameBlock.Text = selectedMedicalReport.MedicalRecord.Patient.Surname;
            DiagnosisBox.Text = selectedMedicalReport.Diagnosis;
            AnamnesisBox.Text = selectedMedicalReport.Anamnesis;

            MedicalReceipt selectedMedicalReceipt = MedicalReceiptController.Instance.GetAll().Where(r => r.Id == selectedMedicalReport.MedicalReceipt.Id).FirstOrDefault();
            selectedMedicine = selectedMedicalReceipt.Medicine[0];
            AmountComboBox.Text = selectedMedicine.Amount.ToString();

            MedicationComboBox.ItemsSource = MedicineDataController.Instance.GetAllApproved();
            MedicationComboBox.SelectedValuePath = "Id";
            if(selectedMedicalReport.MedicalReceipt.Medicine.Count > 0)
            {
                MedicationComboBox.SelectedValue = selectedMedicalReport.MedicalReceipt.Medicine[0].MedicineData.Id;
            }

            EndDateDatePicker.Text = selectedMedicalReport.MedicalReceipt.EndDate.ToString();
            TimeComboBox.Text = selectedMedicalReport.MedicalReceipt.Time;
            AdditionalInstructionsTextBox.Text = selectedMedicalReport.MedicalReceipt.AdditionalInstructions;
            PurposeComboBox.Text = selectedMedicalReport.MedicalReceipt.TherapyPurpose;

            oldDiagnosis = DiagnosisBox.Text;
            this.canCreateRecord = canCreateRecord;

            oldMedcineAmount = selectedMedicine.Amount;

            if (!isEditable())
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

        private bool isEditable()
        {
            return selectedMedicalReport.DoctorID == DoctorHome.selectedDoctorId;
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
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

        private void ChangeReportClick(object sender, RoutedEventArgs e)
        {
            MedicalRecord medRec = MedicalRecordView.selectedPatient.MedicalRecord;

            if (AnamnesisBox.Text == null)
            {
                selectedMedicalReport.Anamnesis = "";
            }
            else
            {
                selectedMedicalReport.Anamnesis = AnamnesisBox.Text;
            }

            if (DiagnosisBox.Text == null)
            {
                selectedMedicalReport.Diagnosis = "";
            }
            else
            {
                selectedMedicalReport.Diagnosis = DiagnosisBox.Text;
            }

            if (AdditionalInstructionsTextBox.Text == null)
            {
                selectedMedicalReport.MedicalReceipt.AdditionalInstructions = "";
            }
            else
            {
                selectedMedicalReport.MedicalReceipt.AdditionalInstructions = AdditionalInstructionsTextBox.Text;
            }

            if (PurposeComboBox.Text == null)
            {
                selectedMedicalReport.MedicalReceipt.TherapyPurpose = PurposeComboBox.Text;
            }

            if (MedicationComboBox.SelectedItem == null ||
                EndDateDatePicker.SelectedDate == null ||
                TimeComboBox.Text == null)
            {
                MessageBox.Show("Please fill out Medication, End date and Time fields", "Open report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                MedicineData selectedMedicineData = MedicationComboBox.SelectedItem as MedicineData;
                selectedMedicine.MedicineData = selectedMedicineData;

#pragma warning disable CS0168 // Variable is declared but never used
                try
                {
                    selectedMedicine.Amount = Int32.Parse(AmountComboBox.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("The field 'Amount' can only be a number!", "Open report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
#pragma warning restore CS0168 // Variable is declared but never used

                Medicine warehouseMedicine = MedicineController.Instance.GetAllFree().
                    Where(r => r.MedicineData.Id == selectedMedicine.MedicineData.Id).FirstOrDefault();

                if (warehouseMedicine.Amount - selectedMedicine.Amount < 0)
                {
                    MessageBox.Show("Selected amount excedes the amount located in the werehouse", "Open report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    warehouseMedicine.Amount -= selectedMedicine.Amount;            //izmena leka u skladistu
                    MedicineController.Instance.Update(warehouseMedicine);
                }

                selectedMedicalReport.MedicalReceipt.Medicine[0] = selectedMedicine;
                selectedMedicine.MedicalReceipt = selectedMedicalReport.MedicalReceipt;
                MedicineController.Instance.Update(selectedMedicine);

                selectedMedicalReport.MedicalReceipt.EndDate = (DateTime)EndDateDatePicker.SelectedDate;
                selectedMedicalReport.MedicalReceipt.Time = TimeComboBox.Text;
                selectedMedicalReport.MedicalReceipt.TherapyPurpose = PurposeComboBox.Text;
            }
            

            MedicalReceiptController.Instance.Update(selectedMedicalReport.MedicalReceipt);
            MedicalReportController.Instance.Update(selectedMedicalReport);

            foreach (string diagnosis in medRec.ConditionList)
            {
                if (diagnosis == oldDiagnosis)         //ako kreiram novi izvestaj pritiskom na back sve ponistavam, u suprotnom cuvam promenu dijagnoze
                {                                                               
                    medRec.ConditionList[medRec.ConditionList.IndexOf(diagnosis)] = DiagnosisBox.Text;
                    MedicalRecordController.Instance.Update(medRec);
                    break;
                }
            }

            MedicalRecordView.selectedPatientMedicineHistory[MedicalRecordView.selectedPatientReportHistory.IndexOf(selectedMedicalReport)] =
            selectedMedicine.MedicineData;
        medicalRecordView.Show();
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
