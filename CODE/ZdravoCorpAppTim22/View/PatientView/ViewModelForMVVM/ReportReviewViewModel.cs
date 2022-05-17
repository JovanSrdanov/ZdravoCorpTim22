using Model;
using MVVM1;
using System.Windows;
using Controller;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.PatientView.ViewModelForMVVM
{
    class ReportReviewViewModel : BindableBase
    {

        public MedicalReport MedicalReport { get; set; }
        public ReportReview CreateReportReview { get; set; }

        public MyICommand SubmitReviewCommand { get; set; }
        public ReportReviewViewModel()
        {
            MedicalReport = MedicalReportsViewModel.selectedMedicalReport;
            SubmitReviewCommand = new MyICommand(SubmitReview);


            if (MedicalReport.reportReview == null)
            {
                CreateReportReview = new ReportReview();
                MedicalReport.reportReview = CreateReportReview;
                ReportReviewController.Instance.Create(CreateReportReview);
                MedicalReportController.Instance.Update(MedicalReport);
            }

            else
            {
                CreateReportReview = MedicalReport.reportReview;
            }


            SetAll();
        }
        public void SubmitReview()
        {
            CreateReportReview.Diagnosis = DiagnosisCheck();
            CreateReportReview.RecommendedTherapy = RecommendedTherapyCheck();
            CreateReportReview.AppointmentDuration = AppointmentDurationCheck();

            CreateReportReview.DoctorKindness = DoctorKindnessCheck();
            CreateReportReview.DoctorExpertise = DoctorExpertiseCheck();
            CreateReportReview.DoctorDiscretion = DoctorDiscretionCheck();

            MedicalReport.reportReview = CreateReportReview;
            MedicalReport.ReportReviewed = true;

            ReportReviewController.Instance.Update(CreateReportReview);
            MedicalReportController.Instance.Update(MedicalReport);
            MessageBox.Show("Pregled i lekar uspešno ocenjenjeni!");
        }



        private void SetAll()
        {
            DiagnosisSet();
            RecommendedTherapySet();
            AppointmentDurationSet();

            DoctorKindnessSet();
            DoctorExpertiseSet();
            DoctorDiscretionSet();
        }


        public bool DiagnosisGroup1 { get; set; }


        public bool DiagnosisGroup2 { get; set; }


        public bool DiagnosisGroup3 { get; set; }


        public bool DiagnosisGroup4 { get; set; }


        public bool DiagnosisGroup5 { get; set; }

        private void DiagnosisSet()
        {
            DiagnosisGroup1 = true;
        }

        private int DiagnosisCheck()
        {
            if (DiagnosisGroup1)
            {
                return 1;
            }
            if (DiagnosisGroup2)
            {
                return 2;
            }
            if (DiagnosisGroup3)
            {
                return 3;
            }
            if (DiagnosisGroup4)
            {
                return 4;
            }
            if (DiagnosisGroup5)
            {
                return 5;
            }

            return -1;
        }


        public bool RecommendedTherapyGroup1 { get; set; }

        public bool RecommendedTherapyGroup2 { get; set; }


        public bool RecommendedTherapyGroup3 { get; set; }


        public bool RecommendedTherapyGroup4 { get; set; }


        public bool RecommendedTherapyGroup5 { get; set; }

        private int RecommendedTherapyCheck()
        {
            if (RecommendedTherapyGroup1)
            {
                return 1;
            }
            if (RecommendedTherapyGroup2)
            {
                return 2;
            }
            if (RecommendedTherapyGroup3)
            {
                return 3;
            }
            if (RecommendedTherapyGroup4)
            {
                return 4;
            }
            if (RecommendedTherapyGroup5)
            {
                return 5;
            }

            return -1;
        }


        private void RecommendedTherapySet()
        {

            RecommendedTherapyGroup1 = true;


        }



        public bool AppointmentDurationGroup1 { get; set; }

        public bool AppointmentDurationGroup2 { get; set; }


        public bool AppointmentDurationGroup3 { get; set; }


        public bool AppointmentDurationGroup4 { get; set; }


        public bool AppointmentDurationGroup5 { get; set; }

        private int AppointmentDurationCheck()
        {
            if (AppointmentDurationGroup1)
            {
                return 1;
            }
            if (AppointmentDurationGroup2)
            {
                return 2;
            }
            if (AppointmentDurationGroup3)
            {
                return 3;
            }
            if (AppointmentDurationGroup4)
            {
                return 4;
            }
            if (AppointmentDurationGroup5)
            {
                return 5;
            }

            return -1;
        }


        private void AppointmentDurationSet()
        {

            AppointmentDurationGroup1 = true;


        }

        public bool DoctorKindnessGroup1 { get; set; }

        public bool DoctorKindnessGroup2 { get; set; }


        public bool DoctorKindnessGroup3 { get; set; }


        public bool DoctorKindnessGroup4 { get; set; }


        public bool DoctorKindnessGroup5 { get; set; }

        private int DoctorKindnessCheck()
        {
            if (DoctorKindnessGroup1)
            {
                return 1;
            }
            if (DoctorKindnessGroup2)
            {
                return 2;
            }
            if (DoctorKindnessGroup3)
            {
                return 3;
            }
            if (DoctorKindnessGroup4)
            {
                return 4;
            }
            if (DoctorKindnessGroup5)
            {
                return 5;
            }

            return -1;
        }


        private void DoctorKindnessSet()
        {

            DoctorKindnessGroup1 = true;

        }



        public bool DoctorExpertiseGroup1 { get; set; }

        public bool DoctorExpertiseGroup2 { get; set; }


        public bool DoctorExpertiseGroup3 { get; set; }


        public bool DoctorExpertiseGroup4 { get; set; }


        public bool DoctorExpertiseGroup5 { get; set; }

        private int DoctorExpertiseCheck()
        {
            if (DoctorExpertiseGroup1)
            {
                return 1;
            }
            if (DoctorExpertiseGroup2)
            {
                return 2;
            }
            if (DoctorExpertiseGroup3)
            {
                return 3;
            }
            if (DoctorExpertiseGroup4)
            {
                return 4;
            }
            if (DoctorExpertiseGroup5)
            {
                return 5;
            }

            return -1;
        }


        private void DoctorExpertiseSet()
        {

            DoctorExpertiseGroup1 = true;

        }


        public bool DoctorDiscretionGroup1 { get; set; }

        public bool DoctorDiscretionGroup2 { get; set; }


        public bool DoctorDiscretionGroup3 { get; set; }


        public bool DoctorDiscretionGroup4 { get; set; }


        public bool DoctorDiscretionGroup5 { get; set; }

        private int DoctorDiscretionCheck()
        {
            if (DoctorDiscretionGroup1)
            {
                return 1;
            }
            if (DoctorDiscretionGroup2)
            {
                return 2;
            }
            if (DoctorDiscretionGroup3)
            {
                return 3;
            }
            if (DoctorDiscretionGroup4)
            {
                return 4;
            }
            if (DoctorDiscretionGroup5)
            {
                return 5;
            }

            return -1;
        }


        private void DoctorDiscretionSet()
        {
            DoctorDiscretionGroup1 = true;
        }




    }
}
