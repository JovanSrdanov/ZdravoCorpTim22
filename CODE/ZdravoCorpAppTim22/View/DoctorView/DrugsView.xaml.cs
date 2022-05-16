using Controller;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    /// <summary>
    /// Interaction logic for DrugsView.xaml
    /// </summary>
    public partial class DrugsView : Window
    {
        public ObservableCollection<Medicine> allMedicineInStorageObservale;
        public DrugsView()
        {
            InitializeComponent();
            List<Medicine> allMedicineInStorage = MedicineController.Instance.GetAllFree();
            allMedicineInStorageObservale = new ObservableCollection<Medicine>(allMedicineInStorage);
            MedicineDataGrid.ItemsSource = allMedicineInStorageObservale;
        }

        //Button event handlers
        private void ApproveBtnClick(object sender, RoutedEventArgs e)
        {
            Medicine selected = MedicineDataGrid.SelectedItem as Medicine;
            selected.MedicineData.Approval.IsApproved = true;
            selected.MedicineData.Approval.Doctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);
            selected.MedicineData.Approval.Message = "";
            ApprovalController.Instance.Update(selected.MedicineData.Approval);
            MedicineDataController.Instance.Update(selected.MedicineData);
            MedicineController.Instance.Update(selected);
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
            DoctorHome.doctorHome.Show();
            this.Close();
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
