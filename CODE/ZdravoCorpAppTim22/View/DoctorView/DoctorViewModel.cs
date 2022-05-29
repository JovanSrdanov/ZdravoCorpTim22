using Controller;
using Model;
using System.Collections.ObjectModel;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public class DoctorViewModel
    {
        public ObservableCollection<Doctor> DoctorList { get; set; }

        public DoctorViewModel()
        {
            /*List<Doctor> doctorRep = DoctorController.Instance.GetAll();
            DoctorList = new ObservableCollection<Doctor>(doctorRep);*/
            DoctorList = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
        }
    }
}
