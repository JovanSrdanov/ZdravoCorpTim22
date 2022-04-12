using System;
using System.Text.Json.Serialization;

namespace Model
{
    public class SecretaryClass : User
    {
        public SecretaryClass(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, int iD, Address address) : base(name, surname, email, jmbg, password, birthday, phone, gender, iD, address)
        {
        }

        public SecretaryClass(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address) : base(name, surname, email, jmbg, password, birthday, phone, gender, address)
        {
        }
        
        [JsonConstructor]
        public SecretaryClass(){

        }
    }
}