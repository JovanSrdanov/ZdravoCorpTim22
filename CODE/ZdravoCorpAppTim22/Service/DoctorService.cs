using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class DoctorService
    {
        private static DoctorService instance;

        private DoctorService()
        {

        }

        public static DoctorService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorService();
                }

                return instance;
            }
        }
        public List<Doctor> GetAll()
        {
            return DoctorRepository.Instance.GetAll();
        }

        public Model.Doctor GetByID(int id)
        {
            return DoctorRepository.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            DoctorRepository.Instance.DeleteByID(id);
        }

        public void Create(Model.Doctor doctor)
        {
            DoctorRepository.Instance.Create(doctor);
        }

        public void Update(Model.Doctor doctor)
        {
            DoctorRepository.Instance.Update(doctor);
        }
    }
}