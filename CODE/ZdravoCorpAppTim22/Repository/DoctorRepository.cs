using Controller;
using Model;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace Repository
{
    public class DoctorRepository
    {
        private static DoctorRepository instance;

        public string Filename = "DoctorData.json";
        
        List<Doctor> doctors = new List<Doctor>();
        DoctorFileHandler doctorFileHandler;

        private DoctorRepository()
        {
            doctorFileHandler = new DoctorFileHandler(Filename);
            doctors = doctorFileHandler.LoadData();
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

        public List<Doctor> GetAll()
        {
            return doctors;
        }

        public Doctor GetByID(int id)
        {
            int index = doctors.FindIndex(r => r.ID == id);
            return doctors[index];
        }

        public void DeleteByID(int id)
        {
            int index = doctors.FindIndex(r => r.ID == id);
            doctors.RemoveAt(index);
            doctorFileHandler.SaveData(doctors);
        }

        public void Create(Doctor doctor)
        {
            if (doctors.Count > 0)
            {
                doctor.ID = doctors[doctors.Count - 1].ID + 1;
            }
            else
            {
                doctor.ID = 0;
            }
            this.doctors.Add(doctor);
            doctorFileHandler.SaveData(doctors);
        }

        public void Update(Doctor doctor)
        {
            int index = doctors.FindIndex(r => r.ID == doctor.ID);
            doctors[index] = doctor;
            doctorFileHandler.SaveData(doctors);
        }

    }
}