using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class MedicalRecordViewModel : ViewModel
    {
        public int Id { get; set; }
        public string PatientName { get; set; }
        public string PatientSurname { get; set; }
        public string Jmbg { get; set; }
        public Patient Patient { get; set; }
        public MedicalRecordViewModel(MedicalRecord medicalRecord)
        {
            this.Id = medicalRecord.Id;
            if (medicalRecord.Patient == null)
            {
                this.PatientName = "";
                this.PatientSurname = "";
                this.Jmbg = "";
            }
            else
            {
                this.PatientName = medicalRecord.Patient.Name;
                this.PatientSurname = medicalRecord.Patient.Surname;
                this.Jmbg = medicalRecord.Patient.Jmbg;
            }
            this.Patient = medicalRecord.Patient;
        }
    }
}
