using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace ZdravoCorpAppTim22.Repository
{
    public class EquipmentRepository
    {
        public string FileName = "EquipmentData.json";
        EquipmentFileHandler equipmentFileHandler;

        List<Equipment> EquipmentList = new List<Equipment>();

        private static EquipmentRepository instance;

        private EquipmentRepository()
        {
            equipmentFileHandler = new EquipmentFileHandler(FileName);
            EquipmentList = equipmentFileHandler.LoadData();
        }

        public static EquipmentRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EquipmentRepository();
                }

                return instance;
            }
        }

        public List<Equipment> GetAll()
        {
            return this.EquipmentList;
        }

        public Equipment GetByID(int id)
        {
            int index = EquipmentList.FindIndex(r => r.id == id);
            return EquipmentList[index];
        }

        public void DeleteByID(int id)
        {
            int index = EquipmentList.FindIndex(r => r.id == id);
            EquipmentList.RemoveAt(index);
            equipmentFileHandler.SaveData(EquipmentList);
        }

        public void Create(Equipment equipment)
        {
            this.EquipmentList.Add(equipment);
            equipmentFileHandler.SaveData(EquipmentList);
        }

        public void Update(Equipment equipment)
        {
            int index = EquipmentList.FindIndex(r => r.id == equipment.id);
            EquipmentList[index] = equipment;
            equipmentFileHandler.SaveData(EquipmentList);
        }

    }
}