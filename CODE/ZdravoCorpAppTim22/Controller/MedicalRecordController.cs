using Model;
using System;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Service;

namespace Controller
{
    public class MedicalRecordController : GenericController<MedicalRecordService, MedicalRecord>
    {
        private static MedicalRecordController instance;
        private MedicalRecordController() : base(MedicalRecordService.Instance) { }
        public static MedicalRecordController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MedicalRecordController();
                }
                return instance;
            }
        }
    }
}