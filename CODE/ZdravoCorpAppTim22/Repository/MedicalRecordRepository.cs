using Model;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;
using ZdravoCorpAppTim22.Repository.Generic;

namespace Repository
{
   public class MedicalRecordRepository : IRepository<MedicalRecord>
    {
      public string Filename = "MedicalRecordData.json";
        GenericFileHandler<MedicalRecord> medicalRecordFileHandler;

        List<MedicalRecord> medicalRecords = new List<MedicalRecord>();

        private static MedicalRecordRepository instance;

        private MedicalRecordRepository()
        {
            medicalRecordFileHandler = new GenericFileHandler<MedicalRecord>(Filename);
        }
      
        public static MedicalRecordRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalRecordRepository();
                }

                return instance;
            }
        }

        public void Load()
        {
            medicalRecords = medicalRecordFileHandler.LoadData();
        }

        public List<MedicalRecord> GetAll()
        {
            return this.medicalRecords;
        }

      public MedicalRecord GetByID(int id)
      {
           int index = medicalRecords.FindIndex(r => r.Id == id);
           return medicalRecords[index];
        }
      
      public void DeleteByID(int id)
      {
            int index = medicalRecords.FindIndex(r => r.Id == id); 
            medicalRecords.RemoveAt(index);
            medicalRecordFileHandler.SaveData(medicalRecords);
      }
      
      public void Create(Model.MedicalRecord medicalRecord)
      {
         if (medicalRecords.Count > 0)
            {
                medicalRecord.Id = medicalRecords[medicalRecords.Count - 1].Id + 1;
            }
         else
            {
                medicalRecord.Id = 0;
            }
         this.medicalRecords.Add(medicalRecord);
            medicalRecordFileHandler.SaveData(medicalRecords);
      }
      
      public void Update(Model.MedicalRecord medicalRecord)
      {
            int index = medicalRecords.FindIndex(r => r.Id == medicalRecord.Id);
            medicalRecords[index] = medicalRecord;
            medicalRecordFileHandler.SaveData(medicalRecords);
      }
   }
}