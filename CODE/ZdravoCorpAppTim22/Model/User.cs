using System;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace Model
{
    public class User : IHasID
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Jmbg { get; set; }
        public string Password { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public int Id { get; set; }

        [JsonConverter(typeof(AddressToIDConverter))]
        public Address address;
        [JsonConverter(typeof(AddressToIDConverter))]
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

        public User(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, int id, Address address)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Jmbg = jmbg;
            Password = password;
            Birthday = birthday;
            Phone = phone;
            Gender = gender;
            Id = id;
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