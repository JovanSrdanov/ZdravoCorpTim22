using System;
using System.Text.Json.Serialization;

namespace Model
{
   public class Equipment
   {
        public int id { get; set; }
        public string name { get; set; }
        public int amount { get; set; }
        public EquipmentType type { get; set; }
        [JsonIgnore]
        public Room room;
        [JsonIgnore]
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

        public int RoomID { get; set; }

        [JsonConstructor]
        public Equipment() { }

        public Equipment(int id) 
        {
            this.id = id;
        }

        public Equipment(int id, string name, int amount, EquipmentType type, Room room) : this(id)
        {
            this.name = name;
            this.amount = amount;
            this.type = type;
            this.room = room;
        }
    }
}