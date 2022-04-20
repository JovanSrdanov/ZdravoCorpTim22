using Model;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class EquipmentRelocation : IHasID, IHasInterval
    {
        public int Id { get; set; }
        public Interval Interval { get; set; }

        [JsonIgnore]
        public List<Equipment> equipment;
        [JsonIgnore]
        public List<Equipment> Equipment
        {
            get
            {
                if (equipment == null)
                    equipment = new List<Equipment>();
                return equipment;
            }
            set
            {
                RemoveAllEquipment();
                if (value != null)
                {
                    foreach (Equipment oEquipment in value)
                        AddEquipment(oEquipment);
                }
            }
        }
        [JsonConverter(typeof(RoomToIDConverter))]
        public Room sourceRoom;
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
                        oldRoom.RemoveRelocationSource(this);
                    }
                    if (value != null)
                    {
                        this.sourceRoom = value;
                        this.sourceRoom.AddRelocationSource(this);
                    }
                }
            }
        }

        [JsonConverter(typeof(RoomToIDConverter))]
        public Room destinationRoom;
        [JsonConverter(typeof(RoomToIDConverter))]
        public Room DestinationRoom
        {
            get
            {
                return destinationRoom;
            }
            set
            {
                if (this.destinationRoom == null || !this.destinationRoom.Equals(value))
                {
                    if (this.destinationRoom != null)
                    {
                        Room oldRoom = this.destinationRoom;
                        this.destinationRoom = null;
                        oldRoom.RemoveRelocationDestination(this);
                    }
                    if (value != null)
                    {
                        this.destinationRoom = value;
                        this.destinationRoom.AddRelocationDestination(this);
                    }
                }
            }
        }

        [JsonConstructor]
        public EquipmentRelocation() { }

        public void AddEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipment == null)
                this.equipment = new List<Equipment>();
            if (!this.equipment.Contains(newEquipment))
            {
                this.equipment.Add(newEquipment);
                newEquipment.EquipmentRelocation = this;
            }
        }
        public void RemoveEquipment(Equipment oldEquipment)
        {
            if (oldEquipment == null)
                return;
            if (this.equipment != null)
                if (this.equipment.Contains(oldEquipment))
                {
                    this.equipment.Remove(oldEquipment);
                    oldEquipment.EquipmentRelocation = null;
                }
        }
        public void RemoveAllEquipment()
        {
            if (equipment != null)
            {
                System.Collections.ArrayList tmpEquipment = new System.Collections.ArrayList();
                foreach (Equipment oldEquipment in equipment)
                    tmpEquipment.Add(oldEquipment);
                equipment.Clear();
                foreach (Equipment oldEquipment in tmpEquipment)
                    oldEquipment.EquipmentRelocation = null;
                tmpEquipment.Clear();
            }
        }
    }
}
