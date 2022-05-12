using Model;
using Service;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

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
        public void Create(Room source, Room destination, Interval interval, List<Equipment> equipment)
        {
            if (equipment.Count > 0)
            {
                EquipmentRelocation equipmentRelocation = new EquipmentRelocation
                {
                    SourceRoom = source,
                    DestinationRoom = destination,
                    Interval = interval,
                    Equipment = equipment
                };
                foreach (Equipment eq in equipment)
                {
                    EquipmentController.Instance.Create(eq);
                }
                Create(equipmentRelocation);
            }
        }
        public void DaemonMethod()
        {
            EquipmentRelocationService.Instance.DaemonMethod();
        }
   }
}