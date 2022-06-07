using System;
using System.Text.Json.Serialization;

namespace Model
{
    public class ManagerClass : User
    {
        [JsonConstructor]
        public ManagerClass()
        {

        }

        public ManagerClass(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, int iD, Address address) : base(name, surname, email, jmbg, password, birthday, phone, gender, iD, address)
        {
        }

        public ManagerClass(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address) : base(name, surname, email, jmbg, password, birthday, phone, gender, address)
        {
        }

        public override string ToString()
        {
            return Name + " " + Surname + " " + Jmbg;
        }
    }
}