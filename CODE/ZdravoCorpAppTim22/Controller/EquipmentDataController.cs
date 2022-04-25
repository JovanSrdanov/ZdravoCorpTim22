using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class EquipmentDataController : GenericController<EquipmentDataService, EquipmentData>
    {
        private static EquipmentDataController instance;
        private EquipmentDataController() : base(EquipmentDataService.Instance) { }
        public static EquipmentDataController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentDataController();
                }
                return instance;
            }
        }
    }
}