namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
    public class AppointmentDoctorViewModel : ViewModel
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string DoctorSpecializationName { get; set; }


        public AppointmentDoctorViewModel(int id, string name, string surname, string doctorSpecializationName)
        {
            Id = id;
            Name = name;
            Surname = surname;
            DoctorSpecializationName = doctorSpecializationName;
        }
    }
}