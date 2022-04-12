using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public class DoctorViewModel
    {
        public ObservableCollection<Doctor> DoctorList { get; set; }

        public DoctorViewModel()
        {
            List<Doctor> doctorRep = DoctorController.Instance.GetAll();
            DoctorList = new ObservableCollection<Doctor>(doctorRep);
        }
    }
}
