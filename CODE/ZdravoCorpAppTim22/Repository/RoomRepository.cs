using Model;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.DataHandlers;

namespace Repository
{
    public class RoomRepository
    {
        public string FileName = "RoomData.json";

        List<Room> Rooms = new List<Room>();
        RoomDataHandler roomDataHandler;

        public RoomRepository()
        {
            roomDataHandler = new RoomDataHandler(FileName);
            Rooms = roomDataHandler.LoadData();
        }

        public List<Room> GetAll()
        {
            return this.Rooms;
        }
      
        public Room GetByID(int id)
        {
            int index = Rooms.FindIndex(r => r.id == id);
            if (index == -1)
            {
                return null;
            }
            return Rooms[index];
        }
      
        public void DeleteByID(int id)
        {
            int index = Rooms.FindIndex(r => r.id == id);
            Rooms.RemoveAt(index);
            roomDataHandler.SaveData(Rooms);
        }
      
        public void Create(Room roomObj)
        {
            this.Rooms.Add(roomObj);
            roomDataHandler.SaveData(Rooms);
        }

        public void Update(Room roomObj)
        {
            int index = Rooms.FindIndex(r => r.id == roomObj.id);
            Rooms[index] = roomObj;
            roomDataHandler.SaveData(Rooms);
        }
    }
}