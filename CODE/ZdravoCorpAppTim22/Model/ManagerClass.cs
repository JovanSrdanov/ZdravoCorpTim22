using System;

namespace Model
{
    public class ManagerClass : User
    {
        public ManagerClass(string name, string surname, string email, string jmbg, string password, DateTime birthday, string phone, Gender gender, int iD, Address address) : base(name, surname, email, jmbg, password, birthday, phone, gender, iD, address)
        {
        }
    }
}