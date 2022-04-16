using Controller;
using Model;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace Repository
{
    public class DoctorRepository : IRepository<Doctor>
    {
        private static DoctorRepository instance;

        public string Filename = "DoctorData.json";
        
        List<Doctor> doctors = new List<Doctor>();
        GenericFileHandler<Doctor> doctorFileHandler;

        private DoctorRepository()
        {
            doctorFileHandler = new GenericFileHandler<Doctor>(Filename);
        }

        public static DoctorRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorRepository();

                }

                return instance;
            }
        }

        public void Load()
        {
            doctors = doctorFileHandler.LoadData();
        }

        public List<Doctor> GetAll()
        {
            return doctors;
        }

        public Doctor GetByID(int id)
        {
            int index = doctors.FindIndex(r => r.Id == id);
            return doctors[index];
        }

        public void DeleteByID(int id)
        {
            int index = doctors.FindIndex(r => r.Id == id);
            doctors.RemoveAt(index);
            doctorFileHandler.SaveData(doctors);
        }

        public void Create(Doctor doctor)
        {
            if (doctors.Count > 0)
            {
                doctor.Id = doctors[doctors.Count - 1].Id + 1;
            }
            else
            {
                doctor.Id = 0;
            }
            this.doctors.Add(doctor);
            doctorFileHandler.SaveData(doctors);
        }

        public void Update(Doctor doctor)
        {
            int index = doctors.FindIndex(r => r.Id == doctor.Id);
            doctors[index] = doctor;
            doctorFileHandler.SaveData(doctors);
        }

    }
}