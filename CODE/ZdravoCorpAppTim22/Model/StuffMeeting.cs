using Model;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.Model
{
    public class StuffMeeting : IHasID, IHasInterval
    {
        public int Id { get; set; }
        public Interval Interval { get; set; }
        public Room Room { get => room; set => room = value; }

        private Room room;
        public List<SecretaryClass> Secretaries;
        public List<Doctor> Doctors;
        public List<ManagerClass> Managers;

        public StuffMeeting(int id, Interval interval, Room room, List<SecretaryClass> secretaries, List<Doctor> doctors, List<ManagerClass> managers)
        {
            this.Id = id;
            this.Interval = interval;
            this.Secretaries = secretaries;
            this.Doctors = doctors;
            this.Managers = managers;
            this.Room = room;
        }
    }
}
