using Model;
using System;

namespace ZdravoCorpAppTim22.DTO
{
    public class MedicalAppointmentDTO
    {
        public MedicalAppointmentDTO(DateTime? startDate, string startTime, Object appointmentType)
        {
            StartDate = startDate;
            StartTime = startTime;
            AppointmentType = appointmentType;
        }
        public DateTime? StartDate { get; set; }
        public string StartTime { get; set; }
        public Object AppointmentType { get; set; }


    }
}
