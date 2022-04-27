using Model;
using Service;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Repository;
using ZdravoCorpAppTim22.Service.Generic;

namespace ZdravoCorpAppTim22.Service
{
    public class RenovationService : GenericService<RenovationRepository, Renovation>
    {
        private static RenovationService instance;
        private RenovationService() : base(RenovationRepository.Instance) { }
        public static RenovationService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RenovationService();
                }

                return instance;
            }
        }

        public void DaemonMethod()
        {
            List<Renovation> list = new List<Renovation>();
            foreach (Renovation item in Instance.GetAll())
            {
                if (item.Interval.End <= DateTime.Now)
                {
                    list.Add(item);
                }
            }
            if(list.Count > 0)
            {
                Debug.WriteLine("PROMIJENJENO: " + list.Count);
            }
            foreach(Renovation item in list)
            {
                Room oldRoom = RoomService.Instance.GetByID(item.NewRoom.Id);
                oldRoom.Name = item.NewRoom.Name;
                oldRoom.Level = item.NewRoom.Level;
                oldRoom.Type = item.NewRoom.Type;
                RoomService.Instance.Update(oldRoom);
                oldRoom.RemoveRenovation(item);
                Instance.DeleteByID(item.Id);
            }
        }
    }
}