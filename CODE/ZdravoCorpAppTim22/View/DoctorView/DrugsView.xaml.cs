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
                if (ApprovalController.Instance.isRejected(item))
                    allMedicineInStorageObservable.Remove(item);
            }
            MedicineDataGrid.ItemsSource = allMedicineInStorageObservable;
        }

        private void MedicineDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Medicine selectedMedicine = MedicineDataGrid.SelectedItem as Medicine;
            if (selectedMedicine == null || ApprovalController.Instance.isApproved(selectedMedicine.MedicineData.Approval))
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

        private void ApproveBtnClick(object sender, RoutedEventArgs e)
        {
            Medicine selectedMedicine = MedicineDataGrid.SelectedItem as Medicine;
            updateMedicineApproval(selectedMedicine);

            ApprovalController.Instance.Update(selectedMedicine.MedicineData.Approval);
            MedicineDataController.Instance.Update(selectedMedicine.MedicineData);
            MedicineController.Instance.Update(selectedMedicine);

            allMedicineInStorageObservable[MedicineDataGrid.SelectedIndex] = selectedMedicine;
        }

        private void updateMedicineApproval(Medicine medicine)
        {
            medicine.MedicineData.Approval.IsApproved = true;
            medicine.MedicineData.Approval.Doctor = DoctorController.Instance.GetByID(DoctorHomeScreen.LoggedInDoctor.Id);
            medicine.MedicineData.Approval.Message = "";
        }

        private void RejectBtnClick(object sender, RoutedEventArgs e)
        {
            RejectDrugView rejectDrugView = new RejectDrugView(MedicineDataGrid.SelectedItem as Medicine);
            rejectDrugView.Owner = this;
            rejectDrugView.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            rejectDrugView.Show();
            this.Hide();
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Show();
            foreach (Window item in App.Current.Windows)
            {
                if (item != Application.Current.MainWindow)
                {
                    item.Close();
                }
            }
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
