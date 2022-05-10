using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class Medicine : IHasID
    {
        public int Id { get; set; }
        public int Amount { get; set; }

        [JsonConstructor]
        public Medicine() { }
        public Medicine(Medicine m)
        {
            if (m != null)
            {
                Id = m.Id;
                Amount = m.Amount;
                MedicineData = new MedicineData(m.MedicineData);
                Ingredient = m.Ingredient;
            }
        }

        [JsonConverter(typeof(MedicineDataToIDConverter))]
        private MedicineData medicineData;
        [JsonConverter(typeof(MedicineDataToIDConverter))]
        public MedicineData MedicineData
        {
            get
            {
                return medicineData;
            }
            set
            {
                if (this.medicineData == null || !this.medicineData.Equals(value))
                {
                    if (this.medicineData != null)
                    {
                        MedicineData oldMedicineData = this.medicineData;
                        this.medicineData = null;
                        oldMedicineData.RemoveMedicine(this);
                    }
                    if (value != null)
                    {
                        this.medicineData = value;
                        this.medicineData.AddMedicine(this);
                    }
                }
            }
        }

        [JsonConverter(typeof(MedicalReceiptToIDConverter))]
        private MedicalReceipt medicalReceipt;
        [JsonConverter(typeof(MedicalReceiptToIDConverter))]

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
        public MedicalReceipt MedicalReceipt
        {
            get
            {
                return medicalReceipt;
            }
            set
            {
                if (this.medicalReceipt == null || !this.medicalReceipt.Equals(value))
                {
                    if (this.medicalReceipt != null)
                    {
                        MedicalReceipt oldMedicalReceipt = this.medicalReceipt;
                        this.medicalReceipt = null;
                        oldMedicalReceipt.RemoveMedicine(this);
                    }
                    if (value != null)
                    {
                        this.medicalReceipt = value;
                        this.medicalReceipt.AddMedicine(this);
                    }
                }
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
                newIngredient.Medicine = this;
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
                    oldIngredient.Medicine = null;
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
                    oldIngredient.Medicine = null;
                tmpIngredient.Clear();
            }
        }
    }
}
