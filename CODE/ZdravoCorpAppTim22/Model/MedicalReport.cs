using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace Model
{
    public class MedicalReport : IHasID
    {
        //izvestaj
        public int Id { get; set; }
        public int DoctorID { get; set;}            //ko je napravio izvestaj
        public string Anamnesis { get; set; }
        public string Diagnosis { get; set; }
        public DateTime ReportDate { get; set; }
        public bool ReportReviewed { get; set; }
        public string ReportComment { get; set; }

        //recept
        [JsonConverter(typeof(MedicalReceiptToIDConverter))]
        public MedicalReceipt MedicalReceipt { get; set; }



        [JsonConverter(typeof(ReportReviewToIDConverter))]

        private ReportReview reportReview;
        [JsonConverter(typeof(ReportReviewToIDConverter))]

        public ReportReview ReportReview
        {
            get
            {
                return reportReview;
            }
            set
            {
                this.reportReview = value;
            }
        }


        [JsonConstructor]
        public MedicalReport() { }
        public MedicalReport(int id, string anamnesis, string diagnosis, DateTime ReportDate, MedicalRecord medicalRecord)
        {
            Id = id;
            Anamnesis = anamnesis;
            Diagnosis = diagnosis;
            this.ReportDate = ReportDate;
            this.medicalRecord = medicalRecord;
            ReportReviewed = false;
            ReportComment = "";
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