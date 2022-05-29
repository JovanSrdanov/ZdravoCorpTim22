using Model;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class RoomDiverge : IHasID, IHasInterval
    {
        public int Id { get; set; }
        public Interval Interval { get; set; }

        [JsonConverter(typeof(RoomToIDConverter))]
        private Room sourceRoom;
        
        public Room FirstRoom { get; set; }
        public Room SecondRoom { get; set; }

        public List<Equipment> FirstRoomEquipment { get; set; }
        public List<Equipment> SecondRoomEquipment { get; set; }

        #region properties

        [JsonConverter(typeof(RoomToIDConverter))]
        public Room SourceRoom
        {
            get
            {
                return sourceRoom;
            }
            set
            {
                if (this.sourceRoom == null || !this.sourceRoom.Equals(value))
                {
                    if (this.sourceRoom != null)
                    {
                        Room oldRoom = this.sourceRoom;
                        this.sourceRoom = null;
                        oldRoom.RemoveDiverge(this);
                    }
                    if (value != null)
                    {
                        this.sourceRoom = value;
                        this.sourceRoom.AddDiverge(this);
                    }
                }
            }
        }
        #endregion

        [JsonConstructor]
        public RoomDiverge() 
        { 
            FirstRoomEquipment = new List<Equipment>();
            SecondRoomEquipment = new List<Equipment>();
        }

    }
}
