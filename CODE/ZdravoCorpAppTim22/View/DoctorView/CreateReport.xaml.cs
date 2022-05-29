using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class CreateReport : Window
    {
        private Patient selectedPatient;
        private MedicalRecordView medicalRecordView;

        private string diagnosis;
        public static string anamnesis;
        public CreateReport(Patient selectedPatient, MedicalRecordView medicalRecordView)
        {
            InitializeComponent();
            this.medicalRecordView = medicalRecordView;
            this.selectedPatient = selectedPatient;

            DateBlock.Text = DateTime.Now.ToShortDateString();
            NameBlock.Text = selectedPatient.Name;
            SurnameBlock.Text = selectedPatient.Surname;

            //List<Medicine> medicineList = MedicineController.Instance.GetAllFree();
            List<Medicine> medicineList = MedicineController.Instance.GetAllApproved();
            ObservableCollection<Medicine> medicineObservableList = new ObservableCollection<Medicine>(medicineList);
            ObservableCollection<MedicineData> medicineDataList = new ObservableCollection<MedicineData>(MedicineDataController.Instance.GetAll());

            MedicationComboBox.ItemsSource = medicineObservableList;
            MedicationComboBox.SelectedIndex = 0;
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

        private void ConfirmBtnClick(object sender, RoutedEventArgs e)
        {
            if (AnamnesisBox.Text == null)
            {
                anamnesis = "";
            }
            else
            {
                anamnesis = AnamnesisBox.Text;
            }

            if (DiagnosisBox.Text == null)
            {
                diagnosis = "";
            }
            else
            {
                diagnosis = DiagnosisBox.Text;
            }

            string additionalInstructions;

            if (AdditionalInstructionsTextBox.Text == null)
            {
                additionalInstructions = "";
            }
            else
            {
                additionalInstructions = AdditionalInstructionsTextBox.Text;
            }

            string therapyPurpose;

            if (PurposeComboBox.Text == null)
            {
                therapyPurpose = "";
            }
            else
            {
                therapyPurpose = PurposeComboBox.Text;
            }

            string amount;

            if (AmountComboBox.Text == null)
            {
                amount = "";
            }
            else
            {
                amount = AmountComboBox.Text;
            }

            //recept
            if (MedicationComboBox.SelectedItem == null || EndDateDatePicker.SelectedDate == null || TimeComboBox.Text == "")
            {
                MessageBox.Show("Please fill out Medication, End date and Time fields", "Create report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Medicine selectedMedicine = MedicationComboBox.SelectedItem as Medicine;
            //Medicine medicine = new Medicine(selectedMedicine);
            Medicine medicine = new Medicine();
            medicine.MedicineData = selectedMedicine.MedicineData;
#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                medicine.Amount = Int32.Parse(amount);
            }
            catch (Exception ex)
            {
                MessageBox.Show("The field 'Amount' not filled in correctly!", "Create report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
#pragma warning restore CS0168 // Variable is declared but never used

            DateTime endDate = (DateTime)EndDateDatePicker.SelectedDate;
            string time = TimeComboBox.Text;
            MedicalRecord medRec = selectedPatient.MedicalRecord;

            MedicalReport medicalReport = new MedicalReport(-1, anamnesis, diagnosis, DateTime.Now,
                medRec);
            medicalReport.DoctorID = DoctorHome.selectedDoctorId;       //da bih prepoznao koji doktor je kreirao koji izvestaj, da bih kontrolisao ko moze da ga menja

            MedicalRecordView.medicineDataObservableList.Add(medicine.MedicineData);
            MedicalReceipt medicalReceipt = new MedicalReceipt(endDate, time, medicine, additionalInstructions, therapyPurpose, medRec);
            medicine.MedicalReceipt = medicalReceipt;

            MedicineController.Instance.Create(medicine);
            if (selectedMedicine.Amount - medicine.Amount < 0)
            {
                MessageBox.Show("Selected amount excedes the amount located in the werehouse", "Create report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            else
            {
                selectedMedicine.Amount -= medicine.Amount;
            }
            MedicineController.Instance.Update(selectedMedicine);
            MedicalReceiptController.Instance.Create(medicalReceipt);

            medicalReport.MedicalReceipt = medicalReceipt;
            MedicalReportController.Instance.Create(medicalReport);
            MedicalRecordView.newlyCreatedReports.Add(medicalReport);

            if (!diagnosis.Equals(""))
            {
                medRec.ConditionList.Add(diagnosis);
            }
            medRec.MedicalReport.Add(medicalReport);
            MedicalRecordView.medRepList.Add(medicalReport);
            MedicalRecordController.Instance.Update(medRec);

            medicalReceipt.MedicalRecord = medRec;
            MedicalReceiptController.Instance.Update(medicalReceipt);

            //
            MedicineController.Instance.Update(medicine);
            //

            MedicalRecordView.newlyCreatedDiagnosis.Add(diagnosis);

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

        private void ReferToSpecialistBtnClick(object sender, RoutedEventArgs e)
        {
            ReferToDoctorView referToDoctorView = new ReferToDoctorView(this, selectedPatient);
            referToDoctorView.Owner = this;
            referToDoctorView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            referToDoctorView.Show();

            this.Hide();
        }
    }
}
