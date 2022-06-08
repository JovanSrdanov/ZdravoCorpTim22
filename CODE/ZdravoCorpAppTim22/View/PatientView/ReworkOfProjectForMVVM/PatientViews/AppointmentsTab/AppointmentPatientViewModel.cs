using Model;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
    public class AppointmentPatientViewModel : ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public AppointmentPatientViewModel(int id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }

        public AppointmentPatientViewModel(Patient patient)
        {
            Id = patient.Id;
            Name = patient.Name;
            Surname = patient.Surname;

        }
    }
}