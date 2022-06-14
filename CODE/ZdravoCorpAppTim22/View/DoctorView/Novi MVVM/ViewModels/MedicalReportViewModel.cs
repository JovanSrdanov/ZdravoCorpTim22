using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class MedicalReportViewModel : ViewModel
    {
        public int Id { get; set; }
        public int DoctorId { get; set; }            //ko je napravio izvestaj
        public string Anamnesis { get; set; }
        public string Diagnosis { get; set; }
        public DateTime ReportDate { get; set; }
        public MedicalReceipt MedicalReceipt { get; set; }
        public MedicalReport MedicalReport { get; set; }
        public MedicalReportViewModel(MedicalReport medicalReport)
        {
            this.MedicalReport = medicalReport;
            this.Id = medicalReport.Id;
            this.DoctorId = medicalReport.DoctorId;
            this.Anamnesis = medicalReport.Anamnesis;
            this.Diagnosis = medicalReport.Diagnosis;
            this.ReportDate = medicalReport.ReportDate;
            this.MedicalReceipt = medicalReport.MedicalReceipt;
        }
    }
}
