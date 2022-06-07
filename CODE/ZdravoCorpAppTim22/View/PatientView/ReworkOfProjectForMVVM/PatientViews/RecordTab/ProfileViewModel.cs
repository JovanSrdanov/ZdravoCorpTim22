using Model;
using System;
using System.Collections.ObjectModel;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab
{
    public class ProfileViewModel : ViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Jmbg { get; set; }
        public DateTime Birthday { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public string Adress { get; set; }

        public ObservableCollection<MedicalReportsViewModel> MedicalReportsViewModels { get; set; }

        public ProfileViewModel(Patient patient)
        {
            Id = patient.Id;
            Name = patient.Name;
            Surname = patient.Surname;
            Email = patient.Email;
            Jmbg = patient.Jmbg;
            Birthday = patient.Birthday;
            Phone = patient.Phone;
            Gender = patient.Gender;
            Adress = patient.address.ToString();

            MedicalReportsViewModels = new ObservableCollection<MedicalReportsViewModel>();
            if (patient.medicalRecord.medicalReport == null) return;
            foreach (MedicalReport medicalReport in patient.medicalRecord.medicalReport)
            {
                MedicalReportsViewModels.Add(new MedicalReportsViewModel(medicalReport));

            }


        }
    }
}