using Model;
using System;
using System.Collections.Generic;

namespace Repository
{
    public class MedicalAppointmentRepository
    {
        public static DoctorRepository doctorRepository;
       /* private List<MedicalAppointment> medicalAppointmentsDoctor = new List<MedicalAppointment>
        {
             new MedicalAppointment(123, AppointmentType.examination, DateTime.Now, null, null, doctorRepository.GetByID(123), 30),
             new MedicalAppointment(1231, AppointmentType.examination, DateTime.Now, null, null, doctorRepository.GetByID(123), 30),
             new MedicalAppointment(1232, AppointmentType.examination, DateTime.Now, null, null, doctorRepository.GetByID(123), 30),

             new MedicalAppointment(124, AppointmentType.checkup, DateTime.Now, null, null, doctorRepository.GetByID(124), 45),
             new MedicalAppointment(1241, AppointmentType.examination, DateTime.Now, null, null, doctorRepository.GetByID(123), 30),
             new MedicalAppointment(1242, AppointmentType.examination, DateTime.Now, null, null, doctorRepository.GetByID(123), 30),

             new MedicalAppointment(125, AppointmentType.operation, DateTime.Now, null, null, doctorRepository.GetByID(125), 35),
             new MedicalAppointment(1251, AppointmentType.examination, DateTime.Now, null, null, doctorRepository.GetByID(123), 30),
             new MedicalAppointment(1252, AppointmentType.examination, DateTime.Now, null, null, doctorRepository.GetByID(123), 30),
         };*/
           
        private static MedicalAppointmentRepository instance;

        private MedicalAppointmentRepository()
        {

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

        public List<MedicalAppointment> medicalAppointments = new List<MedicalAppointment>()
        {
             new MedicalAppointment(123, AppointmentType.examination, DateTime.Now, null, null, DoctorRepository.Instance.GetByID(123), 30),
             new MedicalAppointment(1231, AppointmentType.examination, DateTime.Now, null, null, DoctorRepository.Instance.GetByID(123), 30),
             new MedicalAppointment(1232, AppointmentType.examination, DateTime.Now, null, null, DoctorRepository.Instance.GetByID(123), 30),

             new MedicalAppointment(124, AppointmentType.checkup, DateTime.Now, null, null, DoctorRepository.Instance.GetByID(124), 45),
             new MedicalAppointment(1241, AppointmentType.examination, DateTime.Now, null, null, DoctorRepository.Instance.GetByID(123), 30),
             new MedicalAppointment(1242, AppointmentType.examination, DateTime.Now, null, null, DoctorRepository.Instance.GetByID(123), 30),

             new MedicalAppointment(125, AppointmentType.operation, DateTime.Now, null, null, DoctorRepository.Instance.GetByID(125), 35),
             new MedicalAppointment(1251, AppointmentType.examination, DateTime.Now, null, null, DoctorRepository.Instance.GetByID(123), 30),
             new MedicalAppointment(1252, AppointmentType.examination, DateTime.Now, null, null, DoctorRepository.Instance.GetByID(123), 30),
        };

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