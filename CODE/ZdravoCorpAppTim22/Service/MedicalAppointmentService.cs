using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
    public class MedicalAppointmentService
    {
        private static MedicalAppointmentService instance;

        private MedicalAppointmentService()
        {

        }

        public static MedicalAppointmentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalAppointmentService();
                }

                return instance;
            }
        }

        public List<MedicalAppointment> getAll()
        {
            return MedicalAppointmentRepository.Instance.getAll();
        }

        public MedicalAppointment GetByID(int id)
        {
            return MedicalAppointmentRepository.Instance.GetByID(id);
        }

        public void DeleteByID(int id)
        {
            MedicalAppointmentRepository.Instance.DeleteByID(id);
        }

        public void Create(Model.MedicalAppointment medicalAppointment)
        {
            MedicalAppointmentRepository.Instance.Create(medicalAppointment);
        }

        public void Update(Model.MedicalAppointment medicalAppointment)
        {
            MedicalAppointmentRepository.Instance.Update(medicalAppointment);
        }

        public String path;

    }
}