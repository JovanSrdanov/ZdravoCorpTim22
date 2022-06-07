using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Model.Utility;

namespace Model
{
    public class Room : IHasID
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public RoomType Type { get; set; }
        public string Name { get; set; }
        public double Surface { get; set; }

        [JsonIgnore]
        private List<Equipment> equipment;
        [JsonIgnore]
        private List<MedicalAppointment> medicalAppointment;
        [JsonIgnore]
        private List<Renovation> renovations;
        [JsonIgnore]
        private List<EquipmentRelocation> relocationSources;
        [JsonIgnore]
        private List<EquipmentRelocation> relocationDestinations;
        [JsonIgnore]
        private List<RoomMerge> mergesWhereFirst;
        [JsonIgnore]
        private List<RoomMerge> mergesWhereSecond;
        [JsonIgnore]
        private List<RoomDiverge> diverges;

        #region properties

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
        [JsonIgnore]
        public List<RoomMerge> MergesWhereFirst
        {
            get
            {
                if (mergesWhereFirst == null)
                {
                    mergesWhereFirst = new List<RoomMerge>();
                }
                return mergesWhereFirst;
            }
            set
            {
                RemoveAllMergesWhereFirst();
                if (value != null)
                {
                    foreach (RoomMerge oMerge in value)
                        AddMergeWhereFirst(oMerge);
                }
            }
        }
        [JsonIgnore]
        public List<RoomMerge> MergesWhereSecond
        {
            get
            {
                if (mergesWhereSecond == null)
                {
                    mergesWhereSecond = new List<RoomMerge>();
                }
                return mergesWhereSecond;
            }
            set
            {
                RemoveAllMergesWhereSecond();
                if (value != null)
                {
                    foreach (RoomMerge oMerge in value)
                        AddMergeWhereSecond(oMerge);
                }
            }
        }
        [JsonIgnore]
        public List<RoomDiverge> Diverges
        {
            get
            {
                if (diverges == null)
                {
                    diverges = new List<RoomDiverge>();
                }
                return diverges;
            }
            set
            {
                RemoveAllDiverges();
                if (value != null)
                {
                    foreach (RoomDiverge oDiverge in value)
                        AddDiverge(oDiverge);
                }
            }
        }
        #endregion

        [JsonConstructor]
        public Room() { }

        public Room(int id, int level, RoomType type, string name, double surface)
        {
            this.Id = id;
            this.Level = level;
            this.Type = type;
            this.Name = name;
            this.Surface = surface;
        }
        public override string ToString()
        {
            return Name;
        }
        public bool IsAvailable(DateTime start, DateTime end)
        {
            if (MergesWhereFirst.Count > 0 || MergesWhereSecond.Count > 0 || Diverges.Count > 0) return false;
            foreach (MedicalAppointment medicalAppointmentRoom in MedicalAppointment)
            {
                if (!((medicalAppointmentRoom.Interval.Start >= end) || (medicalAppointmentRoom.Interval.End <= start)))
                {
                    return false;
                }
            }
            foreach (Renovation ren in Renovations)
            {
                if (!((ren.Interval.Start >= end) || (ren.Interval.End <= start)))
                {
                    return false;
                }
            }
            foreach (EquipmentRelocation er in RelocationSources)
            {
                if (!((er.Interval.Start >= end) || (er.Interval.End <= start)))
                {
                    return false;
                }
            }
            foreach (EquipmentRelocation er in RelocationDestinations)
            {
                if (!((er.Interval.Start >= end) || (er.Interval.End <= start)))
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsAvailable(Interval interval)
        {
            return IsAvailable(interval.Start, interval.End);
        }
        public bool CanMergeOrDiverge()
        {
            if (MedicalAppointment.Count > 0 || Renovations.Count > 0 || RelocationSources.Count > 0 || RelocationDestinations.Count > 0 || MergesWhereFirst.Count > 0 || MergesWhereSecond.Count > 0 || Diverges.Count > 0)
            {
                return false;
            }
            return true;
        }

        #region boilerplate
        public void AddEquipment(Equipment newEquipment)
        {
            if (newEquipment == null)
                return;
            if (this.equipment == null)
                this.equipment = new List<Equipment>();
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

        public void RemoveMergeWhereFirst(RoomMerge oldMerge)
        {
            if (oldMerge == null)
                return;
            if (this.mergesWhereFirst != null)
                if (this.mergesWhereFirst.Contains(oldMerge))
                {
                    this.mergesWhereFirst.Remove(oldMerge);
                    oldMerge.FirstRoom = null;
                }
        }
        public void AddMergeWhereFirst(RoomMerge newMerge)
        {
            if (newMerge == null)
                return;
            if (this.mergesWhereFirst == null)
                this.mergesWhereFirst = new List<RoomMerge>();
            if (!this.mergesWhereFirst.Contains(newMerge))
            {
                this.mergesWhereFirst.Add(newMerge);
                newMerge.FirstRoom = this;
            }
        }
        public void RemoveAllMergesWhereFirst()
        {
            if (mergesWhereFirst != null)
            {
                System.Collections.ArrayList tmpMerges = new System.Collections.ArrayList();
                foreach (RoomMerge oldMerge in mergesWhereFirst)
                    tmpMerges.Add(oldMerge);
                mergesWhereFirst.Clear();
                foreach (RoomMerge oldMerge in tmpMerges)
                    oldMerge.FirstRoom = null;
                tmpMerges.Clear();
            }
        }

        public void RemoveMergeWhereSecond(RoomMerge oldMerge)
        {
            if (oldMerge == null)
                return;
            if (this.mergesWhereSecond != null)
                if (this.mergesWhereSecond.Contains(oldMerge))
                {
                    this.mergesWhereSecond.Remove(oldMerge);
                    oldMerge.SecondRoom = null;
                }
        }
        public void AddMergeWhereSecond(RoomMerge newMerge)
        {
            if (newMerge == null)
                return;
            if (this.mergesWhereSecond == null)
                this.mergesWhereSecond = new List<RoomMerge>();
            if (!this.mergesWhereSecond.Contains(newMerge))
            {
                this.mergesWhereSecond.Add(newMerge);
                newMerge.SecondRoom = this;
            }
        }
        public void RemoveAllMergesWhereSecond()
        {
            if (mergesWhereSecond != null)
            {
                System.Collections.ArrayList tmpMerges = new System.Collections.ArrayList();
                foreach (RoomMerge oldMerge in mergesWhereSecond)
                    tmpMerges.Add(oldMerge);
                mergesWhereSecond.Clear();
                foreach (RoomMerge oldMerge in tmpMerges)
                    oldMerge.SecondRoom = null;
                tmpMerges.Clear();
            }
        }

        public void RemoveDiverge(RoomDiverge oldDiverge)
        {
            if (oldDiverge == null)
                return;
            if (this.diverges != null)
                if (this.diverges.Contains(oldDiverge))
                {
                    this.diverges.Remove(oldDiverge);
                    oldDiverge.SourceRoom = null;
                }
        }
        public void AddDiverge(RoomDiverge newDiverge)
        {
            if (newDiverge == null)
                return;
            if (this.diverges == null)
                this.diverges = new List<RoomDiverge>();
            if (!this.diverges.Contains(newDiverge))
            {
                this.diverges.Add(newDiverge);
                newDiverge.SourceRoom = this;
            }
        }
        public void RemoveAllDiverges()
        {
            if (diverges != null)
            {
                System.Collections.ArrayList tmpDiverges = new System.Collections.ArrayList();
                foreach (RoomDiverge oldDiverge in diverges)
                    tmpDiverges.Add(oldDiverge);
                diverges.Clear();
                foreach (RoomDiverge oldDiverge in tmpDiverges)
                    oldDiverge.SourceRoom = null;
                tmpDiverges.Clear();
            }
        }
        #endregion
    }
}