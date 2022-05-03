using Model;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class EquipmentController : GenericController<EquipmentService, Equipment>
    {
        private static EquipmentController instance;
        private EquipmentController() : base(EquipmentService.Instance) { }
        public static EquipmentController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentController();
                }
                return instance;
            }
        }

        public List<Equipment> GetWarehouseEquipment()
        {
            return EquipmentService.Instance.GetWarehouseEquipment();
        }

        public void AddWarehouseEquipment(Equipment eq)
        {
            EquipmentService.Instance.AddWarehouseEquipment(eq);
        }

        public void AddRoomEquipment(Room destination, Equipment eq)
        {
            EquipmentService.Instance.AddRoomEquipment(destination, eq);
        }
    }
}