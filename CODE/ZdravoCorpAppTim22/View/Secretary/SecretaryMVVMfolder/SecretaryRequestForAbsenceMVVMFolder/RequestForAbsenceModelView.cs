using Model;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder.SecretaryRequestForAbsenceMVVMFolder
{
    public class RequestForAbsenceModelView : ViewModel
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public bool Urgent { get; set; }
        public Interval AbsenceInterval { get; set; }
        public RequestState RequestState { get; set; }

        public Doctor doctor;
        public Doctor Doctor
        {
            get
            {
                return doctor;
            }
            set
            {
                if (this.doctor == null || !this.doctor.Equals(value))
                {
                    if (this.doctor != null)
                    {
                        Doctor oldDoctor = this.doctor;
                        this.doctor = null;
                    }
                    if (value != null)
                    {
                        this.doctor = value;
                    }
                }
            }
        }
        public RequestForAbsenceModelView(RequestForAbsence requestForAbsence)
        {
            this.Id = requestForAbsence.Id;
            this.Reason = requestForAbsence.Reason;
            this.Urgent = requestForAbsence.Urgent;
            this.AbsenceInterval = requestForAbsence.AbsenceInterval;
            this.RequestState = requestForAbsence.RequestState;
            this.Doctor = requestForAbsence.Doctor;
        }
    }
}
