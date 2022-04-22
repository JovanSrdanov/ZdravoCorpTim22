using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}