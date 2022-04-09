using System;

namespace Model
{
   public class MedicalAppointment
   {
      public int id;
      public AppointmentType type;
      public DateTime date;
      
      public Room room;
      
      
      public Room Room
      {
         get
         {
            return room;
         }
         set
         {
            if (this.room == null || !this.room.Equals(value))
            {
               if (this.room != null)
               {
                  Room oldRoom = this.room;
                  this.room = null;
                  oldRoom.RemoveMedicalAppointment(this);
               }
               if (value != null)
               {
                  this.room = value;
                  this.room.AddMedicalAppointment(this);
               }
            }
         }
      }
      public Patient patient;
      public Doctor doctor;
   
   }
}