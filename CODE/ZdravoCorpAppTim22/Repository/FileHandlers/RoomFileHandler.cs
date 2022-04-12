using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Repository.FileHandlers
{
    public class RoomFileHandler
    {
        private Serializer<List<Room>> serializer;

        public RoomFileHandler(string FileName)
        {
            serializer = new Serializer<List<Room>>(FileName);
        }

        public void SaveData(List<Room> rooms) { 
            serializer.Serialize(rooms);
        }

        public List<Room> LoadData()
        {
            List<Room> rooms = serializer.Deserialize();
            return rooms == null ? new List<Room>() : rooms;
        }
    }
}
