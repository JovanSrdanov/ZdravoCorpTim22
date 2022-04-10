using Model;
using System;
using System.Collections.Generic;

namespace Service
{
   public class PatientService
   {
      public List<Patient> GetAll()
      {
            return patientRepository.GetAll();
      }
      
      public Model.Patient GetByID(int id)
      {
            return patientRepository.GetByID(id);
      }
      
      public void DeleteByID(int id)
      {
            patientRepository.DeleteByID(id);
      }
      
      public void Create(Model.Patient patient)
      {
            patientRepository.Create(patient);
      }
      
      public void Update(Model.Patient patient)
      {
            patientRepository.Create(patient);
        }
      
      public String path;
      
      public Repository.PatientRepository patientRepository = new Repository.PatientRepository();
   
   }
}