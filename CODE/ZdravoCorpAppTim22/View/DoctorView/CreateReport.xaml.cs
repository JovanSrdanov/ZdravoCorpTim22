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

            //lek
            /*Medicine medicine1 = new Medicine("lek1", "1mg", "kikiriki");
            MedicineController.Instance.Create(medicine1);
            Medicine medicine2 = new Medicine("lek2", "3kg", "kokakola");
            MedicineController.Instance.Create(medicine2);
            Medicine medicine3 = new Medicine("lek3", "124mg", "secer");
            MedicineController.Instance.Create(medicine3);*/

            ObservableCollection<Medicine> medicationList = MedicineController.Instance.GetAll();
            MedicationComboBox.ItemsSource = medicationList;
            //lek
        }

        private void CreateReportClose(object sender, EventArgs e)
        {
            Application.Current.MainWindow.Show();
            this.Close();
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

            /////////////////////
            //MedicalRecord medRec = MedicalRecordController.Instance.GetByID(MedicalRecordController.Instance.GetAll().FindIndex(r => r.Id == selectedPatient.Id));
            MedicalRecord medRec = MedicalRecordController.Instance.GetAll().Where(r => r.Patient.Id == selectedPatient.Id).FirstOrDefault();
            ////////////////////

            MedicalReport medicalReport = new MedicalReport(-1, anamnesis, diagnosis, DateTime.Now,
                medRec);
            medicalReport.DoctorID = DoctorHome.selectedDoctorId;       //da bih prepoznao koji doktor je kreirao koji izvestaj, da bih kontrolisao ko moze da ga menja

            //recept
            Medicine medicine = MedicationComboBox.SelectedItem as Medicine;
            DateTime endDate = new DateTime();
            endDate = DateTime.Now;

#pragma warning disable CS0168 // Variable is declared but never used
            try
            {
                endDate = (DateTime)EndDateDatePicker.SelectedDate;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Please enter a valid number of days", "Open report", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
#pragma warning restore CS0168 // Variable is declared but never used
            string time = TimeComboBox.Text;
            string additionalInstructions = AdditionalInstructionsTextBox.Text;

            ObservableCollection<Medicine> medicineList = new ObservableCollection<Medicine>();
            medicineList.Add(medicine);

            MedicalReceipt medicalReceipt = new MedicalReceipt(endDate, time, medicineList, additionalInstructions);
            MedicalReceiptController.Instance.Create(medicalReceipt);

            medicalReport.MedicalReceipt = medicalReceipt;
            //recept

            MedicalReportController.Instance.Create(medicalReport);
            MedicalRecordView.newlyCreatedReports.Add(medicalReport);

            medRec.ConditionList.Add(diagnosis);
            medRec.MedicalReport.Add(medicalReport);
            MedicalRecordView.medRepList.Add(medicalReport);
            MedicalRecordController.Instance.Update(medRec);

            MedicalRecordView.newlyCreatedDiagnosis.Add(diagnosis);

            medicalRecordView.Show();
            this.Close();
        }
    }
}
