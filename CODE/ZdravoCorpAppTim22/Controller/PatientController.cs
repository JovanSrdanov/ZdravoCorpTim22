using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class PatientController
   {
        public List<Patient> GetAll()
        {
            return patientService.GetAll();
        }

        public Model.Patient GetByID(int id)
        {
            return patientService.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            patientService.DeleteByID(id);
        }

        public void Create(Model.Patient patient)
        {
            patientService.Create(patient);
        }

        public void Update(Model.Patient patient)
        {
            patientService.Create(patient);
        }

        public String path;
      
      public Service.PatientService patientService;
   
   }
}