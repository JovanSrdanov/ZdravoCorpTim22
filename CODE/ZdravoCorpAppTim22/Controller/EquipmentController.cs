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
        public void Update(Equipment oldEquipment, string name, int amount, EquipmentType type)
        {
            oldEquipment.EquipmentData.Name = name;
            oldEquipment.EquipmentData.Type = type;
            oldEquipment.Amount = amount;
            EquipmentDataController.Instance.Update(oldEquipment.EquipmentData);
            Update(oldEquipment);
        }

        public void DeleteWarehouseEquipmentByID(int id)
        {
            EquipmentService.Instance.DeleteWarehouseEquipmentByID(id);
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

        public Equipment GetEquipmentAndUpdateSource(Equipment source, int amount, bool deleteIfEmpty)
        {
            return EquipmentService.Instance.GetEquipmentAndUpdateSource(source, amount, deleteIfEmpty);
        }
    }
}