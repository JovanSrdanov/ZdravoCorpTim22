using Model;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
    public class AppointmentRoomViewModel: ViewModel
    {

        public int Id { get; set; }
        public int Level { get; set; }
     
        public string Name { get; set; }

        public AppointmentRoomViewModel(int id, int level, string name)
        {
            Id = id;
            Level = level;
            Name = name;
        }
    }
}