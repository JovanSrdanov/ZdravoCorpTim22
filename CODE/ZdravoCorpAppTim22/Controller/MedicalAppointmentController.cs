using Model;
using Service;
using System;
namespace Controller
{
    public class MedicalAppointmentController
    {
        private static MedicalAppointmentController instance;

        private MedicalAppointmentController()
        {

        }
        public static MedicalAppointmentController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalAppointmentController();
                }

                return instance;
            }
        }

        public MedicalAppointment GetByID(int id)
        {
            return MedicalAppointmentService.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            MedicalAppointmentService.Instance.DeleteByID(id);
        }

        public void Create(MedicalAppointment medicalAppointment)
        {
            MedicalAppointmentService.Instance.Create(medicalAppointment);
        }

        public void Update(MedicalAppointment medicalAppointment)
        {
            MedicalAppointmentService.Instance.Update(medicalAppointment);
        }
        public String path;

    }
}