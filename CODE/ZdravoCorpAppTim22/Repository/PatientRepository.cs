using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class PatientRepository
    {
        //TO-DO Ispravi nullove 

        public List<Patient> patients = new List<Patient> 
        {
            new Patient("Jovan","Srdanov","aaa","aaaa","aaaaa",DateTime.Now,"",Gender.male,1,null,null,null),
            new Patient("Jovan1","Srdanov1","bbb","bbbb","bbbbbb",DateTime.Now,"",Gender.male,2,null,null,null),
            new Patient("Jovan2","Srdanov2","c","cc","ccc",DateTime.Now,"",Gender.male,2,null,null,null),

        };


        public List<Patient> GetAll()
        {
            return this.patients;
        }

        public Model.Patient GetByID(int id)
        {
            int index = patients.FindIndex(r => r.ID == id);
            return patients[index];
        }

        public void DeleteByID(int id)
        {
            int index = patients.FindIndex(r => r.ID == id);
            patients.RemoveAt(index);
        }

        public void Create(Model.Patient patient)
        {
            this.patients.Add(patient);
        }

        public void Update(Model.Patient patient)
        {
            int index = patients.FindIndex(r => r.ID == patient.ID);
            patients[index] = patient;
        }

        public String path;

    }
}