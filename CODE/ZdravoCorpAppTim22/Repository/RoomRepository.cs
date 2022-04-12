using Controller;
using Model;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace Repository
{
    public class RoomRepository
    {
        public string FileName = "RoomData.json";

        List<Room> Rooms = new List<Room>();
        RoomFileHandler roomFileHandler;

        public RoomRepository()
        {
            roomFileHandler = new RoomFileHandler(FileName);
            Rooms = roomFileHandler.LoadData();
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
            if (index == -1) return;
            Room room = Rooms[index];
            if(room.medicalAppointment != null)
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
            this.Rooms.Add(roomObj);
            roomFileHandler.SaveData(Rooms);
        }

        public void Update(Room roomObj)
        {
            int index = Rooms.FindIndex(r => r.id == roomObj.id);
            Rooms[index] = roomObj;
            roomFileHandler.SaveData(Rooms);
        }
    }
}