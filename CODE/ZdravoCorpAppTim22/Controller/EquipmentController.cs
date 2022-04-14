using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Service;

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
            return EquipmentService.Instance.GetAllEquipment();
        }

        public Equipment GetEquipmentByID(int id)
        {
            return EquipmentService.Instance.GetEquipmentByID(id);
        }

        public void DeleteEquipmentByID(int id)
        {
            EquipmentService.Instance.DeleteEquipmentByID(id);
        }

        public void CreateEquipment(Equipment equipment)
        {
            EquipmentService.Instance.CreateEquipment(equipment);
        }

        public void UpdateEquipment(Equipment equipment)
        {
            EquipmentService.Instance.UpdateEquipment(equipment);
        }

    }
}