using Model;
using System;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    internal class MedicalReportController : GenericController<MedicalReportService, MedicalReport>
    {
        private static MedicalReportController instance;
        private MedicalReportController() : base(MedicalReportService.Instance) { }
        public static MedicalReportController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalReportController();
                }
                return instance;
            }
        }

        public void ReviewTheReport(int medicalReportId, int diagnosis, int recommendedTherapy, int appointmentDuration, int doctorKindness, int doctorExpertise, int doctorDiscretion)
        {
            MedicalReport medicalReport = Instance.GetByID(medicalReportId);

            ReportReview reportReview = new ReportReview(diagnosis, recommendedTherapy, appointmentDuration,
                doctorKindness, doctorExpertise, doctorDiscretion); 

            MedicalReportService.Instance.ReviewTheReport(medicalReport, reportReview);

        }
    }
}
