using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace Model
{
    public class MedicalReport
    {
        //izvestaj
        public int Id { get; set; }
        public int DoctorID { get; set;}            //ko je napravio izvestaj
        public string Anamnesis { get; set; }
        public string Diagnosis { get; set; }
        public DateTime ReportDate { get; set; }

        //recept
        public ObservableCollection<string> MedicineNameList { get; set; }
        public ObservableCollection<string> MedicineAmountList { get; set; }
        public ObservableCollection<string> MedicineInstructionList { get; set; }

        [JsonConstructor]
        public MedicalReport() { }
        public MedicalReport(int id, string anamnesis, string diagnosis,
            ObservableCollection<string> MedicineNameList, ObservableCollection<string> MedicineAmountList,
            ObservableCollection<string> MedicineInstructionList, DateTime ReportDate, MedicalRecord medicalRecord)
        {
            Id = id;
            Anamnesis = anamnesis;
            Diagnosis = diagnosis;
            this.MedicineNameList = MedicineNameList;
            this.MedicineAmountList = MedicineAmountList;
            this.MedicineInstructionList = MedicineInstructionList;
            this.ReportDate = ReportDate;
            this.medicalRecord = medicalRecord;
        }

        //karton
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
                        MedicalRecord oldMedicalRecord = this.medicalRecord;
                        this.medicalRecord = null;
                        oldMedicalRecord.RemoveMedicalReport(this);
                    }
                    if (value != null)
                    {
                        this.medicalRecord= value;
                        this.medicalRecord.AddMedicalReport(this);
                    }
                }
            }
        }

    }
}