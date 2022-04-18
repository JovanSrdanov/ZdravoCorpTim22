using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class Renovation : IHasID
    {
        public int Id { get; set; }
        public Room NewRoom { get; set; }
        public Interval Interval { get; set; }

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
                        oldRoom.RemoveRenovation(this);
                    }
                    if (value != null)
                    {
                        this.room = value;
                        this.room.AddRenovation(this);
                    }
                }
            }
        }

        [JsonConstructor]
        public Renovation() { }

        public Renovation(int id, Room room, Room newRoom, Interval interval)
        {
            Id = id;
            Room = room;
            NewRoom = newRoom;
            Interval = interval;
        }
    }

    public struct Interval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
