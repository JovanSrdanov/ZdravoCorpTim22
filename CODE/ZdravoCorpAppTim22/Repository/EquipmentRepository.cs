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
        GenericFileHandler<Equipment> EquipmentFileHandler;

        List<Equipment> EquipmentList = new List<Equipment>();

        private static EquipmentRepository instance;

        private EquipmentRepository()
        {
            EquipmentFileHandler = new GenericFileHandler<Equipment>(FileName);
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

        public void Load()
        {
            EquipmentList = EquipmentFileHandler.LoadData();
        }

        public List<Equipment> GetAll()
        {
            return this.EquipmentList;
        }

        public Equipment GetByID(int id)
        {
            int index = EquipmentList.FindIndex(r => r.Id == id);
            if(index == -1)
            {
                return null;
            }
            return EquipmentList[index];
        }

        public void DeleteByID(int id)
        {
            int index = EquipmentList.FindIndex(r => r.Id == id);
            if(index == -1)
            {
                return;
            }
            EquipmentList.RemoveAt(index);
            EquipmentFileHandler.SaveData(EquipmentList);
        }

        public void Create(Equipment equipment)
        {
            if(equipment== null)
            {
                return;
            }
            if (EquipmentList.Count > 0)
            {
                equipment.Id = EquipmentList[EquipmentList.Count - 1].Id + 1;
            }
            else
            {
                equipment.Id = 0;
            }
            this.EquipmentList.Add(equipment);
            EquipmentFileHandler.SaveData(EquipmentList);
        }

        public void Update(Equipment equipment)
        {
            if(equipment == null)
            {
                return;
            }
            int index = EquipmentList.FindIndex(r => r.Id == equipment.Id);
            if(index == -1)
            {
                return;
            }
            EquipmentList[index] = equipment;
            EquipmentFileHandler.SaveData(EquipmentList);
        }
    }
}