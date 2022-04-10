using System;

namespace Model
{
    public class Secretary : User
    {
        public Secretary(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, Address address) : base(name, surname, email, jmbg, password, birthday, phone, gender, address)
        {
        }
    }
}