using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class RoomController
   {
      public List<Room> GetAllRooms()
      {
            return roomService.GetAllRooms();
      }
      
      public Room GetRoomByID(int id)
      {
            return roomService.GetRoomByID(id);
      }
      
      public void DeleteRoomByID(int id)
      {
            roomService.DeleteRoomByID(id);
      }
      
      public void CreateRoom(Room roomObj)
      {
         roomService.CreateRoom(roomObj);
      }
      
      public void UpdateRoom(Room roomObj)
      {
            roomService.UpdateRoom(roomObj);
      }

        public Service.RoomService roomService = new Service.RoomService();
   
   }
}