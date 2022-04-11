using Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ZdravoCorpAppTim22.Repository.DataHandlers.Serialization;

namespace Repository
{
    public class RoomRepository
    {
        public string FileName = "RoomData.json";

        List<Room> Rooms = new List<Room>();
        Serializer<List<Room>> Serializer;

        public RoomRepository()
        {
            Serializer = new Serializer<List<Room>>(FileName);
            Rooms = Serializer.Deserialize();
        }

        public List<Room> GetAll()
        {
            return this.Rooms;
        }
      
        public Room GetByID(int id)
        {
            int index = Rooms.FindIndex(r => r.id == id);
            return Rooms[index];
        }
      
        public void DeleteByID(int id)
        {
            int index = Rooms.FindIndex(r => r.id == id);
            Rooms.RemoveAt(index);
            Serializer.Serialize(Rooms);
        }
      
        public void Create(Room roomObj)
        {
            this.Rooms.Add(roomObj);
            Serializer.Serialize(Rooms);
        }

        public void Update(Room roomObj)
        {
            int index = Rooms.FindIndex(r => r.id == roomObj.id);
            Rooms[index] = roomObj;
            Serializer.Serialize(Rooms);
        }
    }
}