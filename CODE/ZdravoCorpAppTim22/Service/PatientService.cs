using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class PatientService
    {
        private static PatientService instance;

        private PatientService()
        {

        }

        public static PatientService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientService();
                }

                return instance;
            }
        }

        public void Load()
        {
            PatientRepository.Instance.Load();
        }

        public List<Patient> GetAll()
        {
            return PatientRepository.Instance.GetAll();
        }

        public Patient GetByID(int id)
        {
            return PatientRepository.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            PatientRepository.Instance.DeleteByID(id);
        }

        public void Create(Model.Patient patient)
        {
            PatientRepository.Instance.Create(patient);
        }

        public void Update(Model.Patient patient)
        {
            PatientRepository.Instance.Update(patient);
        }
    }
}