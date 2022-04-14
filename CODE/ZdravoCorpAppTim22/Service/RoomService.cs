using Model;
using Repository;
using System;
using System.Collections.Generic;

namespace Service
{
   public class RoomService
   {
        private static RoomService instance;

        private RoomService() { }
        public static RoomService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomService();
                }
                return instance;
            }
        }
        public List<Room> GetAllRooms()
      {
            return RoomRepository.Instance.GetAll();
      }
      
      public Room GetRoomByID(int id)
      {
            return RoomRepository.Instance.GetByID(id);
      }
      
      public void DeleteRoomByID(int id)
      {
            RoomRepository.Instance.DeleteByID(id);
        }
      
      public void CreateRoom(Room roomObj)
      {
            RoomRepository.Instance.Create(roomObj);
      }
      
      public void UpdateRoom(Room roomObj)
      {
            RoomRepository.Instance.Update(roomObj);
        }
   
   }
}