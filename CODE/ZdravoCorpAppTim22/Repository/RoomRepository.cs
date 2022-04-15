using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ZdravoCorpAppTim22.Repository.Generic;
using ZdravoCorpAppTim22.Repository.FileHandlers;

namespace Repository
{
    public class RoomRepository : GenericRepository<Room>
    {
        public static string FileName = "RoomData.json";
        private static RoomRepository instance;
        private RoomRepository() : base(FileName) { }
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

        public new void DeleteByID(int id)
        {
            int index = List.FindIndex(r => r.Id == id);
            if (index == -1) return;
            Room room = List[index];
            if (room.medicalAppointment != null)
            {
                List<MedicalAppointment> l = room.medicalAppointment;
                foreach (MedicalAppointment m in l)
                {
                    MedicalAppointmentController.Instance.DeleteByID(m.Id);
                }
            }
            List.RemoveAt(index);
            FileHandler.SaveData(List);
        }
    }
}