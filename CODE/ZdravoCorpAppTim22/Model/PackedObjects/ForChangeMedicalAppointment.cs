using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.Model.PackedObjects
{
    public class ForChangeMedicalAppointment
    {
        public ForChangeMedicalAppointment(Doctor doctor, Room room, Patient enteredPatient, DateTime selectedDateTime, AppointmentType type)
        {
            Doctor = doctor;
            Room = room;
            EnteredPatient = enteredPatient;
            SelectedDateTime = selectedDateTime;
            Type = type;
        }

        public Doctor Doctor { get; }
        public Room Room { get; }
        public Patient EnteredPatient { get; }
        public DateTime SelectedDateTime { get; }
        public AppointmentType Type { get; }
    }
}
