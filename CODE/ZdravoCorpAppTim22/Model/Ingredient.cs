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
                        oldMedicineData.RemoveIngredient(this);
                    }
                    if (value != null)
                    {
                        this.medicineData = value;
                        this.medicineData.AddIngredient(this);
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
