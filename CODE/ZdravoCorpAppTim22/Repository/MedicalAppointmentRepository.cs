using Model;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace Repository
{
    public class MedicalAppointmentRepository
    {
        public string Filename = "MedicalAppointmentData.json";
        MedicalAppointmentFileHandler medicalAppointmentFileHandler;

        //List<MedicalAppointment> medicalAppointments = new List<MedicalAppointment>();

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
            //medicalAppointmentFileHandler = new MedicalAppointmentFileHandler(Filename);
           // medicalAppointments = medicalAppointmentFileHandler.LoadData();
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
            /* new MedicalAppointment(123, AppointmentType.examination, DateTime.Now,DateTime.Now, null, PatientRepository.Instance.GetByID(1), DoctorRepository.Instance.GetByID(123)),
             new MedicalAppointment(1231, AppointmentType.examination, DateTime.Now, DateTime.Now, null, PatientRepository.Instance.GetByID(2), DoctorRepository.Instance.GetByID(123)),
             new MedicalAppointment(1232, AppointmentType.examination, DateTime.Now, DateTime.Now, null, PatientRepository.Instance.GetByID(3), DoctorRepository.Instance.GetByID(123)),

             new MedicalAppointment(124, AppointmentType.checkup, DateTime.Now, DateTime.Now, null, PatientRepository.Instance.GetByID(1), DoctorRepository.Instance.GetByID(124)),
             new MedicalAppointment(1241, AppointmentType.examination, DateTime.Now, DateTime.Now, null, PatientRepository.Instance.GetByID(2), DoctorRepository.Instance.GetByID(123)),
             new MedicalAppointment(1242, AppointmentType.examination, DateTime.Now, DateTime.Now, null, PatientRepository.Instance.GetByID(3), DoctorRepository.Instance.GetByID(123)),

             new MedicalAppointment(125, AppointmentType.operation, DateTime.Now, DateTime.Now, null, PatientRepository.Instance.GetByID(1), DoctorRepository.Instance.GetByID(125)),
             new MedicalAppointment(1251, AppointmentType.examination, DateTime.Now, DateTime.Now, null, PatientRepository.Instance.GetByID(2), DoctorRepository.Instance.GetByID(123)),
             new MedicalAppointment(1252, AppointmentType.examination, DateTime.Now, DateTime.Now, null, PatientRepository.Instance.GetByID(3), DoctorRepository.Instance.GetByID(123)),*/
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
           // medicalAppointmentFileHandler.SaveData(medicalAppointments);
        }

        public void Create(Model.MedicalAppointment medicalAppointment)
        {
            this.medicalAppointments.Add(medicalAppointment);
           // medicalAppointmentFileHandler.SaveData(medicalAppointments);
        }

        public void Update(Model.MedicalAppointment medicalAppointment)
        {
            int index = medicalAppointments.FindIndex(r => r.Id == medicalAppointment.Id);
            medicalAppointments[index] = medicalAppointment;
           // medicalAppointmentFileHandler.SaveData(medicalAppointments);
        }

        public String path;

    }
}