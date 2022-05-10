using System.Text.Json.Serialization;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class Ingredient : IHasID
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        [JsonConverter(typeof(IngredientDataToIDConverter))]
        private IngredientData ingredientData;
        [JsonConverter(typeof(IngredientDataToIDConverter))]
        public IngredientData IngredientData
        {
            get
            {
                return ingredientData;
            }
            set
            {
                if (this.ingredientData == null || !this.ingredientData.Equals(value))
                {
                    if (this.ingredientData != null)
                    {
                        IngredientData oldIngredientData = this.ingredientData;
                        this.ingredientData = null;
                        oldIngredientData.RemoveIngredient(this);
                    }
                    if (value != null)
                    {
                        this.ingredientData = value;
                        this.ingredientData.AddIngredient(this);
                    }
                }
            }
        }
        [JsonConverter(typeof(MedicineToIDConverter))]
        private Medicine medicine;
        [JsonConverter(typeof(MedicineToIDConverter))]
        public Medicine Medicine
        {
            get
            {
                return medicine;
            }
            set
            {
                if (this.medicine == null || !this.medicine.Equals(value))
                {
                    if (this.medicine != null)
                    {
                        Medicine oldMedicine = this.medicine;
                        this.medicine = null;
                        oldMedicine.RemoveIngredient(this);
                    }
                    if (value != null)
                    {
                        this.medicine = value;
                        this.medicine.AddIngredient(this);
                    }
                }
            }
        }

        [JsonConstructor]
        public Ingredient() { }
        public Ingredient(IngredientData ingredientData)
        {
            IngredientData = ingredientData;
            Amount = 1;
        }
    }
}
