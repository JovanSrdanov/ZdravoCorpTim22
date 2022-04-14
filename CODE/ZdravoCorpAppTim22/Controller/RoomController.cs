using Model;
using Service;
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

        public List<Room> GetAllRooms()
        {
            return RoomService.Instance.GetAllRooms();
        }
      
        public Room GetRoomByID(int id)
        {
            return RoomService.Instance.GetRoomByID(id);
        }
      
        public void DeleteRoomByID(int id)
        {
            RoomService.Instance.DeleteRoomByID(id);
        }
      
        public void CreateRoom(Room roomObj)
        {
            RoomService.Instance.CreateRoom(roomObj);
        }
      
        public void UpdateRoom(Room roomObj)
        {
            RoomService.Instance.UpdateRoom(roomObj);
        }
   }
}