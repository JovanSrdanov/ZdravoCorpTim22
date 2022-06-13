using Model;
using Service;
using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Controller.Generic;
using ZdravoCorpAppTim22.Model;

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

        public bool isDoctorRegular(Doctor doctor)
        {
            return DoctorService.Instance.isDoctorRegular(doctor);
        }

        public bool doctorHasMedicalRecord(Doctor doctor, Patient patient)
        {
            return DoctorService.Instance.doctorHasMedicalRecord(doctor, patient);
        }
    }
}