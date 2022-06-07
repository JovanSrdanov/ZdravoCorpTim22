using Model;
using System;
using ZdravoCorpAppTim22.Model.Utility;


namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
    public class MedicalAppointmentsViewModel : ViewModel
    {

        public int Id { get; set; }
        public AppointmentType Type { get; set; }
        public Interval Interval { get; set; }
        public AppointmentRoomViewModel Room { get; set; }
        public AppointmentPatientViewModel Patient { get; set; }
        public AppointmentDoctorViewModel Doctor { get; set; }

        public DateTime MaxDaysForward { get; set; }
        public DateTime MaxDaysBackward { get; set; }

        public MedicalAppointmentsViewModel(int id, AppointmentType type, Interval interval, AppointmentRoomViewModel room, AppointmentPatientViewModel patient, AppointmentDoctorViewModel doctor)
        {
            Id = id;
            Type = type;
            Interval = interval;
            Room = room;
            Patient = patient;
            Doctor = doctor;

            MaxDaysForward = interval.Start.Date.AddDays(Constants.Constants.MAX_DAYS_IN_ADVANCE);

            MaxDaysBackward = interval.Start.Date.AddDays(-2);

            if (DateTime.Now.AddDays(4).Date > interval.Start.Date)
            {

                MaxDaysBackward = interval.Start.Date.AddDays(-1);
            }
            if (DateTime.Now.AddDays(3).Date > interval.Start.Date)
            {

                MaxDaysBackward = interval.Start.Date;
            }

        }

    }
}