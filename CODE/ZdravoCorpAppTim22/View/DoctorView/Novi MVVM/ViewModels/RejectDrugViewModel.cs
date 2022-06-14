using Model;
using MVVM1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class RejectDrugViewModel : ViewModel
    {
        //POLJA
        private readonly NavigationService navigationService;

        //PROPERTY
        public DrugsViewModel SelectedDrugsViewModel { get; set; }
        public string RejectMessageReason { get; set; }
        Medicine SelectedMedicine { get; set; }

        //KONSTRUKTOR
        public RejectDrugViewModel(System.Windows.Navigation.NavigationService navigationService, 
            DrugsViewModel selectedDrugsViewModel)
        {
            this.navigationService = navigationService;
            SelectedDrugsViewModel = selectedDrugsViewModel;
            SelectedMedicine = MedicineController.Instance.GetByID(SelectedDrugsViewModel.Id);

            ConfirmCommand = new MyICommand(ExecuteConfirmCommand);
            CancelCommand = new MyICommand(ExecuteCancelCommand);
        }
        private void updateMedicineApproval()
        {
            SelectedDrugsViewModel.IsApproved = false;
            SelectedDrugsViewModel.DoctorName = (AuthenticationController.Instance.GetLoggedUser() as Doctor).Name;

            SelectedMedicine.MedicineData.Approval.IsApproved = false;
            SelectedMedicine.MedicineData.Approval.Doctor = (AuthenticationController.Instance.GetLoggedUser() as Doctor);
            SelectedMedicine.MedicineData.Approval.Message = RejectMessageReason;

        }

        //KOMANDE
        public MyICommand ConfirmCommand { get; set; }
        private void ExecuteConfirmCommand()
        {
            updateMedicineApproval();
            DrugsPageViewModel.AllMedicineInStorageObservable.Remove(SelectedDrugsViewModel);

            ApprovalController.Instance.Update(SelectedMedicine.MedicineData.Approval);
            MedicineDataController.Instance.Update(SelectedMedicine.MedicineData);
            MedicineController.Instance.Update(SelectedMedicine);

            this.navigationService.GoBack();
        }


        public MyICommand CancelCommand { get; set; }

        private void ExecuteCancelCommand()
        {
            MessageBoxResult result = MessageBox.Show("Close window without saving?", "Reject drug request",
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.navigationService.GoBack();
                    break;
                case MessageBoxResult.No:

                    break;
            } 
        }
    }
}
