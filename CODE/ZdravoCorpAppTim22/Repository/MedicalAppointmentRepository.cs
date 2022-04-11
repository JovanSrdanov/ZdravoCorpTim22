using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class MedicalAppointmentRepository
    {
        private List<MedicalAppointment> medicalAppointments = new List<MedicalAppointment>();


        public Model.MedicalAppointment GetByID(int id)
        {
            int index = medicalAppointments.FindIndex(r => r.Id == id);
            return medicalAppointments[index];
        }

        public void DeleteByID(int id)
        {
            int index = medicalAppointments.FindIndex(r => r.Id == id);
            medicalAppointments.RemoveAt(index);
        }

        public void Create(Model.MedicalAppointment medicalAppointment)
        {
            this.medicalAppointments.Add(medicalAppointment);
        }

        public void Update(Model.MedicalAppointment medicalAppointment)
        {
            int index = medicalAppointments.FindIndex(r => r.Id == medicalAppointment.Id);
            medicalAppointments[index] = medicalAppointment;
        }

        public String path;

    }
}