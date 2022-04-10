using System;

namespace Model
{
    public class Doctor : User
    {
        public DoctorSpecialisationType DoctorType { set; get; }

        public MedicalAppointment[] MedicalAppointment { set; get; }

        public Doctor(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address, DoctorSpecialisationType doctorType) : base(name, surname, email, jmbg, password, birthday, phone, gender, address)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Jmbg = jmbg;
            Password = password;
            Birthday = birthday;
            Phone = phone;
            Gender = gender;
            Address = address;
            DoctorType = doctorType;
        }

    }
}