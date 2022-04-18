using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Generic;

namespace Model
{
    public class Room : IHasID
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public RoomType Type { get; set; }
        public string Name { get; set; }

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

        [JsonIgnore]
        public List<MedicalAppointment> medicalAppointment;

        [JsonIgnore]
        public List<MedicalAppointment> MedicalAppointment
        {
            get
            {
                if (medicalAppointment == null)
                    medicalAppointment = new System.Collections.Generic.List<MedicalAppointment>();
                return medicalAppointment;
            }
            set
            {
                RemoveAllMedicalAppointment();
                if (value != null)
                {
                    foreach (MedicalAppointment oMedicalAppointment in value)
                        AddMedicalAppointment(oMedicalAppointment);
                }
            }
        }
        [JsonIgnore]
        public List<Renovation> renovations;
        [JsonIgnore]
        public List<Renovation> Renovations
        {
            get
            {
                if (renovations == null)
                {
                    renovations = new List<Renovation>();
                }
                return renovations;
            }
            set
            {
                RemoveAllRenovations();
                if (value != null)
                {
                    foreach (Renovation oRenovation in value)
                        AddRenovation(oRenovation);
                }
            }
        }

        [JsonConstructor]
        public Room() { }

        public Room(int id, int level, RoomType type, string name)
        {
            this.Id = id;
            this.Level = level;
            this.Type = type;
            this.Name = name;
        }

        public bool IsAvailable(DateTime start, DateTime end)
        {
            if (medicalAppointment == null)
                return true;
            else
            {
                foreach (MedicalAppointment medicalAppointmentRoom in medicalAppointment)
                {
                    if (! ((medicalAppointmentRoom.MedicalAppointmentStartDateTime >= end) || (medicalAppointmentRoom.MedicalAppointmentEndDateTime <= start)) )
                    {
                        return false;
                    }
                }
                return true;
            }
        }
      
        public void AddEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipment == null)
                this.equipment = new System.Collections.Generic.List<Equipment>();
            if (!this.equipment.Contains(newEquipment))
            {
                this.equipment.Add(newEquipment);
                newEquipment.Room = this;
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
                    oldEquipment.Room = null;
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
                    oldEquipment.Room = null;
                tmpEquipment.Clear();
            }
        }

        public void RemoveRenovation(Renovation oldRenovation)
        {
            if (oldRenovation == null)
                return;
            if (this.renovations != null)
                if (this.renovations.Contains(oldRenovation))
                {
                    this.renovations.Remove(oldRenovation);
                    oldRenovation.Room = null;
                }
        }

        public void AddRenovation(Renovation newRenovation)
        {
            if (newRenovation == null)
                return;
            if (this.renovations == null)
                this.renovations = new List<Renovation>();
            if (!this.renovations.Contains(newRenovation))
            {
                this.renovations.Add(newRenovation);
                newRenovation.Room = this;
            }
        }

        public void RemoveAllRenovations()
        {
            if (renovations != null)
            {
                System.Collections.ArrayList tmpRenovations = new System.Collections.ArrayList();
                foreach (Renovation oldRenovation in renovations)
                    tmpRenovations.Add(oldRenovation);
                renovations.Clear();
                foreach (Renovation oldRenovation in tmpRenovations)
                    oldRenovation.Room = null;
                tmpRenovations.Clear();
            }
        }

        
      
        public void AddMedicalAppointment(MedicalAppointment newMedicalAppointment)
        {
            if (newMedicalAppointment == null)
                return;
            if (this.medicalAppointment == null)
                this.medicalAppointment = new System.Collections.Generic.List<MedicalAppointment>();
            if (!this.medicalAppointment.Contains(newMedicalAppointment))
            {
                this.medicalAppointment.Add(newMedicalAppointment);
                newMedicalAppointment.Room = this;
            }
        }
      
        public void RemoveMedicalAppointment(MedicalAppointment oldMedicalAppointment)
        {
            if (oldMedicalAppointment == null)
                return;
            if (this.medicalAppointment != null)
                if (this.medicalAppointment.Contains(oldMedicalAppointment))
                {
                    this.medicalAppointment.Remove(oldMedicalAppointment);
                    oldMedicalAppointment.Room = null;
                }
        }
      
        public void RemoveAllMedicalAppointment()
        {
            if (medicalAppointment != null)
            {
                System.Collections.ArrayList tmpMedicalAppointment = new System.Collections.ArrayList();
                foreach (MedicalAppointment oldMedicalAppointment in medicalAppointment)
                    tmpMedicalAppointment.Add(oldMedicalAppointment);
                medicalAppointment.Clear();
                foreach (MedicalAppointment oldMedicalAppointment in tmpMedicalAppointment)
                    oldMedicalAppointment.Room = null;
                tmpMedicalAppointment.Clear();
            }
        }

    }
}