using Model;
using System;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class RoomMergeController : GenericController<RoomMergeService, RoomMerge>
    {
        private static RoomMergeController instance;
        private RoomMergeController() : base(RoomMergeService.Instance) { }
        public static RoomMergeController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomMergeController();
                }
                return instance;
            }
        }
        public void BackgroundTask()
        {
            RoomMergeService.Instance.BackgroundTask();
        }

        public void MergeInstant(Room room_1, Room room_2, Room newRoom)
        {
            RoomMergeService.Instance.MergeInstant(room_1, room_2, newRoom);
        }

        public void Create(Room room_1, Room room_2, Room newRoom, Interval interval)
        {
            if(interval.End <= DateTime.Now)
            {
                MergeInstant(room_1, room_2, newRoom);
            }
            else
            {
                RoomMergeService.Instance.Create(room_1, room_2 , newRoom, interval);
            }
        }
            
    }
}
