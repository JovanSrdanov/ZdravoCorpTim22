using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.DataHandlers;

namespace ZdravoCorpAppTim22.Repository
{
    public class EquipmentRepository
    {
        public string FileName = "EquipmentData.json";
        EquipmentDataHandler equipmentDataHandler;

        List<Equipment> EquipmentList = new List<Equipment>();

        public EquipmentRepository()
        {
            equipmentDataHandler = new EquipmentDataHandler(FileName);
            EquipmentList = equipmentDataHandler.LoadData();
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
            equipmentDataHandler.SaveData(EquipmentList);
        }

        public void Create(Equipment equipment)
        {
            this.EquipmentList.Add(equipment);
            equipmentDataHandler.SaveData(EquipmentList);
        }

        public void Update(Equipment equipment)
        {
            int index = EquipmentList.FindIndex(r => r.id == equipment.id);
            EquipmentList[index] = equipment;
            equipmentDataHandler.SaveData(EquipmentList);
        }

    }
}