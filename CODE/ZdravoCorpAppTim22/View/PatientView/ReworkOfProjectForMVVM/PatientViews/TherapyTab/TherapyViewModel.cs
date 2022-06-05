using System;
using System.Collections.ObjectModel;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.TherapyTab
{
    public class TherapyViewModel : ViewModel
    {
        public int Id { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime NotifyNextDateTime { get; set; }
        public string Time { get; set; }
        public string AdditionalInstructions { get; set; }
        public string TherapyPurpose { get; set; }

        public ObservableCollection<string> MedicineName { get; set; }

        public TherapyViewModel(int id, DateTime endDate, DateTime notifyNextDateTime, string time, string additionalInstructions, string therapyPurpose)
        {
            Id = id;
            EndDate = endDate;
            NotifyNextDateTime = notifyNextDateTime;
            Time = time;
            AdditionalInstructions = additionalInstructions;
            TherapyPurpose = therapyPurpose;
            MedicineName = new ObservableCollection<string>();
        }
    }
}