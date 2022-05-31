using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace Model
{
    public class MedicalRecord : IHasID
    {
        public int Id { get; set; }
        public BloodType BloodType { get; set; }

        public List<string> AllergiesList { get; set; }
        public List<string> ConditionList { get; set; }


        [JsonIgnore]
        public System.Collections.Generic.List<MedicalReceipt> medicalReceipt;
        [JsonIgnore]
        public System.Collections.Generic.List<MedicalReceipt> MedicalReceipt
        {
            get
            {
                if (medicalReceipt == null)
                    medicalReceipt = new System.Collections.Generic.List<MedicalReceipt>();
                return medicalReceipt;
            }
            set
            {
                RemoveAllMedicalReceipt();
                if (value != null)
                {
                    foreach (MedicalReceipt oMedicalReceipt in value)
                        AddMedicalReceipt(oMedicalReceipt);
                }
            }
        }

        public void AddMedicalReceipt(MedicalReceipt newMedicalReceipt)
        {
            if (newMedicalReceipt == null)
                return;
            if (this.medicalReceipt == null)
                this.medicalReceipt = new System.Collections.Generic.List<MedicalReceipt>();
            if (!this.medicalReceipt.Contains(newMedicalReceipt))
            {
                this.medicalReceipt.Add(newMedicalReceipt);
                newMedicalReceipt.MedicalRecord = this;
            }
        }

        public void RemoveMedicalReceipt(MedicalReceipt oldMedicalReceipt)
        {
            if (oldMedicalReceipt == null)
                return;
            if (this.medicalReceipt != null)
                if (this.medicalReceipt.Contains(oldMedicalReceipt))
                {
                    this.medicalReceipt.Remove(oldMedicalReceipt);
                    oldMedicalReceipt.MedicalRecord = null;
                }
        }

        public void RemoveAllMedicalReceipt()
        {
            if (medicalReceipt != null)
            {
                System.Collections.ArrayList tmpMedicalReceipt = new System.Collections.ArrayList();
                foreach (MedicalReceipt oldMedicalReceipt in medicalReceipt)
                    tmpMedicalReceipt.Add(oldMedicalReceipt);
                medicalReceipt.Clear();
                foreach (MedicalReceipt oldMedicalReceipt in tmpMedicalReceipt)
                    oldMedicalReceipt.MedicalRecord = null;
                tmpMedicalReceipt.Clear();
            }
        }

        [JsonIgnore]
        public System.Collections.Generic.List<MedicalReport> medicalReport;
        [JsonIgnore]
        public System.Collections.Generic.List<MedicalReport> MedicalReport
        {
            get
            {
                if (medicalReport == null)
                    medicalReport = new System.Collections.Generic.List<MedicalReport>();
                return medicalReport;
            }
            set
            {
                RemoveAllMedicalReport();
                if (value != null)
                {
                    foreach (MedicalReport oMedicalReport in value)
                        AddMedicalReport(oMedicalReport);
                }
            }
        }

        public void AddMedicalReport(MedicalReport newMedicalReport)
        {
            if (newMedicalReport == null)
                return;
            if (this.medicalReport == null)
                this.medicalReport = new System.Collections.Generic.List<MedicalReport>();
            if (!this.medicalReport.Contains(newMedicalReport))
            {
                this.medicalReport.Add(newMedicalReport);
                newMedicalReport.MedicalRecord = this;
            }
        }

        public void RemoveMedicalReport(MedicalReport oldMedicalReport)
        {
            if (oldMedicalReport == null)
                return;
            if (this.medicalReport != null)
                if (this.medicalReport.Contains(oldMedicalReport))
                {
                    this.medicalReport.Remove(oldMedicalReport);
                    oldMedicalReport.MedicalRecord = null;
                }
        }

        public void RemoveAllMedicalReport()
        {
            if (medicalReport != null)
            {
                System.Collections.ArrayList tmpMedicalReport = new System.Collections.ArrayList();
                foreach (MedicalReport oldMedicalReport in medicalReport)
                    tmpMedicalReport.Add(oldMedicalReport);
                medicalReport.Clear();
                foreach (MedicalReport oldMedicalReport in tmpMedicalReport)
                    oldMedicalReport.MedicalRecord = null;
                tmpMedicalReport.Clear();
            }
        }

        [JsonIgnore]
        public Patient Patient { get; set; }

        [JsonConstructor]
        public MedicalRecord()
        {
        }

        public MedicalRecord(int id, BloodType bloodType, Patient patient, List<string> allergiesList, List<string> conditionList)
        {
            Id = id;
            BloodType = bloodType;
            Patient = patient;

            this.AllergiesList = allergiesList;
            this.ConditionList = conditionList;
        }
    }
}