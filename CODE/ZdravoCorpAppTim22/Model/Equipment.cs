using System;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace Model
{
   public class Equipment
   {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Amount { get; set; }
        public EquipmentType Type { get; set; }

        [JsonConverter(typeof(RoomToIDConverter))]
        public Room room;
        [JsonConverter(typeof(RoomToIDConverter))]
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

        [JsonConstructor]
        public Equipment() { }

        public Equipment(int id) 
        {
            this.Id = id;
        }

        public Equipment(int id, string name, int amount, EquipmentType type, Room room) : this(id)
        {
            this.Name = name;
            this.Amount = amount;
            this.Type = type;
            this.Room = room;
        }
    }
}