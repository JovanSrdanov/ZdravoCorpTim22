using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace Model
{
    public class Equipment : IHasID
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        [JsonConstructor]
        public Equipment() { }
        public Equipment(Equipment eq)
        {
            Id = eq.Id;
            Amount = eq.Amount;
            EquipmentData = new EquipmentData(eq.EquipmentData);
        }

        [JsonConverter(typeof(EquipmentDataToIDConverter))]
        public EquipmentData equipmentData;
        [JsonConverter(typeof(EquipmentDataToIDConverter))]
        public EquipmentData EquipmentData
        {
            get
            {
                return equipmentData;
            }
            set
            {
                if (this.equipmentData == null || !this.equipmentData.Equals(value))
                {
                    if (this.equipmentData != null)
                    {
                        EquipmentData oldEquipmentData = this.equipmentData;
                        this.equipmentData = null;
                        oldEquipmentData.RemoveEquipment(this);
                    }
                    if (value != null)
                    {
                        this.equipmentData = value;
                        this.equipmentData.AddEquipment(this);
                    }
                }
            }
        }

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
        [JsonConverter(typeof(EquipmentRelocationToIDConverter))]
        public EquipmentRelocation equipmentRelocation;
        [JsonConverter(typeof(EquipmentRelocationToIDConverter))]
        public EquipmentRelocation EquipmentRelocation
        {
            get
            {
                return equipmentRelocation;
            }
            set
            {
                if (this.equipmentRelocation == null || !this.equipmentRelocation.Equals(value))
                {
                    if (this.equipmentRelocation != null)
                    {
                        EquipmentRelocation oldEquipmentRelocation = this.equipmentRelocation;
                        this.equipmentRelocation = null;
                        oldEquipmentRelocation.RemoveEquipment(this);
                    }
                    if (value != null)
                    {
                        this.equipmentRelocation = value;
                        this.equipmentRelocation.AddEquipment(this);
                    }
                }
            }
        }
    }
}