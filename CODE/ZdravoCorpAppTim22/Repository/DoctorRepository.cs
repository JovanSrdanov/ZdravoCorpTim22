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
        
       // List<Doctor> doctors = new List<Doctor>();
       // DoctorFileHandler doctorFileHandler;

        private DoctorRepository()
        {
            //doctorFileHandler = new DoctorFileHandler(Filename);
           // doctors = doctorFileHandler.LoadData();
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

        

        /*public List<Doctor> doctors = new List<Doctor>
        {
            new Doctor("Stefan", "Apostolovic", "stefan@gmail.com", "123123123", "stefan123", DateTime.Now, "123321123", Gender.male, 123, null, DoctorSpecialisationType.regular,
                new List<MedicalAppointment> {MedicalAppointmentRepository.Instance.GetByID(123) , MedicalAppointmentRepository.Instance.GetByID(1231), MedicalAppointmentRepository.Instance.GetByID(1232)}),
            new Doctor("Petar", "Apostolovic1", "stefan1@gmail.com", "223123123", "stefan124", DateTime.Now, "223321123", Gender.male, 124, null, DoctorSpecialisationType.regular,
                new List<MedicalAppointment> { MedicalAppointmentRepository.Instance.GetByID(124) , MedicalAppointmentRepository.Instance.GetByID(1241), MedicalAppointmentRepository.Instance.GetByID(1242)}),
            new Doctor("Marija", "Apostolovic2", "stefan2@gmail.com", "323123123", "stefan125", DateTime.Now, "323321123", Gender.male, 125, null, DoctorSpecialisationType.regular,
                new List<MedicalAppointment> { MedicalAppointmentRepository.Instance.GetByID(125) , MedicalAppointmentRepository.Instance.GetByID(1251), MedicalAppointmentRepository.Instance.GetByID(1252)}),
        };*/

        /*public List<MedicalAppointment> medicalAppointments1 = new List<MedicalAppointment>()
        {
             new MedicalAppointment(123, AppointmentType.examination, DateTime.Now, null, PatientRepository.Instance.GetByID(1), DoctorRepository.Instance.GetByID(123), 30),
             new MedicalAppointment(1231, AppointmentType.examination, DateTime.Now, null, PatientRepository.Instance.GetByID(2), DoctorRepository.Instance.GetByID(123), 30),
             new MedicalAppointment(1232, AppointmentType.examination, DateTime.Now, null, PatientRepository.Instance.GetByID(3), DoctorRepository.Instance.GetByID(123), 30),

        };

        public List<MedicalAppointment> medicalAppointments2 = new List<MedicalAppointment>()
        {
             new MedicalAppointment(123, AppointmentType.examination, DateTime.Now, null, PatientRepository.Instance.GetByID(1), DoctorRepository.Instance.GetByID(123), 30),
             new MedicalAppointment(1231, AppointmentType.examination, DateTime.Now, null, PatientRepository.Instance.GetByID(2), DoctorRepository.Instance.GetByID(123), 30),
             new MedicalAppointment(1232, AppointmentType.examination, DateTime.Now, null, PatientRepository.Instance.GetByID(3), DoctorRepository.Instance.GetByID(123), 30),

        };

        public List<MedicalAppointment> medicalAppointments3 = new List<MedicalAppointment>()
        {
             new MedicalAppointment(123, AppointmentType.examination, DateTime.Now, null, PatientRepository.Instance.GetByID(1), DoctorRepository.Instance.GetByID(123), 30),
             new MedicalAppointment(1231, AppointmentType.examination, DateTime.Now, null, PatientRepository.Instance.GetByID(2), DoctorRepository.Instance.GetByID(123), 30),
             new MedicalAppointment(1232, AppointmentType.examination, DateTime.Now, null, PatientRepository.Instance.GetByID(3), DoctorRepository.Instance.GetByID(123), 30),

        };*/

        public List<Doctor> doctors = new List<Doctor>
        {
            new Doctor("Stefan", "Apostolovic", "stefan@gmail.com", "123123123", "stefan123", DateTime.Now, "123321123", Gender.male, 123, null, DoctorSpecialisationType.regular,
                null),
            new Doctor("Petar", "Apostolovic1", "stefan1@gmail.com", "223123123", "stefan124", DateTime.Now, "223321123", Gender.male, 124, null, DoctorSpecialisationType.regular,
                null),
            new Doctor("Marija", "Apostolovic2", "stefan2@gmail.com", "323123123", "stefan125", DateTime.Now, "323321123", Gender.male, 125, null, DoctorSpecialisationType.regular,
                null),
        };

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
            //doctorFileHandler.SaveData(doctors);
        }

        public void Create(Doctor doctor)
        {
            this.doctors.Add(doctor);
           // doctorFileHandler.SaveData(doctors);
        }

        public void Update(Doctor doctor)
        {
            int index = doctors.FindIndex(r => r.ID == doctor.ID);
            doctors[index] = doctor;
           // doctorFileHandler.SaveData(doctors);
        }

        public String path;

    }
}