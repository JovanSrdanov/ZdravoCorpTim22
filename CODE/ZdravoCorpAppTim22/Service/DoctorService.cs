using Model;
using System;
using System.Collections.Generic;

namespace Service
{
   public class DoctorService
   {
      public List<Doctor> GetAll()
      {
            return doctorRepository.GetAll();
      }
      
      public Model.Doctor GetByID(int id)
      {
            return doctorRepository.GetByID(id);
        }
      
      public void DeleteByID(int id)
      {
            doctorRepository.DeleteByID(id);
        }
      
      public void Create(Model.Doctor doctor)
      {
            doctorRepository.Create(doctor);
        }
      
      public void Update(Model.Doctor doctor)
      {
            doctorRepository.Update(doctor);
        }
      
      public String path;
      
      public Repository.DoctorRepository doctorRepository;
   
   }
}