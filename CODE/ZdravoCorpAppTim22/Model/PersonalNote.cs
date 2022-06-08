using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Model;
using ZdravoCorpAppTim22.Model.Generic;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.Repository.FileHandlers.Serialization;

namespace ZdravoCorpAppTim22.Model
{
    public class PersonalNote : IHasID
    {
        public int Id { get; set; }
        public int Frequency { get; set; }
        public string Message { get; set; }
        public string Reason { get; set; }
        public DateTime EndOfShowing { get; set; }
        public List<DateTime> NextNotification { get; set; }

        public PersonalNote(int id , Patient patient, int frequency, string message, string reason, DateTime endOfShowing)
        {
            Patient = patient;
            Id = id;
            Frequency = frequency;
            Message = message;
            Reason = reason;
            EndOfShowing = endOfShowing;
            NextNotification = new List<DateTime>();
        }

        [JsonConstructor]
        public PersonalNote()
        {

        }

        [JsonConverter(typeof(PatientToIDConverter))]
        private Patient patient;

        [JsonConverter(typeof(PatientToIDConverter))]
        public Patient Patient
        {
            get
            {
                return patient;
            }
            set
            {
                if (this.patient == null || !this.patient.Equals(value))
                {
                    if (this.patient != null)
                    {
                        Patient oldPatient = this.patient;
                        this.patient = null;
                        oldPatient.RemovePersonalNote(this);
                    }
                    if (value != null)
                    {
                        this.patient = value;
                        this.patient.AddPersonalNote(this);
                    }
                }
            }
        }



    }
}
