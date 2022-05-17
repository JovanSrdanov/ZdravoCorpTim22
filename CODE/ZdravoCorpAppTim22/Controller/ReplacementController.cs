using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class ReplacementController : GenericController<ReplacementService, Replacement>
    {
        private static ReplacementController instance;
        private ReplacementController() : base(ReplacementService.Instance) { }
        public static ReplacementController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ReplacementController();
                }

                return instance;
            }
        }
        public void DeleteByReplacementTarget(MedicineData medicineData)
        {
            ReplacementService.Instance.DeleteByReplacementTarget(medicineData);
        }
    }
}
