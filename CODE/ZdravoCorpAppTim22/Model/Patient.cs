using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Model
{
    public class Patient : User
    {
        [JsonIgnore]
        public MedicalRecord medicalRecord { get; set; }
        [JsonIgnore]
        public System.Collections.Generic.List<MedicalAppointment> medicalAppointment;


        public bool IsAvailable(DateTime start, DateTime end)
        {
            if (medicalAppointment == null)
                return true;
            else
            {

                foreach (MedicalAppointment medicalAppointmentPatient in medicalAppointment)
                {
                    if (!((medicalAppointmentPatient.MedicalAppointmentStartDateTime >= end) || (medicalAppointmentPatient.MedicalAppointmentEndDateTime <= start)))
                    {
                        return false;
                    }

                }
                return true;

            }
        }


        [JsonConstructor]
        public Patient()
        {
        }

        public Patient(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, int iD, Address address, MedicalRecord medicalRecord, System.Collections.Generic.List<MedicalAppointment> medicalAppointment) : base(name, surname, email, jmbg, password, birthday, phone, gender, iD, address)
        {
            if (medicalRecord == null)
            {
                this.medicalRecord = new MedicalRecord();
            }
            else
            {
                this.medicalRecord = medicalRecord;
            }


            if (medicalAppointment == null)
            {
                medicalAppointment = new List<MedicalAppointment>();
            }
            else
            {
                this.medicalAppointment = medicalAppointment;
            }


        }

        public Patient(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address, MedicalRecord medicalRecord, System.Collections.Generic.List<MedicalAppointment> medicalAppointment) : base(name, surname, email, jmbg, password, birthday, phone, gender, address)
        {
            if (medicalRecord == null)
            {
                this.medicalRecord = new MedicalRecord();
            }
            else
            {
                this.medicalRecord = medicalRecord;
            }


            if (medicalAppointment == null)
            {
                medicalAppointment = new List<MedicalAppointment>();
            }
            else
            {
                this.medicalAppointment = medicalAppointment;
            }

        }

        [JsonIgnore]
        public System.Collections.Generic.List<MedicalAppointment> MedicalAppointment
        {
            get
            {
                if (medicalAppointment == null)
                    medicalAppointment = new System.Collections.Generic.List<MedicalAppointment>();
                return medicalAppointment;
            }
            set
            {
                RemoveAllMedicalAppointment();
                if (value != null)
                {
                    foreach (MedicalAppointment oMedicalAppointment in value)
                        AddMedicalAppointment(oMedicalAppointment);
                }
            }
        }


        public void AddMedicalAppointment(MedicalAppointment newMedicalAppointment)
        {
            if (newMedicalAppointment == null)
                return;
            if (this.medicalAppointment == null)
                this.medicalAppointment = new System.Collections.Generic.List<MedicalAppointment>();
            if (!this.medicalAppointment.Contains(newMedicalAppointment))
            {
                this.medicalAppointment.Add(newMedicalAppointment);
                newMedicalAppointment.Patient = this;
            }
        }


        public void RemoveMedicalAppointment(MedicalAppointment oldMedicalAppointment)
        {
            if (oldMedicalAppointment == null)
                return;
            if (this.medicalAppointment != null)
                if (this.medicalAppointment.Contains(oldMedicalAppointment))
                {
                    this.medicalAppointment.Remove(oldMedicalAppointment);
                    oldMedicalAppointment.Patient = null;
                }
        }


        public void RemoveAllMedicalAppointment()
        {
            if (medicalAppointment != null)
            {
                System.Collections.ArrayList tmpMedicalAppointment = new System.Collections.ArrayList();
                foreach (MedicalAppointment oldMedicalAppointment in medicalAppointment)
                    tmpMedicalAppointment.Add(oldMedicalAppointment);
                medicalAppointment.Clear();
                foreach (MedicalAppointment oldMedicalAppointment in tmpMedicalAppointment)
                    oldMedicalAppointment.Patient = null;
                tmpMedicalAppointment.Clear();
            }
        }

    }
}