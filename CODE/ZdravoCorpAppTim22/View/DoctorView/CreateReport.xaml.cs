using Controller;
using Model;
using System;
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
        private string anamnesis;

        //konstruktor za kreiranje izvestaja
        public CreateReport(Patient selectedPatient, MedicalRecordView medicalRecordView)
        {
            InitializeComponent();
            this.medicalRecordView = medicalRecordView;
            this.selectedPatient = selectedPatient;

            DateBlock.Text = DateTime.Now.ToShortDateString();
            NameBlock.Text = selectedPatient.Name;
            SurnameBlock.Text = selectedPatient.Surname;

            ObservableCollection<Medicine> medicationList = MedicineController.Instance.GetAll();
            MedicationComboBox.ItemsSource = medicationList;
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

            //recept
            if (MedicationComboBox.SelectedItem == null || EndDateDatePicker.SelectedDate == null || TimeComboBox.Text == "")
            {
                MessageBox.Show("Please fill out Medication, End date and Time fields", "Create report",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            Medicine medicine = MedicationComboBox.SelectedItem as Medicine;
            DateTime endDate = (DateTime)EndDateDatePicker.SelectedDate;
            string time = TimeComboBox.Text;
            MedicalRecord medRec = selectedPatient.MedicalRecord;

            MedicalReport medicalReport = new MedicalReport(-1, anamnesis, diagnosis, DateTime.Now,
                medRec);
            medicalReport.DoctorID = DoctorHome.selectedDoctorId;       //da bih prepoznao koji doktor je kreirao koji izvestaj, da bih kontrolisao ko moze da ga menja

            ObservableCollection<Medicine> medicineList = new ObservableCollection<Medicine>();
            medicineList.Add(medicine);

            MedicalRecordView.medicineObservableList.Add(medicine);
            MedicalReceipt medicalReceipt = new MedicalReceipt(endDate, time, medicine, additionalInstructions, therapyPurpose, medRec);
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
    }
}
