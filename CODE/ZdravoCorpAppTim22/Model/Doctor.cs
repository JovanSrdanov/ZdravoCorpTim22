using System;

namespace Model
{
    public class Doctor : User
    {
        private DoctorSpecialisationType doctorType;

        public MedicalAppointment[] medicalAppointment;

        public Doctor(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address, DoctorSpecialisationType doctorType) : base(name, surname, email, jmbg, password, birthday, phone, gender, address)
        {
            this.doctorType = doctorType;
        }

    }
}