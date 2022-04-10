using System;
using System.ComponentModel;

namespace Model
{
   public class Room
   {
            
      
      public int id { get; set; }
        public int level { get; set; }
        public RoomType type { get; set; }
        public string name { get; set; }

        public System.Collections.Generic.List<Equipment> equipment;

        public Room(int id, int level, RoomType type, string name)
        {
            this.id = id;
            this.level = level;
            this.type = type;
            this.name = name;
        }

        public bool IsAvailable(DateTime time)
        {
            throw new NotImplementedException();
        }
      
      public System.Collections.Generic.List<Equipment> Equipment
      {
         get
         {
            if (equipment == null)
               equipment = new System.Collections.Generic.List<Equipment>();
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
      public System.Collections.Generic.List<MedicalAppointment> medicalAppointment;
      
      public System.Collections.Generic.List<MedicalAppointment> MedicalAppointment
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