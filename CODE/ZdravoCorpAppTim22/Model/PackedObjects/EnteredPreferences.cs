using System;
using Model;

namespace ZdravoCorpAppTim22.Model.PackedObjects
{
    public class EnteredPreferences
    {
        public EnteredPreferences(Patient enteredPatient, DateTime enteredDateTime, AppointmentType enteredAppointmentType, string enteredPriority, Doctor enteredDoctor)
        {
            EnteredPatient = enteredPatient;
            EnteredDateTime = enteredDateTime;
            EnteredAppointmentType = enteredAppointmentType;
            EnteredPriority = enteredPriority;
            EnteredDoctor = enteredDoctor;
        }

      

      
        public Patient EnteredPatient { get; }
        public DateTime DateTime { get; }
        public DateTime EnteredDateTime { get; }
      
        public AppointmentType EnteredAppointmentType { get; }
       
        public string EnteredPriority { get; }
        public Doctor Doctor { get; }
        public Doctor EnteredDoctor { get; }
    }
}