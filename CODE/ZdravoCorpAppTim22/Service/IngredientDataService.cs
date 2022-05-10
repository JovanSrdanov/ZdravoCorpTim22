using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class IngredientDataService : GenericService<IngredientDataRepository, IngredientData>
    {
        private static IngredientDataService instance;
        private IngredientDataService() : base(IngredientDataRepository.Instance) { }
        public static IngredientDataService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IngredientDataService();
                }

                return instance;
            }
        }
    }
}
