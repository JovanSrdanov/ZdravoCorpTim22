using Model;
using System;
using System.Collections.Generic;

namespace Controller
{
   public class RoomController
   {
        private static RoomController instance;

        private RoomController() { }
        public static RoomController Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomController();
                }
                return instance;
            }
        }

        public Service.RoomService roomService = new Service.RoomService();
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
   }
}