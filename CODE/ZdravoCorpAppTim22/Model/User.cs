using System;

namespace Model
{
    public class User
    {
        public string name;
        public string surname;
        public string email;
        public string jmbg;
        public string password;
        public DateTime birthday;
        public string phone;
        public Gender gender;

        public Address address;

        public User(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address)
        {
            this.name = name;
            this.surname = surname;
            this.email = email;
            this.jmbg = jmbg;
            this.password = password;
            this.birthday = birthday;
            this.phone = phone;
            this.gender = gender;
            this.address = address;
        }

    }
}