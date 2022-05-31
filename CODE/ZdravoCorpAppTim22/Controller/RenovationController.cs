using Controller;
using Model;
using System;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Service;

namespace ZdravoCorpAppTim22.Controller
{
    public class RenovationController : GenericController<RenovationService, Renovation>
    {
        private static RenovationController instance;
        private RenovationController() : base(RenovationService.Instance) { }
        public static RenovationController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenovationController();
                }
                return instance;
            }
        }

        public void Create(Room oldRoom, string name, int level, RoomType type, double surface, Interval interval)
        {
            if (interval.End <= DateTime.Now)
            {
                oldRoom.Name = name;
                oldRoom.Level = level;
                oldRoom.Type = type;
                oldRoom.Surface = surface;
                RoomController.Instance.Update(oldRoom);
            }
            else
            {
                Room newRoom = new Room(-1, level, type, name, surface);
                Renovation renovation = new Renovation(0, oldRoom, newRoom, interval);
                Create(renovation);
            }
        }
        public void BackgroundTask()
        {
            RenovationService.Instance.BackgroundTask();
        }
    }
}