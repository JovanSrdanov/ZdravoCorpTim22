using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public class AppointmentViewModel
    {
        public ObservableCollection<MedicalAppointment> MedicalAppointmentList { get; set; }

        public AppointmentViewModel()
        {
            List<MedicalAppointment> appointRep = new List<MedicalAppointment>()
            {
            };

            MedicalAppointmentList = new ObservableCollection<MedicalAppointment>(appointRep);
        }
    }
}
