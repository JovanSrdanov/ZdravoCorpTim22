using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class DrugsViewModel : ViewModel
    {
        public int Id { get; set; }
        public string DrugName { get; set; }
        public int Amount { get; set; }

        private bool isApproved;
        public bool IsApproved
        {
            get { return isApproved; }
            set
            {
                isApproved = value;
                OnPropertyChanged(nameof(IsApproved));
            }
        }
        private string doctorName;
        public string DoctorName
        {
            get { return doctorName; }
            set
            {
                doctorName = value;
                OnPropertyChanged(nameof(DoctorName));
            }
        }

        public Medicine Medicine { get; set; }
        public MedicineData MedicineData { get; set; }

        public DrugsViewModel(Medicine medicine)
        {
            this.Id = medicine.Id;
            this.DrugName = medicine.MedicineData.Name;
            this.Amount = medicine.Amount;
            this.IsApproved = medicine.MedicineData.Approval.IsApproved;
            if (medicine.MedicineData.Approval.Doctor == null)
            {
                this.DoctorName = "";
            }
            else
            {
                this.DoctorName = medicine.MedicineData.Approval.Doctor.Name;
            }
            this.Medicine = medicine;
            this.MedicineData = medicine.MedicineData;
        }
    }
}
