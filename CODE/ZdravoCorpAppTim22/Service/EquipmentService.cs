using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.Service
{
    public class EquipmentService
    {
        public List<Equipment> GetAllEquipment()
        {
            return this.equipmentRepository.GetAll();
        }

        public Equipment GetEquipmentByID(int id)
        {
            return this.equipmentRepository.GetByID(id);
        }

        public void DeleteEquipmentByID(int id)
        {
            this.equipmentRepository.DeleteByID(id);
        }

        public void CreateEquipment(Equipment equipment)
        {
            this.equipmentRepository.Create(equipment);
        }

        public void UpdateEquipment(Equipment equipment)
        {
            this.equipmentRepository.Update(equipment);
        }

        public Repository.EquipmentRepository equipmentRepository = new Repository.EquipmentRepository();

    }
}