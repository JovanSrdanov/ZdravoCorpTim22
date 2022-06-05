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
            DoctorList = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
        }
    }
}
