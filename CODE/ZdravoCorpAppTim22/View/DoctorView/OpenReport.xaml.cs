using Controller;
using Model;
using System;
using System.Collections.Generic;
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
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class OpenReport : Window
    {
        private MedicalReport selectedMedicalReport;
        private MedicalRecordView medicalRecordView;

        private string oldDiagnosis;
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

            oldDiagnosis = DiagnosisBox.Text;
            this.canCreateRecord = canCreateRecord;

            if (!isEditable())
            {
                ChangeReportBtn.IsEnabled = false;
                ChangeReportBtn.Foreground = new SolidColorBrush(Colors.Black);
            }
        }

        private bool isEditable()
        {
            return selectedMedicalReport.DoctorID == DoctorHome.selectedDoctorId;
        }

        //mozda mora da se ponovo doda event
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

            MedicalRecord medRec = MedicalRecordController.Instance.GetByID(MedicalRecordController.Instance.GetAll().
                FindIndex(r => r.Patient.Id == MedicalRecordView.selectedPatient.Id));

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

            MedicalReportController.Instance.Update(selectedMedicalReport);
            //MedicalRecordController.Instance.Update(medRec);

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
                    break;
                }
            }
            medicalRecordView.Show();
            this.Close();
        }
    }
}
