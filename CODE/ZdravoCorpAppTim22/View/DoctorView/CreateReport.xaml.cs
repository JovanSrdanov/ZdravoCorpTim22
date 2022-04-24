using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class CreateReport : Window
    {
        private Patient selectedPatient;
        private MedicalRecordView medicalRecordView;

        private string diagnosis;
        private string anamnesis;

        private ObservableCollection<string> medicineNameList;
        private ObservableCollection<string> medicineAmountList;
        private ObservableCollection<string> medicineInstructionList;

        //konstruktor za kreiranje izvestaja
        public CreateReport(Patient selectedPatient, MedicalRecordView medicalRecordView)
        {
            InitializeComponent();
            this.medicalRecordView = medicalRecordView;
            this.selectedPatient = selectedPatient;

            DateBlock.Text = DateTime.Now.ToShortDateString();
            NameBlock.Text = selectedPatient.Name;
            SurnameBlock.Text = selectedPatient.Surname;

            medicineNameList = new ObservableCollection<string>();
            medicineAmountList = new ObservableCollection<string>();
            medicineInstructionList = new ObservableCollection<string>();
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

            medicineNameList.Add("");
            medicineAmountList.Add("");
            medicineInstructionList.Add("");

            MedicalReport medicalReport = new MedicalReport(1, anamnesis, diagnosis, medicineNameList, medicineAmountList, medicineInstructionList, DateTime.Now,
                selectedPatient.medicalRecord);
            medicalReport.DoctorID = DoctorHome.selectedDoctorId;       //da bih prepoznao koji doktor je kreirao koji izvestaj, da bih kontrolisao ko moze da ga menja
            selectedPatient.medicalRecord.ConditionList.Add(diagnosis);
            selectedPatient.medicalRecord.medicalReport.Add(medicalReport);

            medicalRecordView.Show();
            this.Close();
        }
    }
}
