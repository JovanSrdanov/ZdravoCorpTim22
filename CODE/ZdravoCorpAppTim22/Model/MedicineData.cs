using Model;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;

namespace ZdravoCorpAppTim22.Model
{
    public class MedicineData : IHasID
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Doctor ApprovedBy { get; set; }
        public ObservableCollection<string> Ingredients { get; set; }
        [JsonIgnore]
        private ObservableCollection<Medicine> medicine;
        [JsonIgnore]
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
        public MedicineData() { }
        public MedicineData(int id)
        {
            Id = id;
        }
        public MedicineData(int id, string name) : this(id)
        {
            Name = name;
        }
        public MedicineData(MedicineData md)
        {
            if (md != null)
            {
                Id = md.Id;
                Name = md.Name;
                Ingredients = new ObservableCollection<string>(md.Ingredients);
            }
        }
        
        public void AddMedicine(Medicine newMedicine)
        {
            if (newMedicine == null)
                return;
            if (this.medicine == null)
                this.medicine = new ObservableCollection<Medicine>();
            if (!this.medicine.Contains(newMedicine))
            {
                this.medicine.Add(newMedicine);
                newMedicine.MedicineData = this;
            }
        }
        public void RemoveMedicine(Medicine oldMedicine)
        {
            if (oldMedicine == null)
                return;
            if (this.medicine != null)
                if (this.medicine.Contains(oldMedicine))
                {
                    this.medicine.Remove(oldMedicine);
                    oldMedicine.MedicineData = null;
                }
        }
        public void RemoveAllMedicine()
        {
            if (medicine != null)
            {
                System.Collections.ArrayList tmpMedicine = new System.Collections.ArrayList();
                foreach (Medicine oldMedicine in medicine)
                    tmpMedicine.Add(oldMedicine);
                medicine.Clear();
                foreach (Medicine oldMedicine in tmpMedicine)
                    oldMedicine.MedicineData = null;
                tmpMedicine.Clear();
            }
        }
    }
}
