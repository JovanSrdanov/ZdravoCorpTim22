using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class IngredientService : GenericService<IngredientRepository, Ingredient>
    {
        private static IngredientService instance;
        private IngredientService() : base(IngredientRepository.Instance) { }
        public static IngredientService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IngredientService();
                }

                return instance;
            }
        }
    }
}
