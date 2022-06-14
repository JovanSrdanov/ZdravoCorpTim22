using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class AppointmentTypeViewModel : ViewModel
    {
        public AppointmentType AppointmentType { get; set; }

        public AppointmentTypeViewModel(AppointmentType appointmentType)
        {
            AppointmentType = appointmentType;
        }
    }
}
