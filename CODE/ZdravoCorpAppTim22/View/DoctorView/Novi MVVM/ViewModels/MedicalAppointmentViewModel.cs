using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class MedicalAppointmentViewModel : ViewModel
    {
        public int Id { get; set; }
        public Interval Interval {get;set;}
        public Room Room { get; set; }
        public AppointmentType Type { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public MedicalAppointment MedicalAppointment { get; set; }

        public MedicalAppointmentViewModel(MedicalAppointment medicalAppointment)
        {
            this.Id = medicalAppointment.Id;
            this.Interval = medicalAppointment.Interval;
            this.Room = medicalAppointment.Room;
            this.Type = medicalAppointment.Type;
            this.Patient = medicalAppointment.Patient;
            this.Doctor = medicalAppointment.Doctor;
            this.MedicalAppointment = medicalAppointment;
        }
    }
}
