using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class PatientHistoryDataViewModel : ViewModel
    {
        public List<MedicalReport> MedicalReport { get; set; }
        public List<string> ConditionList { get; set; }
        public List<string> AllergiesList { get; set; }
        public PatientHistoryDataViewModel(Patient patient)
        {
            this.MedicalReport = patient.MedicalRecord.MedicalReport;
            this.ConditionList = patient.MedicalRecord.ConditionList;
            this.AllergiesList = patient.MedicalRecord.AllergiesList;
        }
    }
}
