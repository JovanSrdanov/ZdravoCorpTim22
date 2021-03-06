using System;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace Model
{
    public class Doctor : User
    {

        [JsonIgnore]
        public System.Collections.Generic.List<MedicalAppointment> medicalAppointment;

        [JsonConstructor]
        public Doctor()
        {
        }

        public Doctor(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, int iD, Address address, DoctorSpecialization doctorType, System.Collections.Generic.List<MedicalAppointment> medicalAppointment) : base(name, surname, email, jmbg, password, birthday, phone, gender, iD, address)
        {
            DoctorSpecialization = doctorType;
            this.medicalAppointment = medicalAppointment;
        }

        public Doctor(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address, DoctorSpecialization doctorType, System.Collections.Generic.List<MedicalAppointment> medicalAppointment) : base(name, surname, email, jmbg, password, birthday, phone, gender, address)
        {
            DoctorSpecialization = doctorType;
            this.medicalAppointment = medicalAppointment;
        }


        public bool IsAvailable(Interval interval)
        {
            if (medicalAppointment == null)
                return true;
            else
            {

                foreach (MedicalAppointment medicalAppointmentDoctor in MedicalAppointment)
                {
                    if (!((medicalAppointmentDoctor.Interval.Start >= interval.End) || (medicalAppointmentDoctor.Interval.End <= interval.Start)))

                    {
                        return false;
                    }

                }
                return true;

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
                newMedicalAppointment.Doctor = this;
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
                    oldMedicalAppointment.Doctor = null;
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
                    oldMedicalAppointment.Doctor = null;
                tmpMedicalAppointment.Clear();
            }
        }
        public override string ToString()
        {
            return Name + " " + Surname + " " + Jmbg;
        }

        public System.Collections.Generic.List<MedicalRecord> medicalRecord;

        public System.Collections.Generic.List<MedicalRecord> MedicalRecord
        {
            get
            {
                if (medicalRecord == null)
                    medicalRecord = new System.Collections.Generic.List<MedicalRecord>();
                return medicalRecord;
            }
            set
            {
                RemoveAllMedicalRecord();
                if (value != null)
                {
                    foreach (MedicalRecord oMedicalRecord in value)
                        AddMedicalRecord(oMedicalRecord);
                }
            }
        }

        public void AddMedicalRecord(MedicalRecord newMedicalRecord)
        {
            if (newMedicalRecord == null)
                return;
            if (this.medicalRecord == null)
                this.medicalRecord = new System.Collections.Generic.List<MedicalRecord>();
            if (!this.medicalRecord.Contains(newMedicalRecord))
                this.medicalRecord.Add(newMedicalRecord);
        }

        public void RemoveMedicalRecord(MedicalRecord oldMedicalRecord)
        {
            if (oldMedicalRecord == null)
                return;
            if (this.medicalRecord != null)
                if (this.medicalRecord.Contains(oldMedicalRecord))
                    this.medicalRecord.Remove(oldMedicalRecord);
        }

        public void RemoveAllMedicalRecord()
        {
            if (medicalRecord != null)
                medicalRecord.Clear();
        }

        [JsonIgnore]
        public System.Collections.Generic.List<RequestForAbsence> requestForAbsence;

        [JsonIgnore]
        public System.Collections.Generic.List<RequestForAbsence> RequestForAbsence
        {
            get
            {
                if (requestForAbsence == null)
                    requestForAbsence = new System.Collections.Generic.List<RequestForAbsence>();
                return requestForAbsence;
            }
            set
            {
                RemoveAllRequestForAbsence();
                if (value != null)
                {
                    foreach (RequestForAbsence oRequestForAbsence in value)
                        AddRequestForAbsence(oRequestForAbsence);
                }
            }
        }

        public void AddRequestForAbsence(RequestForAbsence newRequestForAbsence)
        {
            if (newRequestForAbsence == null)
                return;
            if (this.requestForAbsence == null)
                this.requestForAbsence = new System.Collections.Generic.List<RequestForAbsence>();
            if (!this.requestForAbsence.Contains(newRequestForAbsence))
            {
                this.requestForAbsence.Add(newRequestForAbsence);
                newRequestForAbsence.Doctor = this;
            }
        }

        public void RemoveRequestForAbsence(RequestForAbsence oldRequestForAbsence)
        {
            if (oldRequestForAbsence == null)
                return;
            if (this.requestForAbsence != null)
                if (this.requestForAbsence.Contains(oldRequestForAbsence))
                {
                    this.requestForAbsence.Remove(oldRequestForAbsence);
                    oldRequestForAbsence.Doctor = null;
                }
        }

        public void RemoveAllRequestForAbsence()
        {
            if (requestForAbsence != null)
            {
                System.Collections.ArrayList tmpRequestForAbsence = new System.Collections.ArrayList();
                foreach (RequestForAbsence oldRequestForAbsence in requestForAbsence)
                    tmpRequestForAbsence.Add(oldRequestForAbsence);
                requestForAbsence.Clear();
                foreach (RequestForAbsence oldRequestForAbsence in tmpRequestForAbsence)
                    oldRequestForAbsence.Doctor = null;
                tmpRequestForAbsence.Clear();
            }
        }

        [JsonConverter(typeof(DoctorSerializationToIDConverter))]
        public DoctorSpecialization DoctorSpecialization { get; set; }
    }
}