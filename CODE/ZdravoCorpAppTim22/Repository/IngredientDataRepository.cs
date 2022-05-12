using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository.Generic;

namespace ZdravoCorpAppTim22.Repository
{
    public class IngredientDataRepository : GenericRepository<IngredientData>
    {
        public static string FileName = "IngredientInstanceData.json";
        private static IngredientDataRepository instance;
        private IngredientDataRepository() : base(FileName) { }
        public static IngredientDataRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IngredientDataRepository();
                }

                return instance;
            }
        }
    }
}
