using Model;
using System.Collections.Generic;
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
            lock (_lock)
            {
                int index = List.FindIndex(r => r.Id == id);
                if (index == -1) return;
                Room room = List[index];
                if (room.medicalAppointment != null)
                {
                    List<MedicalAppointment> l = room.medicalAppointment;
                    foreach (MedicalAppointment m in l)
                    {
                        MedicalAppointmentRepository.Instance.DeleteByID(m.Id);
                    }
                }
                List.RemoveAt(index);
                FileHandler.SaveData(List);
                OnDataChangedEvent();
            }
        }
    }
}