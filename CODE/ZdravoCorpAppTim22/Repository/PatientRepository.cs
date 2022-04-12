using Model;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace Repository
{
    public class PatientRepository
    {
        //TO-DO Ispravi nullove 
        private static PatientRepository instance;

        public List<Patient> patients = new List<Patient>();
        public string FileName = "PatientData.json";
        PatientFileHandler patientFileHandler;

        private PatientRepository()
        {
            patientFileHandler = new PatientFileHandler(FileName);
            patients = patientFileHandler.LoadData();
        }

        public static PatientRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientRepository();
                }

                return instance;
            }
        }

        

        public List<Patient> GetAll()
        {
            return this.patients;
        }

        public Model.Patient GetByID(int id)
        {
            int index = patients.FindIndex(r => r.ID == id);
            return patients[index];
        }

        public void DeleteByID(int id)
        {
            int index = patients.FindIndex(r => r.ID == id);
            patients.RemoveAt(index);
            patientFileHandler.SaveData(patients);
        }

        public void Create(Model.Patient patient)
        {
            if (patients.Count > 0)
            {
                patient.ID = patients[patients.Count - 1].ID + 1;
            }
            else
            {
                patient.ID = 0;
            }
            this.patients.Add(patient);
            patientFileHandler.SaveData(patients);
        }

        public void Update(Model.Patient patient)
        {
            int index = patients.FindIndex(r => r.ID == patient.ID);
            patients[index] = patient;
            patientFileHandler.SaveData(patients);
        }

    }
}