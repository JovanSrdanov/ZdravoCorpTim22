using System;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Utility;

namespace Model
{
    public class Doctor : User
    {
        public DoctorSpecialisationType DoctorType { get; set; }

        [JsonIgnore]
        public System.Collections.Generic.List<MedicalAppointment> medicalAppointment;

        [JsonConstructor]
        public Doctor()
        {
        }

        public Doctor(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, int iD, Address address, DoctorSpecialisationType doctorType, System.Collections.Generic.List<MedicalAppointment> medicalAppointment) : base(name, surname, email, jmbg, password, birthday, phone, gender, iD, address)
        {
            DoctorType = doctorType;
            this.medicalAppointment = medicalAppointment;
        }

        public Doctor(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address, DoctorSpecialisationType doctorType, System.Collections.Generic.List<MedicalAppointment> medicalAppointment) : base(name, surname, email, jmbg, password, birthday, phone, gender, address)
        {
            DoctorType = doctorType;
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

        //*********************DODAO KARTONE, NE ZABORAVI SERIJALIZACIJU**********************************
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


    }
}