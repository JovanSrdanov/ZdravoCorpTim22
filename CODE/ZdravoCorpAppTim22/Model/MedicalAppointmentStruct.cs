using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.Model
{
    public class MedicalAppointmentStruct
    {
        public int Id { get; set; }
        public AppointmentType Type { get; set; }
        public Interval Interval { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Room Room { get; set; }

        public MedicalAppointmentStruct(int id, AppointmentType type, Interval interval, Patient patient, Doctor doctor, Room room)
        {
            Id = id;
            Type = type;
            Interval = interval;
            Patient = patient;
            Doctor = doctor;
            Room = room;
        }
    }
}
