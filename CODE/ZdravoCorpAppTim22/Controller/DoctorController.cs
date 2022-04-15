using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class DoctorController
    {
        private static DoctorController instance;

        private DoctorController()
        {

        }
        public static DoctorController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorController();
                }

                return instance;
            }
        }

        public void Load()
        {
            DoctorService.Instance.Load();
        }

        public List<Doctor> GetAll()
        {
            return DoctorService.Instance.GetAll();
        }

        public Model.Doctor GetByID(int id)
        {
            return DoctorService.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            DoctorService.Instance.DeleteByID(id);
        }

        public void Create(Model.Doctor doctor)
        {
            DoctorService.Instance.Create(doctor);
        }

        public void Update(Model.Doctor doctor)
        {
            DoctorService.Instance.Update(doctor);
        }

    }
}