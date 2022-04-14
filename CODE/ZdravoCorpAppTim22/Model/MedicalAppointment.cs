

using System;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace Model
{
    public class MedicalAppointment
    {
        public int Id { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime MedicalAppointmentStartDateTime { get; set; }
        public DateTime MedicalAppointmentEndDateTime { get; set; }

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

        [JsonConstructor]
        public MedicalAppointment()
        {
        }

        [JsonConverter(typeof(RoomToIDConverter))]
        public Room room;

        [JsonConverter(typeof(RoomToIDConverter))]
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

        [JsonConverter(typeof(PatientToIDConverter))]
        public Patient patient;

        [JsonConverter(typeof(PatientToIDConverter))]
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

        [JsonConverter(typeof(DoctorToIDConverter))]
        public Doctor doctor;

        [JsonConverter(typeof(DoctorToIDConverter))]
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