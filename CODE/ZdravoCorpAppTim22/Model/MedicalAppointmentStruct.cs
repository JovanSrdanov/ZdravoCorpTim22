using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.Model
{
    public class MedicalAppointmentStruct
    {
        public int Id { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime MedicalAppointmentStartDateTime { get; set; }
        public DateTime MedicalAppointmentEndDateTime { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Room Room { get; set; }

        public MedicalAppointmentStruct(int id, AppointmentType type, DateTime medicalAppointmentStartDateTime, DateTime medicalAppointmentEndDateTime, Patient patient, Doctor doctor, Room room)
        {
            Id = id;
            Type = type;
            MedicalAppointmentStartDateTime = medicalAppointmentStartDateTime;
            MedicalAppointmentEndDateTime = medicalAppointmentEndDateTime;
            Patient = patient;
            Doctor = doctor;
            Room = room;
        }
        



    }
}
