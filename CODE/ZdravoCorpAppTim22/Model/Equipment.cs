using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace Model
{
   public class Equipment : IHasID
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

        [JsonIgnore]
        public List<EquipmentRelocation> relocationEquipment;
        [JsonIgnore]
        public List<EquipmentRelocation> RelocationEquipment
        {
            get
            {
                if (relocationEquipment == null)
                    relocationEquipment = new List<EquipmentRelocation>();
                return relocationEquipment;
            }
            set
            {
                RemoveAllRelocationEquipment();
                if (value != null)
                {
                    foreach (EquipmentRelocation oEquipmentRelocation in value)
                        AddRelocationEquipment(oEquipmentRelocation);
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

        public void AddRelocationEquipment(EquipmentRelocation newEquipmentRelocation)
        {
            if (newEquipmentRelocation == null)
                return;
            if (this.relocationEquipment == null)
                this.relocationEquipment = new List<EquipmentRelocation>();
            if (!this.relocationEquipment.Contains(newEquipmentRelocation))
            {
                this.relocationEquipment.Add(newEquipmentRelocation);
                newEquipmentRelocation.Equipment = this;
            }
        }
        public void RemoveRelocationEquipment(EquipmentRelocation oldEquipmentRelocation)
        {
            if (oldEquipmentRelocation == null)
                return;
            if (this.relocationEquipment != null)
                if (this.relocationEquipment.Contains(oldEquipmentRelocation))
                {
                    this.relocationEquipment.Remove(oldEquipmentRelocation);
                    oldEquipmentRelocation.Equipment = null;
                }
        }
        public void RemoveAllRelocationEquipment()
        {
            if (relocationEquipment != null)
            {
                System.Collections.ArrayList tmpEquipmentRelocation = new System.Collections.ArrayList();
                foreach (EquipmentRelocation oldEquipmentRelocation in relocationEquipment)
                    tmpEquipmentRelocation.Add(oldEquipmentRelocation);
                relocationEquipment.Clear();
                foreach (EquipmentRelocation oldEquipmentRelocation in tmpEquipmentRelocation)
                    oldEquipmentRelocation.Equipment = null;
                tmpEquipmentRelocation.Clear();
            }
        }
    }
}