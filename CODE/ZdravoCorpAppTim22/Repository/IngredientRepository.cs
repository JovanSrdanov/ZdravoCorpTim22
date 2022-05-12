using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class IngredientRepository : GenericRepository<Ingredient>
    {
        public static string FileName = "IngredientData.json";
        private static IngredientRepository instance;
        private IngredientRepository() : base(FileName) { }
        public static IngredientRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IngredientRepository();
                }

                return instance;
            }
        }
    }
}
