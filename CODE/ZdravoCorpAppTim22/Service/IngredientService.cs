using System.Collections.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class IngredientService : GenericService<IngredientRepository, Ingredient>
    {
        private IngredientService() : base(IngredientRepository.Instance) { }
        public static IngredientService Instance
        {
            get
            {
                return new IngredientService();
            }
        }

        public void DeleteByList(List<Ingredient> list)
        {
            foreach (Ingredient ingredient in list)
            {
                ingredient.MedicineData = null;
                DeleteByID(ingredient.Id);
            }
        }

        public void AddIngredientsToMedicineData(MedicineData medicineData, List<Ingredient> ingredients)
        {
            DeleteByMedicineData(medicineData);
            foreach (Ingredient ingredient in ingredients)
            {
                medicineData.AddIngredient(ingredient);
                Create(ingredient);
            }
        }

        public void DeleteByMedicineData(MedicineData medicineData)
        {
            List<Ingredient> ingredientToRemove = new List<Ingredient>();
            foreach (Ingredient ingredient in medicineData.Ingredient)
            {
                ingredientToRemove.Add(ingredient);
            }
            DeleteByList(ingredientToRemove);
            medicineData.RemoveAllIngredient();
        }
    }
}
