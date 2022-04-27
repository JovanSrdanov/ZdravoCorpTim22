using Model;
using Repository;
using System;
using ZdravoCorpAppTim22.Service.Generic;

namespace Service
{
   public class RoomService : GenericService<RoomRepository, Room>
    {
        private static RoomService instance;
        private RoomService() : base(RoomRepository.Instance) { }
        public static RoomService Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomService();
                }
                return instance;
            }
        }
        public event EventHandler DataChangedEvent
        {
            add { RoomRepository.Instance.DataChangedEvent += value; }
            remove { RoomRepository.Instance.DataChangedEvent -= value; }
        }
    }
}