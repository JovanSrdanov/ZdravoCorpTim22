using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;

namespace ZdravoCorpAppTim22.Model
{
    public class MedicineData : IHasID
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [JsonIgnore]
        private Approval approval;
        [JsonIgnore]
        public Approval Approval
        {
            get => approval;
            set
            {
                if (value != approval)
                {
                    approval = value;
                    approval.MedicineData = this;
                }
            }
        }

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

        [JsonIgnore]
        private ObservableCollection<Ingredient> ingredient;
        [JsonIgnore]
        public ObservableCollection<Ingredient> Ingredient
        {
            get
            {
                if (ingredient == null)
                    ingredient = new ObservableCollection<Ingredient>();
                return ingredient;
            }
            set
            {
                RemoveAllIngredient();
                if (value != null)
                {
                    foreach (Ingredient oIngredient in value)
                        AddIngredient(oIngredient);
                }
            }
        }
        [JsonIgnore]
        private ObservableCollection<MedicineData> replacements;
        [JsonIgnore]
        public ObservableCollection<MedicineData> Replacements
        {
            get
            {
                if (replacements == null)
                    replacements = new ObservableCollection<MedicineData>();
                return replacements;
            }
            set
            {
                RemoveAllReplacements();
                if (value != null)
                {
                    foreach (MedicineData oReplacement in value)
                        AddReplacement(oReplacement);
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
                Approval = md.Approval;
                Ingredient = md.Ingredient;
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

        public void AddIngredient(Ingredient newIngredient)
        {
            if (newIngredient == null)
                return;
            if (this.ingredient == null)
                this.ingredient = new ObservableCollection<Ingredient>();
            if (!this.ingredient.Contains(newIngredient))
            {
                this.ingredient.Add(newIngredient);
                newIngredient.MedicineData = this;
            }
        }
        public void RemoveIngredient(Ingredient oldIngredient)
        {
            if (oldIngredient == null)
                return;
            if (this.ingredient != null)
                if (this.ingredient.Contains(oldIngredient))
                {
                    this.ingredient.Remove(oldIngredient);
                    oldIngredient.MedicineData = null;
                }
        }
        public void RemoveAllIngredient()
        {
            if (ingredient != null)
            {
                System.Collections.ArrayList tmpIngredient = new System.Collections.ArrayList();
                foreach (Ingredient oldIngredient in ingredient)
                    tmpIngredient.Add(oldIngredient);
                ingredient.Clear();
                foreach (Ingredient oldIngredient in tmpIngredient)
                    oldIngredient.MedicineData = null;
                tmpIngredient.Clear();
            }
        }

        public void AddReplacement(MedicineData newReplacement)
        {
            if (newReplacement == null)
                return;
            if (this.replacements == null)
                this.replacements = new ObservableCollection<MedicineData>();
            if (!this.replacements.Contains(newReplacement))
            {
                this.replacements.Add(newReplacement);
            }
        }
        public void RemoveReplacement(MedicineData oldReplacement)
        {
            if (oldReplacement == null)
                return;
            if (this.replacements != null)
                if (this.replacements.Contains(oldReplacement))
                {
                    this.replacements.Remove(oldReplacement);
                }
        }
        public void RemoveAllReplacements()
        {
            if (replacements != null)
            {
                replacements.Clear();
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }
}
