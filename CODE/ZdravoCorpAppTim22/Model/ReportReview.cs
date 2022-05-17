using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;

namespace ZdravoCorpAppTim22.Model
{
    public class ReportReview : IHasID
    {
        public int Id { get; set; }
      

        public int Diagnosis { get; set; }
        public int RecommendedTherapy { get; set; }
        public int AppointmentDuration { get; set; }
       
        public int DoctorKindness { get; set; }
        public int DoctorExpertise { get; set; }
        public int DoctorDiscretion { get; set; }


        [JsonConstructor]
        public ReportReview()
        {
            
        }

        public ReportReview(int diagnosis, int recommendedTherapy, int appointmentDuration, int doctorKindness, int doctorExpertise, int doctorDiscretion)
        {
            
            Diagnosis = diagnosis;
            RecommendedTherapy = recommendedTherapy;
            AppointmentDuration = appointmentDuration;
            DoctorKindness = doctorKindness;
            DoctorExpertise = doctorExpertise;
            DoctorDiscretion = doctorDiscretion;
            
        }
    }
}