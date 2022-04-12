using System;
using System.Text.Json.Serialization;

namespace Model
{
    public class User
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Jmbg { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }

        public int addressID { get; set; }

        public int ID { get; set; }

        [JsonIgnore]
        public Address address;
        [JsonIgnore]
        public Address Address
        {
            get
            {
                return address;
            }
            set
            {
                this.address = value;
            }
        }

        public User(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, int iD, Address address)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Jmbg = jmbg;
            Password = password;
            Birthday = birthday;
            Phone = phone;
            Gender = gender;
            ID = iD;
            Address = address;
        }

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

        [JsonConstructor]
        public User()
        {
        }

    }
}