using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository;

namespace ZdravoCorpAppTim22.Service
{
    public class EquipmentService
    {
        private static EquipmentService instance;

        private EquipmentService()
        {

        }

        public static EquipmentService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentService();
                }

                return instance;
            }
        }

        public void Load()
        {
            EquipmentRepository.Instance.Load();
        }

        public List<Equipment> GetAllEquipment()
        {
            return EquipmentRepository.Instance.GetAll();
        }

        public Equipment GetEquipmentByID(int id)
        {
            return EquipmentRepository.Instance.GetByID(id);
        }

        public void DeleteEquipmentByID(int id)
        {
            EquipmentRepository.Instance.DeleteByID(id);
        }

        public void CreateEquipment(Equipment equipment)
        {
            EquipmentRepository.Instance.Create(equipment);
        }

        public void UpdateEquipment(Equipment equipment)
        {
            EquipmentRepository.Instance.Update(equipment);
        }

    }
}