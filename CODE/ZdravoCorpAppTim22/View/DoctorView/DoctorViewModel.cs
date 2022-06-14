using Controller;
using Model;
using System.Collections.ObjectModel;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public class DoctorViewModel
    {
        public ObservableCollection<Doctor> DoctorList { get; set; }
        public string DoctorName { get; set; }
        public Doctor Doctor { get; set; }

        public DoctorViewModel(Doctor doctor)
        {
            DoctorList = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
            this.DoctorName = doctor.Name;
            this.Doctor = doctor;
        }
    }
}
