using System;
using Model;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab
{
    public class MedicalReportsViewModel: ViewModel
    {

        public int Id { get; set; }
        public string Anamnesis { get; set; }
        public string Diagnosis { get; set; }
        public DateTime ReportDate { get; set; }
        public bool ReportReviewed { get; set; }
        public string ReportComment { get; set; }
        public MedicalReportsViewModel(MedicalReport medicalReport)
        {
            Id = medicalReport.Id;
            Anamnesis = medicalReport.Anamnesis;
            Diagnosis = medicalReport.Diagnosis;
            ReportDate = medicalReport.ReportDate;
            ReportReviewed = medicalReport.ReportReviewed;
            ReportComment = medicalReport.ReportComment;

        }
    }
}