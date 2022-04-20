using System;
using System.Collections.Generic;
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
        [JsonIgnore]
        public List<EquipmentRelocation> relocationSources;
        [JsonIgnore]
        public List<EquipmentRelocation> RelocationSources
        {
            get
            {
                if (relocationSources == null)
                    relocationSources = new List<EquipmentRelocation>();
                return relocationSources;
            }
            set
            {
                RemoveAllRelocationSources();
                if (value != null)
                {
                    foreach (EquipmentRelocation oEquipmentRelocation in value)
                        AddRelocationSource(oEquipmentRelocation);
                }
            }
        }
        [JsonIgnore]
        public List<EquipmentRelocation> relocationDestinations;
        [JsonIgnore]
        public List<EquipmentRelocation> RelocationDestinations
        {
            get
            {
                if (relocationDestinations == null)
                    relocationDestinations = new List<EquipmentRelocation>();
                return relocationDestinations;
            }
            set
            {
                RemoveAllRelocationDestinations();
                if (value != null)
                {
                    foreach (EquipmentRelocation oEquipmentRelocation in value)
                        AddRelocationDestination(oEquipmentRelocation);
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
                foreach(Renovation ren in Renovations)
                {
                    if(!((ren.Interval.Start >= end) || (ren.Interval.End <= start)))
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
        
        public void AddRelocationSource(EquipmentRelocation newEquipmentRelocation)
        {
            if (newEquipmentRelocation == null)
                return;
            if (this.relocationSources == null)
                this.relocationSources = new List<EquipmentRelocation>();
            if (!this.relocationSources.Contains(newEquipmentRelocation))
            {
                this.relocationSources.Add(newEquipmentRelocation);
                newEquipmentRelocation.SourceRoom = this;
            }
        }
        public void RemoveRelocationSource(EquipmentRelocation oldEquipmentRelocation)
        {
            if (oldEquipmentRelocation == null)
                return;
            if (this.relocationSources != null)
                if (this.relocationSources.Contains(oldEquipmentRelocation))
                {
                    this.relocationSources.Remove(oldEquipmentRelocation);
                    oldEquipmentRelocation.SourceRoom = null;
                }
        }
        public void RemoveAllRelocationSources()
        {
            if (relocationSources != null)
            {
                System.Collections.ArrayList tmpEquipmentRelocation = new System.Collections.ArrayList();
                foreach (EquipmentRelocation oldEquipmentRelocation in relocationSources)
                    tmpEquipmentRelocation.Add(oldEquipmentRelocation);
                relocationSources.Clear();
                foreach (EquipmentRelocation oldEquipmentRelocation in tmpEquipmentRelocation)
                    oldEquipmentRelocation.SourceRoom = null;
                tmpEquipmentRelocation.Clear();
            }
        }

        public void AddRelocationDestination(EquipmentRelocation newEquipmentRelocation)
        {
            if (newEquipmentRelocation == null)
                return;
            if (this.relocationDestinations == null)
                this.relocationDestinations = new List<EquipmentRelocation>();
            if (!this.relocationDestinations.Contains(newEquipmentRelocation))
            {
                this.relocationDestinations.Add(newEquipmentRelocation);
                newEquipmentRelocation.DestinationRoom = this;
            }
        }
        public void RemoveRelocationDestination(EquipmentRelocation oldEquipmentRelocation)
        {
            if (oldEquipmentRelocation == null)
                return;
            if (this.relocationDestinations != null)
                if (this.relocationDestinations.Contains(oldEquipmentRelocation))
                {
                    this.relocationDestinations.Remove(oldEquipmentRelocation);
                    oldEquipmentRelocation.DestinationRoom = null;
                }
        }
        public void RemoveAllRelocationDestinations()
        {
            if (relocationDestinations != null)
            {
                System.Collections.ArrayList tmpEquipmentRelocation = new System.Collections.ArrayList();
                foreach (EquipmentRelocation oldEquipmentRelocation in relocationDestinations)
                    tmpEquipmentRelocation.Add(oldEquipmentRelocation);
                relocationDestinations.Clear();
                foreach (EquipmentRelocation oldEquipmentRelocation in tmpEquipmentRelocation)
                    oldEquipmentRelocation.DestinationRoom = null;
                tmpEquipmentRelocation.Clear();
            }
        }
    }
}