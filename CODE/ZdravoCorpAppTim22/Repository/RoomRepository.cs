using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    
   public class RoomRepository
   {
        List<Room> rooms = new List<Room> 
        { 
            new Room(1, 5, RoomType.operation, "500a"),
            new Room(2, 4, RoomType.resting, "100c"),
            new Room(3, 3, RoomType.reception, "400a"),
            new Room(4, 1, RoomType.waiting, "300b"),
            new Room(5, 6, RoomType.operation, "200a"),
        };

        public List<Room> GetAll()
        {
            return this.rooms;
        }
      
      public Room GetByID(int id)
      {
            int index = rooms.FindIndex(r => r.id == id);
            return rooms[index];
        }
      
      public void DeleteByID(int id)
      {
            int index = rooms.FindIndex(r => r.id == id);
            rooms.RemoveAt(index);
      }
      
      public void Create(Room roomObj)
      {
         this.rooms.Add(roomObj);
      }
      
      public void Update(Room roomObj)
      {
            int index = rooms.FindIndex(r => r.id == roomObj.id);
            rooms[index] = roomObj;
        }
      
      public string path;
   
   }
}