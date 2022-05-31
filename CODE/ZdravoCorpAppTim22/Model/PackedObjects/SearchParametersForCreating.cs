using System;
using System.Collections.Generic;
using Model;

namespace ZdravoCorpAppTim22.Model.PackedObjects
{
    public class SearchParametersForCreating
    {
        public SearchParametersForCreating(Patient enteredPatient, AppointmentType enteredAppointmentType, DateTime appointmentTimeStart, int durationOfAppointment, DateTime workDayEndTime, List<Doctor> suggestedDoctors, List<Room> suggestedRooms)
        {
            EnteredPatient = enteredPatient;
            EnteredAppointmentType = enteredAppointmentType;
            AppointmentTimeStart = appointmentTimeStart;
            DurationOfAppointment = durationOfAppointment;
            WorkDayEndTime = workDayEndTime;
            SuggestedDoctors = suggestedDoctors;
            SuggestedRooms = suggestedRooms;
        }

        public Patient EnteredPatient { get; }
        public AppointmentType EnteredAppointmentType { get; }
        public DateTime AppointmentTimeStart { get; set; }
        public int DurationOfAppointment { get; }
        public DateTime WorkDayEndTime { get; }
        public List<Doctor> SuggestedDoctors { get; }
        public List<Room> SuggestedRooms { get; }
    }
}