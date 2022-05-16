using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class MedicineController : GenericController<MedicineService, Medicine>
    {
        private static MedicineController instance;
        private MedicineController() : base(MedicineService.Instance) { }
        public static MedicineController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineController();
                }
                return instance;
            }
        }
        public List<Medicine> GetAllFree()
        {
            return MedicineService.Instance.GetAllFree();
        }
        
        public List<Medicine> GetAllApproved()
        {
            return MedicineService.Instance.GetAllApproved();
        }
    }
}
