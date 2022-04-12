using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public class AppointmentViewModel
    {
        public ObservableCollection<MedicalAppointment> MedicalAppointmentList { get; set; }

        public AppointmentViewModel()
        {
            List<MedicalAppointment> appointRep = new List<MedicalAppointment>()
            {
                /*MedicalAppointmentController.Instance.GetByID(123),
                MedicalAppointmentController.Instance.GetByID(1231),
                MedicalAppointmentController.Instance.GetByID(1232),

                MedicalAppointmentController.Instance.GetByID(124),
                MedicalAppointmentController.Instance.GetByID(1241),
                MedicalAppointmentController.Instance.GetByID(1242),

                MedicalAppointmentController.Instance.GetByID(125),
                MedicalAppointmentController.Instance.GetByID(1251),
                MedicalAppointmentController.Instance.GetByID(1252),*/
            };

            MedicalAppointmentList = new ObservableCollection<MedicalAppointment>(appointRep);
        }
    }
}
