using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller.Generic;

namespace Controller
{
    public class MedicalAppointmentController : GenericController<MedicalAppointmentService, MedicalAppointment>
    {
        private static MedicalAppointmentController instance;
        private MedicalAppointmentController() : base(MedicalAppointmentService.Instance) { }
        public static MedicalAppointmentController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalAppointmentController();
                }

                return instance;
            }
        }
    }
}