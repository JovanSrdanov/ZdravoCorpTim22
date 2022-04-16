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
        [JsonConverter(typeof(RoomToIDConverter))]
        public Room OldRoom { get; set; }
        public Room NewRoom { get; set; }
        public Interval Interval { get; set; }

        [JsonConstructor]
        public Renovation() { }
    }

    public struct Interval
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
