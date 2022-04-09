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
      
      
      public Address Address
      {
         get
         {
            return address;
         }
         set
         {
            if (this.address == null || !this.address.Equals(value))
            {
               if (this.address != null)
               {
                  Address oldAddress = this.address;
                  this.address = null;
                  oldAddress.RemoveUser(this);
               }
               if (value != null)
               {
                  this.address = value;
                  this.address.AddUser(this);
               }
            }
         }
      }
   
   }
}