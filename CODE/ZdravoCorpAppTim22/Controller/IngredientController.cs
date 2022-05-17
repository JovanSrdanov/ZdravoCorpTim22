using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class IngredientController : GenericController<IngredientService, Ingredient>
    {
        private static IngredientController instance;
        private IngredientController() : base(IngredientService.Instance) { }
        public static IngredientController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IngredientController();
                }
                return instance;
            }
        }
    }
}
