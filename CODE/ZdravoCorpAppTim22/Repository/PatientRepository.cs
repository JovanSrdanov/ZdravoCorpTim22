using Model;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace Repository
{
    public class PatientRepository : IRepository<Patient>
    {
        private static PatientRepository instance;

        public List<Patient> patients = new List<Patient>();
        public string FileName = "PatientData.json";
        GenericFileHandler<Patient> patientFileHandler;

        private PatientRepository()
        {
            patientFileHandler = new GenericFileHandler<Patient>(FileName);
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

        public void Load()
        {
            patients = patientFileHandler.LoadData();
        }

        public List<Patient> GetAll()
        {
            return this.patients;
        }

        public Model.Patient GetByID(int id)
        {
            int index = patients.FindIndex(r => r.Id == id);
            return patients[index];
        }

        public void DeleteByID(int id)
        {
            int index = patients.FindIndex(r => r.Id == id);
            patients.RemoveAt(index);
            patientFileHandler.SaveData(patients);
        }

        public void Create(Model.Patient patient)
        {
            if (patients.Count > 0)
            {
                patient.Id = patients[patients.Count - 1].Id + 1;
            }
            else
            {
                patient.Id = 0;
            }
            this.patients.Add(patient);
            patientFileHandler.SaveData(patients);
        }

        public void Update(Model.Patient patient)
        {
            int index = patients.FindIndex(r => r.Id == patient.Id);
            patients[index] = patient;
            patientFileHandler.SaveData(patients);
        }

    }
}