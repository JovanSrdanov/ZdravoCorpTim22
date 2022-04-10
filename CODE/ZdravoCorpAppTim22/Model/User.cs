using System;

namespace Model
{
    public class User
    {
        public string Name { set; get; }
        public string Surname { set; get; }
        public string Email { set; get; }
        public string Jmbg { set; get; }
        public string Password { set; get; }
        public DateTime Birthday { set; get; }
        public string Phone { set; get; }
        public Gender Gender { set; get; }

        public Address Address;

        public User(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address)
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
        }

    }
}