

using System;

namespace Model
{
    public class MedicalAppointment
    {
        public int Id { get; set; }
        public AppointmentType Type { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }

        public Room room;

        public MedicalAppointment(int id, AppointmentType type, DateTime date, Room room, Patient patient, Doctor doctor, int duration)
        {
            Id = id;
            Type = type;
            Date = date;
            Room = room;
            Patient = patient;
            Doctor = doctor;
            Duration = duration;
        }

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
        public Patient patient;

     
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
        public Doctor doctor;

        public MedicalAppointment()
        {
        }

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