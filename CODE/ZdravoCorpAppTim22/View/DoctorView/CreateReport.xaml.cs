﻿using Controller;
using Model;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class CreateReport : Window
    {
        private Patient selectedPatient;
        private MedicalRecordView medicalRecordView;
        public static string anamnesis;
        public CreateReport(Patient selectedPatient, MedicalRecordView medicalRecordView)
        {
            InitializeComponent();
            this.medicalRecordView = medicalRecordView;
            this.selectedPatient = selectedPatient;
            setWPFDisplayText();
            setItemSources();
        }

        private void setWPFDisplayText()        //ne pomeraj
        {
            DateBlock.Text = DateTime.Now.ToShortDateString();
            NameBlock.Text = selectedPatient.Name;
            SurnameBlock.Text = selectedPatient.Surname;
        }

        private void setItemSources()       //ne pomeraj
        {
            
            MedicationComboBox.ItemsSource = new ObservableCollection<Medicine>(MedicineController.Instance.GetAllApproved());
            MedicationComboBox.SelectedIndex = 0;
        }
        private void ConfirmBtnClick(object sender, RoutedEventArgs e)      //ne pomeraj
        {

            if (!(isDateTimeValid())) return;

            Medicine selectedMedicineFromStorage = MedicationComboBox.SelectedItem as Medicine;
            Medicine requestedMedicine = new Medicine();
            requestedMedicine.MedicineData = selectedMedicineFromStorage.MedicineData;
            if (!(isAmountValid(requestedMedicine))) return;

            MedicalRecord selectedPatientMedicalRecord = selectedPatient.MedicalRecord;

            MedicalReport newReport = new MedicalReport(-1, AnamnesisBox.Text, DiagnosisBox.Text, DateTime.Now,
                selectedPatientMedicalRecord);
            newReport.DoctorID = DoctorHomeScreen.LoggedInDoctor.Id;       //da bih prepoznao koji doktor je kreirao
                                                                    //koji izvestaj, da bih kontrolisao ko moze da ga menja

            MedicalReceipt newReportMedicalReceipt = new MedicalReceipt((DateTime)EndDateDatePicker.SelectedDate,
                TimeComboBox.Text, requestedMedicine,
                AdditionalInstructionsTextBox.Text, PurposeComboBox.Text, selectedPatientMedicalRecord);

            requestedMedicine.MedicalReceipt = newReportMedicalReceipt;
            if (!(hasStorageEnoughMedicine(selectedMedicineFromStorage, requestedMedicine))) return;
            MedicineController.Instance.Update(selectedMedicineFromStorage);

            newReport.MedicalReceipt = newReportMedicalReceipt;
            MedicalReportController.Instance.Create(newReport);

            addDiagnosisToMedicalRecord(selectedPatientMedicalRecord);
            selectedPatientMedicalRecord.MedicalReport.Add(newReport);
            MedicalRecordController.Instance.Update(selectedPatientMedicalRecord);

            newReportMedicalReceipt.MedicalRecord = selectedPatientMedicalRecord;
            MedicalReceiptController.Instance.Create(newReportMedicalReceipt);
            MedicineController.Instance.Create(requestedMedicine);

            updateMedicalRecordView(requestedMedicine, newReport);
            medicalRecordView.Show();
            this.Close();
        }

        private bool isDateTimeValid()      //ne pomeraj
        {
            bool returnValue = true;
            if (EndDateDatePicker.SelectedDate == null || TimeComboBox.Text == "")
            {
                MessageBox.Show("Please fill out the date and time fields correctly", "Create report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            return returnValue;
        }

        private bool isAmountValid(Medicine medicine)       //ne pomeraj
        {
            bool returnValue = true;
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                medicine.Amount = Int32.Parse(AmountComboBox.Text);
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

        private bool hasStorageEnoughMedicine(Medicine medicineInStorage, Medicine requestedMedicine)       //pomerio
        {
            bool returnValue = true;
            if (!(MedicineController.Instance.hasStorageEnoughMedicine(medicineInStorage, requestedMedicine)))
            {
                MessageBox.Show("Selected amount excedes the amount located in the werehouse", "Create report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                returnValue = false;
            }
            else medicineInStorage.Amount -= requestedMedicine.Amount;
            return returnValue;
        }

        private void addDiagnosisToMedicalRecord(MedicalRecord medicalRecord)       //ne pomeraj
        {
            if (!DiagnosisBox.Text.Equals(""))
            {
                medicalRecord.ConditionList.Add(DiagnosisBox.Text);
            }
        }

        

        private void updateMedicalRecordView(Medicine medicine, MedicalReport medicalReport)        //ne pomeraj
        {
            MedicalRecordView.selectedPatientMedicineHistory.Add(medicine.MedicineData);
            MedicalRecordView.newlyCreatedReports.Add(medicalReport);
            MedicalRecordView.selectedPatientReportHistory.Add(medicalReport);
            MedicalRecordView.newlyCreatedDiagnosis.Add(DiagnosisBox.Text);
            MedicalRecordView.selectedPatientConditionHistory.Add(DiagnosisBox.Text);
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

        private void LogOutBtn(object sender, RoutedEventArgs e)       //ne pomeraj
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

        private void ReferToSpecialistBtnClick(object sender, RoutedEventArgs e)        //ne pomeraj
        {
            ReferToDoctorView referToDoctorView = new ReferToDoctorView(this, selectedPatient);
            referToDoctorView.Owner = this;
            referToDoctorView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            referToDoctorView.Show();

            this.Hide();
        }
    }
}
