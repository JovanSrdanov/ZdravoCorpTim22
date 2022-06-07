using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class Patient : User
    {

        public bool Blocked { get; set; }

        public bool HasBeenLoggedIn { get; set; }
        public List<DateTime> SuspiciousActivity { get; set; }

        [JsonIgnore]
        public System.Collections.Generic.List<PersonalNote> personalNotes;

        [JsonIgnore]
        public System.Collections.Generic.List<PersonalNote> PersonalNotes
        {
            get
            {
                if (personalNotes == null)
                    personalNotes = new System.Collections.Generic.List<PersonalNote>();
                return personalNotes;
            }
            set
            {
                RemoveAllPersonalNote();
                if (value != null)
                {
                    foreach (PersonalNote oPersonalNote in value)
                        AddPersonalNote(oPersonalNote);
                }
            }
        }

       
        public void AddPersonalNote(PersonalNote newPersonalNote)
        {
            if (newPersonalNote == null)
                return;
            if (this.personalNotes == null)
                this.personalNotes = new System.Collections.Generic.List<PersonalNote>();
            if (!this.personalNotes.Contains(newPersonalNote))
            {
                this.personalNotes.Add(newPersonalNote);
                newPersonalNote.Patient = this;
            }
        }

        public void RemovePersonalNote(PersonalNote oldPersonalNote)
        {
            if (oldPersonalNote == null)
                return;
            if (this.personalNotes != null)
                if (this.personalNotes.Contains(oldPersonalNote))
                {
                    this.personalNotes.Remove(oldPersonalNote);
                    oldPersonalNote.Patient = null;
                }
        }

     
        public void RemoveAllPersonalNote()
        {
            if (personalNotes != null)
            {
                System.Collections.ArrayList tmpPersonalNote = new System.Collections.ArrayList();
                foreach (PersonalNote oldPersonalNote in personalNotes)
                    tmpPersonalNote.Add(oldPersonalNote);
                personalNotes.Clear();
                foreach (PersonalNote oldPersonalNote in tmpPersonalNote)
                    oldPersonalNote.Patient = null;
                tmpPersonalNote.Clear();
            }
        }















      

        [JsonConverter(typeof(HospitalReviewToIDConverter))]

        public HospitalReview hospitalReview;
        [JsonConverter(typeof(HospitalReviewToIDConverter))]

        public HospitalReview HospitalReview
        {
            get
            {
                return hospitalReview;
            }
            set
            {
                this.hospitalReview = value;
            }
        }


        [JsonConverter(typeof(MedicalRecordToIDConverter))]

        public MedicalRecord medicalRecord;
        [JsonConverter(typeof(MedicalRecordToIDConverter))]
        public MedicalRecord MedicalRecord
        {
            get
            {
                return medicalRecord;
            }
            set
            {
                if (this.medicalRecord == null || !this.medicalRecord.Equals(value))
                {
                    if (this.medicalRecord != null)
                    {
                        this.medicalRecord = null;
                    }
                    if (value != null)
                    {
                        this.medicalRecord = value;
                        this.medicalRecord.Patient = this;
                    }
                }
            }
        }

        public bool IsAvailable(Interval interval)
        {

            if (Blocked)
            {
                return false;
            }

            if (Password == null)
            {
                return true;
            }

            if (medicalAppointment == null)
                return true;
            else
            {

                foreach (MedicalAppointment medicalAppointmentPatient in MedicalAppointment)
                {
                    if (!((medicalAppointmentPatient.Interval.Start >= interval.End) || (medicalAppointmentPatient.Interval.End <= interval.Start)))
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
                this.MedicalRecord = new MedicalRecord();
            }
            else
            {
                this.MedicalRecord = medicalRecord;
            }


            if (medicalAppointment == null)
            {
                medicalAppointment = new List<MedicalAppointment>();
            }
            else
            {
                this.medicalAppointment = medicalAppointment;
            }

            Blocked = false;
            HasBeenLoggedIn = false;

            SuspiciousActivity = new List<DateTime>();

        }

        public Patient(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address, MedicalRecord medicalRecord, System.Collections.Generic.List<MedicalAppointment> medicalAppointment) : base(name, surname, email, jmbg, password, birthday, phone, gender, address)
        {
            if (medicalRecord == null)
            {
                this.MedicalRecord = new MedicalRecord();
            }
            else
            {
                this.MedicalRecord = medicalRecord;
            }


            if (medicalAppointment == null)
            {
                medicalAppointment = new List<MedicalAppointment>();
            }
            else
            {
                this.medicalAppointment = medicalAppointment;
            }
            Blocked = false;
            HasBeenLoggedIn = false;
            SuspiciousActivity = new List<DateTime>();

        }

        [JsonIgnore]
        public System.Collections.Generic.List<MedicalAppointment> medicalAppointment;
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

        public override string ToString()
        {
            return Name + " " + Surname + " " + Jmbg;
        }




    }
}