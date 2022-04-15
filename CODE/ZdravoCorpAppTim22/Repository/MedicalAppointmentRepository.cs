using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace Repository
{
    public class MedicalAppointmentRepository
    {
        public string Filename = "MedicalAppointmentData.json";
        GenericFileHandler<MedicalAppointment> medicalAppointmentFileHandler;

        List<MedicalAppointment> medicalAppointments = new List<MedicalAppointment>();

        public static DoctorRepository doctorRepository;
           
        private static MedicalAppointmentRepository instance;

        private MedicalAppointmentRepository()
        {
            medicalAppointmentFileHandler = new GenericFileHandler<MedicalAppointment>(Filename);
        }

        public static MedicalAppointmentRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalAppointmentRepository();
                }

                return instance;
            }
        }

        public void Load()
        {
            medicalAppointments = medicalAppointmentFileHandler.LoadData();
        }

        public List<MedicalAppointment> getAll()
        {
            return this.medicalAppointments;
        }

        public Model.MedicalAppointment GetByID(int id)
        {
            int index = medicalAppointments.FindIndex(r => r.Id == id);
            return medicalAppointments[index];
        }

        public void DeleteByID(int id)
        {
            int index = medicalAppointments.FindIndex(r => r.Id == id);
            medicalAppointments.RemoveAt(index);
            medicalAppointmentFileHandler.SaveData(medicalAppointments);
        }

        public void Create(Model.MedicalAppointment medicalAppointment)
        {
            if (medicalAppointments.Count > 0)
            {
                medicalAppointment.Id = medicalAppointments[medicalAppointments.Count - 1].Id + 1;
            }
            else
            {
                medicalAppointment.Id = 0;
            }
            this.medicalAppointments.Add(medicalAppointment);
            medicalAppointmentFileHandler.SaveData(medicalAppointments);
        }

        public void Update(Model.MedicalAppointment medicalAppointment)
        {
            int index = medicalAppointments.FindIndex(r => r.Id == medicalAppointment.Id);
            medicalAppointments[index] = medicalAppointment;
            medicalAppointmentFileHandler.SaveData(medicalAppointments);
        }

    }
}