using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class MedicineDataController : GenericController<MedicineDataService, MedicineData>
    {
        private static MedicineDataController instance;
        private MedicineDataController() : base(MedicineDataService.Instance) { }
        public static MedicineDataController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicineDataController();
                }
                return instance;
            }
        }
    }
}
