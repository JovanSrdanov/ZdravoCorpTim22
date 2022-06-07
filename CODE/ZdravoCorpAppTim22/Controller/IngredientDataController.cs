using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class IngredientDataController : GenericController<IngredientDataService, IngredientData>
    {
        private static IngredientDataController instance;
        private IngredientDataController() : base(IngredientDataService.Instance) { }
        public static IngredientDataController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new IngredientDataController();
                }
                return instance;
            }
        }

        public bool isPatientAlergic(string alergy, Medicine medicine)
        {
            return IngredientDataService.Instance.isPatientAlergic(alergy, medicine);
        }
    }
}
