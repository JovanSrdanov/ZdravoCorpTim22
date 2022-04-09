using Model;
using System;
using System.Collections.Generic;

namespace Service
{
   public class PatientService
   {
      public List<Patient> GetAll()
      {
         throw new NotImplementedException();
      }
      
      public Model.Patient GetByID(int id)
      {
         throw new NotImplementedException();
      }
      
      public void DeleteByID(int id)
      {
         throw new NotImplementedException();
      }
      
      public void Create(Model.Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public void Update(Model.Patient patient)
      {
         throw new NotImplementedException();
      }
      
      public String path;
      
      public Repository.PatientRepository patientRepository;
   
   }
}