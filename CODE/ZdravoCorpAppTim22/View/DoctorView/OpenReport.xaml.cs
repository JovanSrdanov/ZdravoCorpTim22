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
    /// <summary>
    /// Interaction logic for OpenReport.xaml
    /// </summary>
    public partial class OpenReport : Window
    {
        private MedicalReport selectedMedicalReport;
        private MedicalRecordView medicalRecordView;
        public OpenReport(MedicalReport medicalReport, MedicalRecordView medicalRecordView)
        {
            InitializeComponent();
            selectedMedicalReport = medicalReport;
            this.medicalRecordView = medicalRecordView;

            DateBlock.Text = selectedMedicalReport.ReportDate.ToShortDateString();
            NameBlock.Text = selectedMedicalReport.MedicalRecord.Patient.Name;
            SurnameBlock.Text = selectedMedicalReport.MedicalRecord.Patient.Surname;
            DiagnosisBox.Text = selectedMedicalReport.Diagnosis;
            AnamnesisBox.Text = selectedMedicalReport.Anamnesis;

            if (!isEditable())
            {
                ChangeReportBtn.IsEnabled = false;
            }
        }

        private bool isEditable()
        {
            if (selectedMedicalReport.DoctorID == DoctorHome.selectedDoctorId)
            {
                return true;
            }
            else
            {
                return false;
            }
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

            medicalRecordView.Show();
            this.Close();
        }
    }
}
