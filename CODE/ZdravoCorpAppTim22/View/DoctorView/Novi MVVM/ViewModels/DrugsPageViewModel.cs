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
    public class DrugsPageViewModel : ViewModel
    {
        //POLJA
        private readonly NavigationService navigationService;

        //PROPERTY
        public int SelectedIndex { get; set; }
        public static ObservableCollection<DrugsViewModel> AllMedicineInStorageObservable { get; set; }
        private DrugsViewModel selectedDrugsViewModel;
        public DrugsViewModel SelectedDrugsViewModel
        {
            get { return selectedDrugsViewModel; }
            set
            {
                selectedDrugsViewModel = value;
                if (value != null)
                {
                    SelectedDrugsViewModelBackup = value;
                }
                ApproveCommand.RaiseCanExecuteChanged();
                RejectCommand.RaiseCanExecuteChanged();
            }
        }
        public DrugsViewModel SelectedDrugsViewModelBackup { get; set; }

        //KONSTRUKTOR
        public DrugsPageViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            AllMedicineInStorageObservable = new ObservableCollection<DrugsViewModel>(GetMedicine());

            ApproveCommand = new MyICommand(ExecuteApproveCommand, CanExecuteApproveRejectCommand);
            RejectCommand = new MyICommand(ExecuteRejectCommand, CanExecuteApproveRejectCommand);
            BackCommand = new MyICommand(ExecuteBackCommand);
        }

        private List<DrugsViewModel> GetMedicine()
        {
            List<Medicine> allMedicineInStorage = MedicineController.Instance.GetAllFree();
            List<DrugsViewModel> tempList = new List<DrugsViewModel>();

            foreach (Medicine item in allMedicineInStorage)
            {
                if (ApprovalController.Instance.isRejected(item))
                    continue;
                else
                    tempList.Add(new DrugsViewModel(item));
            }
            return tempList;
        }

        private void updateMedicineApproval(Medicine medicine)
        {
            medicine.MedicineData.Approval.IsApproved = true;
            medicine.MedicineData.Approval.Doctor = (Doctor)AuthenticationController.Instance.GetLoggedUser();
            medicine.MedicineData.Approval.Message = "";
            SelectedDrugsViewModel.IsApproved = true;
            SelectedDrugsViewModel.DoctorName = (AuthenticationController.Instance.GetLoggedUser() as Doctor).Name;
        }

        //KOMANDE
        public MyICommand ApproveCommand { get; set; }
        public MyICommand RejectCommand { get; set; }
        public MyICommand BackCommand { get; set; }

        public void ExecuteApproveCommand()
        {
            Medicine medicine = MedicineController.Instance.GetByID(SelectedDrugsViewModel.Id);
            updateMedicineApproval(medicine);

            ApprovalController.Instance.Update(medicine.MedicineData.Approval);
            MedicineDataController.Instance.Update(medicine.MedicineData);
            MedicineController.Instance.Update(medicine);

            AllMedicineInStorageObservable[SelectedIndex] = SelectedDrugsViewModel;
        }
        private void ExecuteRejectCommand()
        {
            RejectDrugViewModel rejectDrugViewModel = new RejectDrugViewModel(navigationService, SelectedDrugsViewModel);
            RejectDrugRequestPage rejectDrugRequestPage = new RejectDrugRequestPage(rejectDrugViewModel);
            this.navigationService.Navigate(rejectDrugRequestPage);
        }

        private bool CanExecuteApproveRejectCommand()
        {
            if (SelectedDrugsViewModel == null)
            {
                return false;
            }
            else if (ApprovalController.Instance.isApproved(SelectedDrugsViewModel.MedicineData.Approval))
            {
                return false;
            }
            return true;
        }

        private void ExecuteBackCommand()
        {
            this.navigationService.GoBack();
        }
    }
}
