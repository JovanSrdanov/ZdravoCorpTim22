using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class RequestViewModel : ViewModel
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public bool Urgent { get; set; }
        public Interval AbsenceInterval { get; set; }
        public RequestState RequestState { get; set; }

        public RequestViewModel(RequestForAbsence requestForAbsence)
        {
            this.Id = requestForAbsence.Id;
            this.Reason = requestForAbsence.Reason;
            this.Urgent = requestForAbsence.Urgent;
            this.AbsenceInterval = requestForAbsence.AbsenceInterval;
            this.RequestState = requestForAbsence.RequestState;
        }
    }
}
