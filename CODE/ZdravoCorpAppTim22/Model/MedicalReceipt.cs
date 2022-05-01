using System;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;

namespace ZdravoCorpAppTim22.Model
{
    public class MedicalReceipt : IHasID
    {
        public int Id { get; set; }
        public DateTime EndDate { get; set; }   
        public string Time { get; set; } 
        public string AdditionalInstructions { get; set; }

        public ObservableCollection<Medicine> medicine;
        public ObservableCollection<Medicine> Medicine
        {
            get
            {
                if (medicine == null)
                    medicine = new ObservableCollection<Medicine>();
                return medicine;
            }
            set
            {
                RemoveAllMedicine();
                if (value != null)
                {
                    foreach (Medicine oMedicine in value)
                        AddMedicine(oMedicine);
                }
            }
        }

        [JsonConstructor]
        public MedicalReceipt() { }

        public MedicalReceipt(DateTime endDate, string time, ObservableCollection<Medicine> medicine, string additionalInstructions)
        {
            EndDate = endDate;
            Time = time;
            Medicine = medicine;
            AdditionalInstructions = additionalInstructions;
         }

        public void AddMedicine(Medicine newMedicine)
        {
            if (newMedicine == null)
                return;
            if (this.medicine == null)
                this.medicine = new ObservableCollection<Medicine>();
            if (!this.medicine.Contains(newMedicine))
                this.medicine.Add(newMedicine);
        }
        public void RemoveMedicine(Medicine oldMedicine)
        {
            if (oldMedicine == null)
                return;
            if (this.medicine != null)
                if (this.medicine.Contains(oldMedicine))
                    this.medicine.Remove(oldMedicine);
        }

        public void RemoveAllMedicine()
        {
            if (medicine != null)
                medicine.Clear();
        }
    }
}
