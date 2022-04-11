using System;

namespace Service
{
    public class MedicalAppointmentService
    {
        public Model.MedicalAppointment GetByID(int id)
        {
            return medicalAppointmentRepository.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            medicalAppointmentRepository.DeleteByID(id);
        }

        public void Create(Model.MedicalAppointment medicalAppointment)
        {
            medicalAppointmentRepository.Create(medicalAppointment);
        }

        public void Update(Model.MedicalAppointment medicalAppointment)
        {
            medicalAppointmentRepository.Update(medicalAppointment);
        }

        public String path;

        public Repository.MedicalAppointmentRepository medicalAppointmentRepository;

    }
}