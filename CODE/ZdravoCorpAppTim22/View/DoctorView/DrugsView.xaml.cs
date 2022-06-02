using Controller;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class DrugsView : Window
    {
        public static ObservableCollection<Medicine> allMedicineInStorageObservable;
        public DrugsView()
        {
            InitializeComponent();
            List<Medicine> allMedicineInStorage = MedicineController.Instance.GetAllFree();
            allMedicineInStorageObservable = new ObservableCollection<Medicine>(allMedicineInStorage);
            foreach (Medicine item in allMedicineInStorage)
            {
                if (item.MedicineData.Approval.Doctor != null && item.MedicineData.Approval.IsApproved == false)
                    allMedicineInStorageObservable.Remove(item);
            }
            MedicineDataGrid.ItemsSource = allMedicineInStorageObservable;
        }

        //selection changed event handlers
        private void MedicineDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)      //mozda?
        {
            Medicine selectedMedicine = MedicineDataGrid.SelectedItem as Medicine;
            if (selectedMedicine == null || selectedMedicine.MedicineData.Approval.IsApproved)
            {
                ApproveBtn.IsEnabled = false;
                RejectBtn.IsEnabled = false;
            }
            else
            {
                ApproveBtn.IsEnabled = true;
                RejectBtn.IsEnabled = true;
            }
        }

        //Button event handlers
        private void ApproveBtnClick(object sender, RoutedEventArgs e)      //ne pomeraj
        {
            Medicine selectedMedicine = MedicineDataGrid.SelectedItem as Medicine;
            updateMedicineApproval(selectedMedicine);

            ApprovalController.Instance.Update(selectedMedicine.MedicineData.Approval);
            MedicineDataController.Instance.Update(selectedMedicine.MedicineData);
            MedicineController.Instance.Update(selectedMedicine);

            allMedicineInStorageObservable[MedicineDataGrid.SelectedIndex] = selectedMedicine;
        }

        private void updateMedicineApproval(Medicine medicine)      //ne pomeraj
        {
            medicine.MedicineData.Approval.IsApproved = true;
            medicine.MedicineData.Approval.Doctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);
            medicine.MedicineData.Approval.Message = "";
        }

        private void RejectBtnClick(object sender, RoutedEventArgs e)       //ne pomeraj
        {
            RejectDrugView rejectDrugView = new RejectDrugView(MedicineDataGrid.SelectedItem as Medicine);
            rejectDrugView.Owner = this;
            rejectDrugView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            rejectDrugView.Show();
            this.Hide();
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)        //ne pomeraj
        {
            DoctorHome.doctorHome.Show();
            this.Close();
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)      //ne pomeraj
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)     //ne pomeraj
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
