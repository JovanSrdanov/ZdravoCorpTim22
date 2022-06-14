using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.DoctorView.Projekat_u_MVVM_u.ViewModels
{
    public class MedicalAppointmentViewModel : ViewModel
    {
        private readonly MedicalAppointment _medicalAppointment;

        public Interval Interval => _medicalAppointment.Interval;
        public Room Room => _medicalAppointment.Room;
        public AppointmentType Type => _medicalAppointment.Type;
        public Patient Patient => _medicalAppointment.Patient;

        public MedicalAppointmentViewModel(MedicalAppointment medicalAppointment)
        {
            _medicalAppointment = medicalAppointment;
        }
    }
}
