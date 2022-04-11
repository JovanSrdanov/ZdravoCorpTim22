using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.Controller
{
    public class EquipmentController
    {
        private static EquipmentController instance;
        private EquipmentController() { }
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
        public List<Equipment> GetAllEquipment()
        {
            return this.equipmentService.GetAllEquipment();
        }

        public Equipment GetEquipmentByID(int id)
        {
            return this.equipmentService.GetEquipmentByID(id);
        }

        public void DeleteEquipmentByID(int id)
        {
            this.equipmentService.DeleteEquipmentByID(id);
        }

        public void CreateEquipment(Equipment equipment)
        {
            this.equipmentService.CreateEquipment(equipment);
        }

        public void UpdateEquipment(Equipment equipment)
        {
            this.equipmentService.UpdateEquipment(equipment);
        }

        public Service.EquipmentService equipmentService = new Service.EquipmentService();

    }
}