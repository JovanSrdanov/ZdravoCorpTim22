using Model;
using System.Collections.Generic;
using System.Linq;
using ZdravoCorpAppTim22.Repository.Generic;

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

        public override void DeleteByID(int id)
        {
            Room room = List.Where(r => r.Id == id).FirstOrDefault();
            if (room == null)
            {
                return;
            }
            if (room.medicalAppointment != null)
            {
                List<MedicalAppointment> l = room.medicalAppointment;
                foreach (MedicalAppointment m in l)
                {
                    MedicalAppointmentRepository.Instance.DeleteByID(m.Id);
                }
            }
            List.Remove(room);
            FileHandler.SaveData(new List<Room>(List));
        }
    }
}