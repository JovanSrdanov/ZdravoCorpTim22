using Model;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;

namespace ZdravoCorpAppTim22.Model
{
    public class EquipmentData : IHasID
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public EquipmentType Type { get; set; }
        public double Price { get; set; }

        [JsonIgnore]
        public ObservableCollection<Equipment> equipment;
        [JsonIgnore]
        public ObservableCollection<Equipment> Equipment
        {
            get
            {
                if (equipment == null)
                    equipment = new ObservableCollection<Equipment>();
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

        [JsonConstructor]
        public EquipmentData() { }
        public EquipmentData(int id)
        {
            this.Id = id;
        }
        public EquipmentData(int id, string name, EquipmentType type) : this(id)
        {
            this.Name = name;
            this.Type = type;
        }
        public EquipmentData(EquipmentData eq)
        {
            if (eq != null)
            {
                Id = eq.Id;
                Name = eq.Name;
                Type = eq.Type;
            }
        }


        public void AddEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipment == null)
                this.equipment = new ObservableCollection<Equipment>();
            if (!this.equipment.Contains(newEquipment))
            {
                this.equipment.Add(newEquipment);
                newEquipment.EquipmentData = this;
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
                    oldEquipment.EquipmentData = null;
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
                    oldEquipment.EquipmentData = null;
                tmpEquipment.Clear();
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
