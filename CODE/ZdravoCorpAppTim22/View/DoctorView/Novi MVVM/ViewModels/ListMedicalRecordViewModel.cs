using Controller;
using Model;
using MVVM1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.Views;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class ListMedicalRecordViewModel : ViewModel
    {
        //POLJA
        private readonly NavigationService navigationService;

        //PROPERTY
        public ObservableCollection<MedicalRecordViewModel> LoggedInDoctorMedicalRecords { get; set; }

        private MedicalRecordViewModel selectedMedicalRecord;
        public MedicalRecordViewModel SelectedMedicalRecord
        {
            get { return selectedMedicalRecord; }
            set
            {
                selectedMedicalRecord = value;
                if (value != null)
                {
                    SelectedMedicalRecordBackup = value;
                }
                OpenCommand.RaiseCanExecuteChanged();
            }
        }

        public MedicalRecordViewModel SelectedMedicalRecordBackup { get; set; }

        //KONSTRUKTOR
        public ListMedicalRecordViewModel(System.Windows.Navigation.NavigationService navigationService)
        {
            this.navigationService = navigationService;
            LoggedInDoctorMedicalRecords = new ObservableCollection<MedicalRecordViewModel>(GetMedicalRecords());
            setPatientInMedicalRecord(LoggedInDoctorMedicalRecords);

            OpenCommand = new MyICommand(ExecuteOpenCommand, CanExecuteOpenCommand);
            BackCommand = new MyICommand(ExecuteBackCommand);
        }

        private List<MedicalRecordViewModel> GetMedicalRecords()
        {
            List<MedicalRecord> doctorMedRec = (AuthenticationController.Instance.GetLoggedUser() as Doctor).MedicalRecord;
            List<MedicalRecordViewModel> tempList = new List<MedicalRecordViewModel>();

            foreach (var item in doctorMedRec)
            {
                tempList.Add(new MedicalRecordViewModel(item));
            }

            return tempList;
        }

        private void setPatientInMedicalRecord(ObservableCollection<MedicalRecordViewModel> loggedInDoctorMedicalRecords)
        {
            foreach (MedicalRecordViewModel medRec in loggedInDoctorMedicalRecords)
            {
                MedicalRecord medRecTemp = MedicalRecordController.Instance.GetByID(medRec.Id);
                medRec.PatientName = medRecTemp.Patient.Name;
                medRec.PatientSurname = medRecTemp.Patient.Surname;
                medRec.Jmbg = medRecTemp.Patient.Jmbg;
            }
        }

        //KOMANDE
        public MyICommand OpenCommand { get; set; }
        public MyICommand BackCommand { get; set; }
        
        private void ExecuteOpenCommand()
        {
            Patient selectedPatient = MedicalRecordController.Instance.GetByID(SelectedMedicalRecord.Id).Patient;
            RecordViewModel viewModel = new RecordViewModel(-1, selectedPatient, navigationService);
            RecordPage recordPage = new RecordPage(viewModel);
            this.navigationService.Navigate(recordPage);
        }

        private bool CanExecuteOpenCommand()
        {
            return SelectedMedicalRecord != null;
        }
        private void ExecuteBackCommand()
        {
            this.navigationService.GoBack();
        }
    }
}
