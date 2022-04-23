using System;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Model;

namespace Model
{
    public class MedicalReport
    {
        //izvestaj
        public int Id { get; set; }
        public string Anamnesis { get; set; }
        public string Diagnosis { get; set; }
        public DateTime ReportDate { get; set; }

        //recept
        public ObservableCollection<string> MedicineNameList { get; set; }
        public ObservableCollection<string> MedicineAmountList { get; set; }
        public ObservableCollection<string> MedicineInstructionList { get; set; }

        public MedicalReport(int id, string anamnesis, string diagnosis,
            ObservableCollection<string> MedicineNameList, ObservableCollection<string> MedicineAmountList,
            ObservableCollection<string> MedicineInstructionList, DateTime ReportDate)
        {
            Id = id;
            Anamnesis = anamnesis;
            Diagnosis = diagnosis;
            this.MedicineNameList = MedicineNameList;
            this.MedicineAmountList = MedicineAmountList;
            this.MedicineInstructionList = MedicineInstructionList;
            this.ReportDate = ReportDate;
        }

        //karton
        private MedicalRecord medicalRecord { get; set; }   
        public MedicalRecord MedicalRecord
        {
            get
            {
                return medicalRecord;
            }
            set
            {
                this.medicalRecord = value;
            }
        }

    }
}