using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Model;
using MVVM1;


namespace ZdravoCorpAppTim22.View.PatientView.ViewModelForMVVM
{
    class MedicalReportsViewModel :BindableBase
    {
        public Patient Patient { get; set; }
        public ObservableCollection<MedicalReport> PatientMedicalReports { get; set; }

        public static MedicalReport selectedMedicalReport;

        public MedicalReport SelectedMedicalReport {
            get
            {
                return selectedMedicalReport;
            }
            set
            {
                selectedMedicalReport = value;
                OpenReportReviewingCommand.RaiseCanExecuteChanged();
            }
        }
        public MyICommand OpenReportReviewingCommand { get; set; }

        public MedicalReportsViewModel()
        {
            OpenReportReviewingCommand = new MyICommand(OpenReportReviewing, CanOpenReportReviewing);
            Patient = ZdravoCorpTabs.LoggedPatient;
            if (Patient.MedicalRecord.MedicalReport == null)
            {
                Patient.MedicalRecord.MedicalReport = new List<MedicalReport>();
            }
            PatientMedicalReports = new ObservableCollection<MedicalReport>(Patient.MedicalRecord.MedicalReport);

            
        }

        public void OpenReportReviewing()
        {
            

        }

        private bool CanOpenReportReviewing()
        {

            return SelectedMedicalReport != null && SelectedMedicalReport.ReportReviewed == false;
        }

    }
}
