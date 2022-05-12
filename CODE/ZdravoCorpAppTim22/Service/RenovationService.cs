using Model;
using Service;
using System;
using System.Collections.Generic;
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
        public void DeleteMany(List<Renovation> list)
        {
            foreach(Renovation renovation in list)
            {
                DeleteByID(renovation.Id);
            }
        }
        
        //Method that's being run every second from background thread
        public void DaemonMethod()
        {
            DoExpiredRenovations();
        }

        private void DoExpiredRenovations()
        {
            List<Renovation> list = GetAllExpired();
            if (App.Current != null)
            {
                App.Current.Dispatcher.Invoke(delegate
                {
                    foreach (Renovation item in list)
                    {
                        if (item.NewRoom != null)
                        {
                            Room oldRoom = RoomService.Instance.GetByID(item.NewRoom.Id);
                            oldRoom.Name = item.NewRoom.Name;
                            oldRoom.Level = item.NewRoom.Level;
                            oldRoom.Type = item.NewRoom.Type;
                            RoomService.Instance.Update(oldRoom);
                            oldRoom.RemoveRenovation(item);
                        }
                        Instance.DeleteByID(item.Id);
                    }
                });
            }
        }
        private List<Renovation> GetAllExpired()
        {
            List<Renovation> list = new List<Renovation>();
            lock (RenovationRepository.Instance._lock)
            {
                foreach (Renovation item in GetAll())
                {
                    if (item.Interval.End <= DateTime.Now)
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }
    }
}