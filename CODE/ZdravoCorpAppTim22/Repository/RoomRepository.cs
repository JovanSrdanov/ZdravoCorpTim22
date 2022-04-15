using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace Repository
{
    public class RoomRepository
    {
        private static RoomRepository instance;

        public string FileName = "RoomData.json";

        List<Room> Rooms = new List<Room>();
        GenericFileHandler<Room> roomFileHandler;

        private RoomRepository()
        {
            roomFileHandler = new GenericFileHandler<Room>(FileName);
        }

        public static RoomRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomRepository();

                }

                return instance;
            }
        }

        public void Load()
        {
            Rooms = roomFileHandler.LoadData();
        }

        public List<Room> GetAll()
        {
            return this.Rooms;
        }
      
        public Room GetByID(int id)
        {
            int index = Rooms.FindIndex(r => r.Id == id);
            if (index == -1)
            {
                return null;
            }
            return Rooms[index];
        }
      
        public void DeleteByID(int id)
        {
            int index = Rooms.FindIndex(r => r.Id == id);
            if (index == -1) return;
            Room room = Rooms[index];
            if (room.medicalAppointment != null)
            {
                List<MedicalAppointment> l = room.medicalAppointment;
                foreach (MedicalAppointment m in l)
                {
                    MedicalAppointmentController.Instance.DeleteByID(m.Id);
                }
            }
            Rooms.RemoveAt(index);
            roomFileHandler.SaveData(Rooms);
        }
      
        public void Create(Room roomObj)
        {
            if (Rooms.Count > 0)
            {
                roomObj.Id = Rooms[Rooms.Count - 1].Id + 1;
            }
            else
            {
                roomObj.Id = 0;
            }
            this.Rooms.Add(roomObj);
            roomFileHandler.SaveData(Rooms);
        }

        public void Update(Room roomObj)
        {
            int index = Rooms.FindIndex(r => r.Id == roomObj.Id);
            Rooms[index] = roomObj;
            roomFileHandler.SaveData(Rooms);
        }
    }
}