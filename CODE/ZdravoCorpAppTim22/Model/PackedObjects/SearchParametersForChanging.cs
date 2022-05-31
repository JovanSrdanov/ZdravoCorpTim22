using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.Model.PackedObjects
{
    class SearchParametersForChanging
    {
        public SearchParametersForChanging(ForChangeMedicalAppointment forChangeMedicalAppointment, DateTime appointmentTimeStart, int durationOfAppointment, DateTime workDayEndTime, int jumpToNextAppointmetnTime)
        {
            ForChangeMedicalAppointment = forChangeMedicalAppointment;
            AppointmentTimeStart = appointmentTimeStart;
            DurationOfAppointment = durationOfAppointment;
            WorkDayEndTime = workDayEndTime;
            JumpToNextAppointmetnTime = jumpToNextAppointmetnTime;
        }

        public ForChangeMedicalAppointment ForChangeMedicalAppointment { get; }
        public DateTime AppointmentTimeStart { get; set; }
        public int DurationOfAppointment { get; }
        public DateTime WorkDayEndTime { get; }
        public int JumpToNextAppointmetnTime { get; }
    }
}
