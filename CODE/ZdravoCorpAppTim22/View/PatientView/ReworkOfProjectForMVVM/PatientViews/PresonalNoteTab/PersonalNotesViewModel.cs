using System;
using System.Collections.Generic;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.PresonalNoteTab
{
    public class PersonalNotesViewModel :ViewModel
    {
        public int Id { get; set; }
        public int Frequency { get; set; }
        public string Message { get; set; }
        public string Reason { get; set; }
        public DateTime EndOfShowing { get; set; }
        public List<DateTime> NextNotification { get; set; }


        public PersonalNotesViewModel(PersonalNote personalNote)
        {
            Id = personalNote.Id;
            Frequency = personalNote.Frequency;
            Message = personalNote.Message;
            Reason = personalNote.Reason;
            EndOfShowing = personalNote.EndOfShowing;
            NextNotification = personalNote.NextNotification;


        }


    }
}