using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Repository.DataHandlers.Serialization;

namespace ZdravoCorpAppTim22.Repository.DataHandlers
{
    public class RoomDataHandler
    {
        private Serializer<List<Room>> serializer;

        public RoomDataHandler(string FileName)
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
