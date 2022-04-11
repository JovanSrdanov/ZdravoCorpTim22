using Model;
using System;
using System.Collections.Generic;

namespace Service
{
   public class RoomService
   {
      public List<Room> GetAllRooms()
      {
            return roomRepository.GetAll();
      }
      
      public Room GetRoomByID(int id)
      {
            return roomRepository.GetByID(id);
      }
      
      public void DeleteRoomByID(int id)
      {
            roomRepository.DeleteByID(id);
        }
      
      public void CreateRoom(Room roomObj)
      {
         roomRepository.Create(roomObj);
      }
      
      public void UpdateRoom(Room roomObj)
      {
            roomRepository.Update(roomObj);
        }
      
      public Repository.RoomRepository roomRepository = new Repository.RoomRepository();
   
   }
}