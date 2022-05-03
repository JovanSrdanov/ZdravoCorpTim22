using Service;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;

namespace Controller
{
   public class EquipmentRelocationController : GenericController<EquipmentRelocationService, EquipmentRelocation>
    {
        private static EquipmentRelocationController instance;
        private EquipmentRelocationController() : base(EquipmentRelocationService.Instance) { }
        public static EquipmentRelocationController Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentRelocationController();
                }
                return instance;
            }
        }
        public void DaemonMethod()
        {
            EquipmentRelocationService.Instance.DaemonMethod();
        }
   }
}