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
    public partial class RejectDrugView : Window
    {
        private Medicine rejectedMedicine;
        public RejectDrugView(Medicine rejectedMedicine)
        {
            InitializeComponent();
            this.rejectedMedicine = rejectedMedicine;
        }

        //Button click event handlers
        private void ConfirmBtnClick(object sender, RoutedEventArgs e)
        {
            rejectedMedicine.MedicineData.Approval.IsApproved = false;
            rejectedMedicine.MedicineData.Approval.Doctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);
            rejectedMedicine.MedicineData.Approval.Message = ReasonTextBox.Text;

            DrugsView.allMedicineInStorageObservable.Remove(rejectedMedicine);

            ApprovalController.Instance.Update(rejectedMedicine.MedicineData.Approval);
            MedicineDataController.Instance.Update(rejectedMedicine.MedicineData);
            MedicineController.Instance.Update(rejectedMedicine);

            this.Owner.Show();
            this.Close();
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Close window without saving?", "Reject drug request", 
                MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    this.Owner.Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:

                    break;
            }
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
    }
}
