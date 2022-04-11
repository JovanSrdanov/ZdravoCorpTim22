using System;

namespace Controller
{
    public class MedicalAppointmentController
    {
        public Model.MedicalAppointment GetByID(int id)
        {
            return medicalAppointmentService.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            medicalAppointmentService.DeleteByID(id);
        }

        public void Create(Model.MedicalAppointment medicalAppointment)
        {
            medicalAppointmentService.Create(medicalAppointment);
        }

        public void Update(Model.MedicalAppointment medicalAppointment)
        {
            medicalAppointmentService.Update(medicalAppointment);
        }


        public String path;

        public Service.MedicalAppointmentService medicalAppointmentService;

    }
}