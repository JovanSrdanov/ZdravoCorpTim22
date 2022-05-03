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
        private Medicine oldMedicine;
        private int canCreateRecord;        //ako ne pravim novi izvestaj cuvam promene kod dijagnoze

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

            MedicationComboBox.ItemsSource = MedicineController.Instance.GetAll();

            MedicationComboBox.SelectedValuePath = "Id";
            MedicationComboBox.SelectedValue = selectedMedicalReport.MedicalReceipt.Medicine.Id;

            //MedicationComboBox.SelectedItem = selectedMedicalReport.MedicalReceipt.Medicine[0];
            //MedicationComboBox.ItemsSource = selectedMedicalReport.MedicalReceipt.Medicine;
            //MedicationComboBox.SelectedIndex = 0;
            EndDateDatePicker.Text = selectedMedicalReport.MedicalReceipt.EndDate.ToString();
            TimeComboBox.Text = selectedMedicalReport.MedicalReceipt.Time;
            AdditionalInstructionsTextBox.Text = selectedMedicalReport.MedicalReceipt.AdditionalInstructions;
            PurposeComboBox.Text = selectedMedicalReport.MedicalReceipt.TherapyPurpose;

            oldDiagnosis = DiagnosisBox.Text;
            oldMedicine = MedicationComboBox.SelectedItem as Medicine;
            this.canCreateRecord = canCreateRecord;

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

        private void OpenReportClosed(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
        }

        private void ChangeReportClick(object sender, RoutedEventArgs e)
        {
            //treba azurirati za serijalizaciju

            //MedicalRecord medRec = MedicalRecordController.Instance.GetByID(MedicalRecordController.Instance.GetAll().FindIndex(r => r.Patient.Id == MedicalRecordView.selectedPatient.Id));
            MedicalRecord medRec = MedicalRecordController.Instance.GetAll().Where(r => r.Patient.Id == MedicalRecordView.selectedPatient.Id).FirstOrDefault();

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

            //dodao
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
                selectedMedicalReport.MedicalReceipt.Medicine = MedicationComboBox.SelectedItem as Medicine;
                /*if (selectedMedicalReport.MedicalRecord.MedicalReport.IndexOf(selectedMedicalReport) ==
                    selectedMedicalReport.MedicalRecord.MedicalReport.Count - 1)        //ako menjam poslednji izvestaj u kartonu
                {
                    MedicalRecordView.medicineObservableList.Clear();
                    MedicalRecordView.medicineObservableList.Add(MedicationComboBox.SelectedItem as Medicine);
                }*/
                selectedMedicalReport.MedicalReceipt.EndDate = (DateTime)EndDateDatePicker.SelectedDate;
                selectedMedicalReport.MedicalReceipt.Time = TimeComboBox.Text;
                selectedMedicalReport.MedicalReceipt.TherapyPurpose = PurposeComboBox.Text;
            }
            

            MedicalReceiptController.Instance.Update(selectedMedicalReport.MedicalReceipt);
            //dodao

            MedicalReportController.Instance.Update(selectedMedicalReport);

            foreach (string diagnosis in medRec.ConditionList)
            {
                if (diagnosis == oldDiagnosis)         //ako kreiram novi izvestaj pritiskom na back sve ponistavam, u suprotnom cuvam
                {                                                               //promenu dijagnoze
                    /*if (canCreateRecord != -1)
                    {
                        MedicalRecordView.newlyCreatedDiagnosis[MedicalRecordView.newlyCreatedDiagnosis.IndexOf(diagnosis)] = DiagnosisBox.Text;
                    }*/
                    medRec.ConditionList[medRec.ConditionList.IndexOf(diagnosis)] = DiagnosisBox.Text;
                    MedicalRecordController.Instance.Update(medRec);
                    //ovde bi update-ovao za pacijenta karton
                    break;
                }
            }

            MedicalRecordView.medicineObservableList[MedicalRecordView.medRepList.IndexOf(selectedMedicalReport)] = 
                MedicationComboBox.SelectedItem as Medicine;

            /*foreach (Medicine medicine in MedicalRecordView.medicineObservableList)
            {
                if (medicine.Id == oldMedicine.Id)
                {
                    int idx = MedicalRecordView.medicineObservableList.IndexOf(medicine);
                    MedicalRecordView.medicineObservableList[idx] =
                        MedicationComboBox.SelectedItem as Medicine;
                    medRec.MedicalReceipt[medRec.MedicalReceipt.IndexOf(selectedMedicalReport.MedicalReceipt)].Medicine = 
                        MedicationComboBox.SelectedItem as Medicine;
                    break;
                }
            }*/
            medicalRecordView.Show();
            this.Close();
        }
    }
}
