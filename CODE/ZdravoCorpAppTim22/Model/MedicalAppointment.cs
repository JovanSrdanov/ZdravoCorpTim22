

using System;
using System.Text.Json.Serialization;

namespace Model
{
    public class MedicalAppointment
    {
        public int Id { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime MedicalAppointmentStartDateTime { get; set; }
        public DateTime MedicalAppointmentEndDateTime { get; set; }

        [JsonIgnore]
        public Room room;

        public int DoctorID { get; set; }
        public int PatientID { get; set; }
        public int RoomID { get; set; }


        //public MedicalAppointment(int id, AppointmentType type, DateTime date, Room room, Patient patient, Doctor doctor, int duration)
        public MedicalAppointment(int id, AppointmentType type, DateTime medicalAppointmentStartDateTime, DateTime medicalAppointmentEndDateTime, Room room, Patient patient, Doctor doctor)
        {
            Id = id;
            Type = type;
            MedicalAppointmentStartDateTime = medicalAppointmentStartDateTime;
            MedicalAppointmentEndDateTime = medicalAppointmentEndDateTime;
            Room = room;
            Patient = patient;
            Doctor = doctor;
        }

        [JsonIgnore]
        public Room room;
        
        [JsonIgnore]
        public Room Room
        {
            get
            {
                return room;
            }
            set
            {
                if (this.room == null || !this.room.Equals(value))
                {
                    if (this.room != null)
                    {
                        Room oldRoom = this.room;
                        this.room = null;
                        oldRoom.RemoveMedicalAppointment(this);
                    }
                    if (value != null)
                    {
                        this.room = value;
                        this.room.AddMedicalAppointment(this);
                    }
                }
            }
        }

        [JsonIgnore]
        public Patient patient;


        [JsonIgnore]
        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                if (this.patient == null || !this.patient.Equals(value))
                {
                    if (this.patient != null)
                    {
                        Patient oldPatient = this.patient;
                        this.patient = null;
                        oldPatient.RemoveMedicalAppointment(this);
                    }
                    if (value != null)
                    {
                        this.patient = value;
                        this.patient.AddMedicalAppointment(this);
                    }
                }
            }
        }

        [JsonIgnore]
        public Doctor doctor;

        [JsonConstructor]
        public MedicalAppointment()
        {
        }

        [JsonIgnore]
        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                if (this.doctor == null || !this.doctor.Equals(value))
                {
                    if (this.doctor != null)
                    {
                        Doctor oldDoctor = this.doctor;
                        this.doctor = null;
                        oldDoctor.RemoveMedicalAppointment(this);
                    }
                    if (value != null)
                    {
                        this.doctor = value;
                        this.doctor.AddMedicalAppointment(this);
                    }
                }
            }
        }

    }
}