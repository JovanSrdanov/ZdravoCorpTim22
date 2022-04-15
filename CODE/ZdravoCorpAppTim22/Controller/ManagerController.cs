using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller.Generic;

namespace Controller
{
    public class ManagerController : GenericController<ManagerService, ManagerClass>
    {
        private static ManagerController instance;
        private ManagerController() : base(ManagerService.Instance) { }
        public static ManagerController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ManagerController();
                }

                return instance;
            }
        }
    }
}