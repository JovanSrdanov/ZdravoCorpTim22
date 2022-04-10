using System;

namespace Model
{
    public class Patient : User
    {
        public MedicalRecord medicalRecord;
        public MedicalAppointment[] medicalAppointment;

        public Patient(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address) : base(name, surname, email, jmbg, password, birthday, phone, gender, address)
        {
        }
    }
}