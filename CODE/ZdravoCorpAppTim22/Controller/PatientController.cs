using Model;
using Service;
using System;
using System.Collections.Generic;

namespace Controller
{
    public class PatientController
    {
        private static PatientController instance;

        private PatientController()
        {

        }


        public static PatientController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new PatientController();
                }

                return instance;
            }
        }

        public List<Patient> GetAll()
        {
            return PatientService.Instance.GetAll();
        }

        public Model.Patient GetByID(int id)
        {
            return PatientService.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            PatientService.Instance.DeleteByID(id);
        }

        public void Create(Model.Patient patient)
        {
            PatientService.Instance.Create(patient);
        }

        public void Update(Model.Patient patient)
        {
            PatientService.Instance.Create(patient);
        }

        public String path;


    }
}