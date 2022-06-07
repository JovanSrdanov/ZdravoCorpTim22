using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class IngredientDataService : GenericService<IngredientDataRepository, IngredientData>
    {
        private IngredientDataService() : base(IngredientDataRepository.Instance) { }
        public static IngredientDataService Instance
        {
            get
            {
                return new IngredientDataService();
            }
        }

        public bool isPatientAlergic(string alergy, Medicine medicine)
        {
            bool returnValue = false;
            foreach (var ingredient in medicine.MedicineData.Ingredient)
            {
                if (ingredient.IngredientData.Name.Equals(alergy))
                {
                    returnValue = true;
                    break;
                }
            }
            return returnValue;
        }
    }
}
