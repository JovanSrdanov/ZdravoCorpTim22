using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class PatientViewModel : ViewModel
    {
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public Patient Patient { get; set; }

        public PatientViewModel(Patient patient)
        {
            PatientName = patient.Name;
            PatientSurname = patient.Surname;
            Patient = patient;
        }
    }
}
