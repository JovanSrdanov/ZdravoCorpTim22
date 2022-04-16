using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller.Generic;

namespace Controller
{
   public class RoomController : GenericController<RoomService, Room>
    {
        private static RoomController instance;
        private RoomController() : base(RoomService.Instance) { }
        public static RoomController Instance 
        {
            get
            {
                if (instance == null)
                {
                    instance = new RoomController();
                }
                return instance;
            }
        }
   }
}