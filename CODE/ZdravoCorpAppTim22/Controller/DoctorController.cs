using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller.Generic;

namespace Controller
{
    public class DoctorController : GenericController<DoctorService, Doctor>
    {
        private static DoctorController instance;
        private DoctorController() : base(DoctorService.Instance) { }
        public static DoctorController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DoctorController();
                }

                return instance;
            }
        }
    }
}