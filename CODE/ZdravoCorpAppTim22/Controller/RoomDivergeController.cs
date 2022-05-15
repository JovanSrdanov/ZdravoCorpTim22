using Model;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class RoomDivergeController : GenericController<RoomDivergeService, RoomDiverge>
    {
        private static RoomDivergeController instance;
        private RoomDivergeController() : base(RoomDivergeService.Instance) { }
        public static RoomDivergeController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomDivergeController();
                }
                return instance;
            }
        }
        
        public void BackgroundTask()
        {
            RoomDivergeService.Instance.BackgroundTask();
        }
        public void DivergeInstant(Room room_1, Room room_2, Room sourceRoom, List<Equipment> equipment_1, List<Equipment> equipment_2)
        {
            RoomDivergeService.Instance.DivergeInstant(room_1, room_2, sourceRoom, equipment_1, equipment_2);
        }
        public void Create(Room room_1, Room room_2, Room sourceRoom, List<Equipment> equipment_1, List<Equipment> equipment_2, Interval interval)
        {
            RoomDivergeService.Instance.Create(room_1, room_2 , sourceRoom, equipment_1, equipment_2, interval);
        }
    }
}
