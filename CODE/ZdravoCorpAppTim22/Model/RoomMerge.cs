using Model;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class RoomMerge : IHasID, IHasInterval
    {
        public int Id { get; set; }
        public Room NewRoom { get; set; }
        public Interval Interval { get; set; }

        [JsonConverter(typeof(RoomToIDConverter))]
        private Room firstRoom;
        [JsonConverter(typeof(RoomToIDConverter))]
        private Room secondRoom;

        #region properties

        [JsonConverter(typeof(RoomToIDConverter))]
        public Room FirstRoom
        {
            get
            {
                return firstRoom;
            }
            set
            {
                if (this.firstRoom == null || !this.firstRoom.Equals(value))
                {
                    if (this.firstRoom != null)
                    {
                        Room oldRoom = this.firstRoom;
                        this.firstRoom = null;
                        oldRoom.RemoveMergeWhereFirst(this);
                    }
                    if (value != null)
                    {
                        this.firstRoom = value;
                        this.firstRoom.AddMergeWhereFirst(this);
                    }
                }
            }
        }
        [JsonConverter(typeof(RoomToIDConverter))]
        public Room SecondRoom
        {
            get
            {
                return secondRoom;
            }
            set
            {
                if (this.secondRoom == null || !this.secondRoom.Equals(value))
                {
                    if (this.secondRoom != null)
                    {
                        Room oldRoom = this.secondRoom;
                        this.secondRoom = null;
                        oldRoom.RemoveMergeWhereSecond(this);
                    }
                    if (value != null)
                    {
                        this.secondRoom = value;
                        this.secondRoom.AddMergeWhereSecond(this);
                    }
                }
            }
        }
        #endregion

        [JsonConstructor]
        public RoomMerge() { }
    }
}
