using Model;
using Repository;
using System;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class MedicalReportService : GenericService<MedicalReportRepository, MedicalReport>
    {
        private MedicalReportService() : base(MedicalReportRepository.Instance) { }
        public static MedicalReportService Instance
        {
            get
            {
                return new MedicalReportService();
            }
        }

        public void ReviewTheReport(MedicalReport medicalReport, ReportReview reportReview)
        {
            ReportReviewService.Instance.Create(reportReview);
            medicalReport.reportReview = reportReview;
            medicalReport.ReportReviewed = true;
            MedicalReportService.Instance.Update(medicalReport);
        }
    }
}
