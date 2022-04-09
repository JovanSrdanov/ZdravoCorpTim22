using System;

namespace Model
{
   public class Equipment
   {
      public int id;
      public string name;
      public int amount;
      public EquipmentType type;
      
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
                  oldRoom.RemoveEquipment(this);
               }
               if (value != null)
               {
                  this.room = value;
                  this.room.AddEquipment(this);
               }
            }
         }
      }
   
   }
}