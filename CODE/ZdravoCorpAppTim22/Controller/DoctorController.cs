using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class DoctorController
   {
        public List<Doctor> GetAll()
        {
            return doctorService.GetAll();
        }

        public Model.Doctor GetByID(int id)
        {
            return doctorService.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            doctorService.DeleteByID(id);
        }

        public void Create(Model.Doctor doctor)
        {
            doctorService.Create(doctor);
        }

        public void Update(Model.Doctor doctor)
        {
            doctorService.Update(doctor);
        }

        public String path;
      
      public Service.DoctorService doctorService;
   
   }
}