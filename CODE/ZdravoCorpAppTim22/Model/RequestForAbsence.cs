using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class RequestForAbsence : IHasID
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public bool Urgent { get; set; }
        public Interval AbsenceInterval { get; set; }
        public RequestState RequestState { get; set; }

        //Constructor
        [JsonConstructor]
        public RequestForAbsence() { }
        public RequestForAbsence(string reason, bool urgent, Interval absenceInterval, Doctor doctor)
        {
            Reason = reason;
            Urgent = urgent;
            AbsenceInterval = absenceInterval;
            Doctor = doctor;
            RequestState = RequestState.pending;
        }

        //Doctor
        [JsonConverter(typeof(DoctorToIDConverter))]
        public Doctor doctor;

        [JsonConverter(typeof(DoctorToIDConverter))]
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
                        oldDoctor.RemoveRequestForAbsence(this);
                    }
                    if (value != null)
                    {
                        this.doctor = value;
                        this.doctor.AddRequestForAbsence(this);
                    }
                }
            }
        }
        ///DODAO////////////
        public override bool Equals(object obj)
        {
            return this.Doctor.Id == ((RequestForAbsence)obj).Doctor.Id;
        }
        public override int GetHashCode()
        {
            //Get the ID hash code value
            return this.Doctor.Id.GetHashCode(); ;
        }
    }
}
