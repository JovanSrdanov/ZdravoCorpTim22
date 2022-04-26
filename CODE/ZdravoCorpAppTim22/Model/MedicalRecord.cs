using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace Model
{
    public class MedicalRecord : IHasID
    {
        public int Id { get; set; }
        public BloodType BloodType { get; set; }

        //dodao
        public ObservableCollection<string> AllergiesList { get; set; }
        public ObservableCollection<string> ConditionList { get; set; }

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
        //dodao

        [JsonConverter(typeof(PatientToIDConverter))]
        public Patient Patient { get; set; }

        [JsonConstructor]
        public MedicalRecord()
        {
        }

        public MedicalRecord(int id, BloodType bloodType, Patient patient, ObservableCollection<string> allergiesList, ObservableCollection<string> conditionList)
        {
            Id = id;
            BloodType = bloodType;
            Patient = patient;

            this.AllergiesList = allergiesList;
            this.ConditionList = conditionList;
        }
    }
}