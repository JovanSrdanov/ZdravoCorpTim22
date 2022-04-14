using Controller;
using Model;
using Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers
{
    public class EquipmentFileHandler
    {
        private Serializer<List<Equipment>> serializer;
        public EquipmentFileHandler(string fileName)
        {
            serializer = new Serializer<List<Equipment>>(fileName);
        }

        public void SaveData(List<Equipment> equipment)
        {
            serializer.Serialize(equipment);
        }

        public List<Equipment> LoadData()
        {
            List<Equipment> equipment = serializer.Deserialize();
            return equipment == null ? new List<Equipment>() : equipment;
        }
    }
}
