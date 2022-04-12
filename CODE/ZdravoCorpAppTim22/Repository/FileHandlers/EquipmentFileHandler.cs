using Controller;
using Model;
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
            foreach (var item in equipment)
            {
                if(item.Room != null)
                {
                    item.RoomID = item.Room.id;
                }
                else
                {
                    item.RoomID = -1;
                }
            }
            serializer.Serialize(equipment);
        }

        public List<Equipment> LoadData()
        {
            RoomController roomController = RoomController.Instance;
            List<Equipment> equipment = serializer.Deserialize();
            if (equipment == null)
            {
                return new List<Equipment>();
            }
            foreach (var item in equipment)
            {
                if (item.RoomID != -1)
                {
                    Room room = roomController.GetRoomByID(item.RoomID);
                    if (room != null)
                    {
                        item.Room = room;
                        room.AddEquipment(item);
                    }
                }
            }
            return equipment;
        }
    }
}
