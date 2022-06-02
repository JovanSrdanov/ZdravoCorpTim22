using Controller;
using System.Windows;
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
            updateMedicineApproval();

            DrugsView.allMedicineInStorageObservable.Remove(rejectedMedicine);

            ApprovalController.Instance.Update(rejectedMedicine.MedicineData.Approval);
            MedicineDataController.Instance.Update(rejectedMedicine.MedicineData);
            MedicineController.Instance.Update(rejectedMedicine);

            this.Owner.Show();
            this.Close();
        }

        private void updateMedicineApproval()       //ne pomeraj
        {
            rejectedMedicine.MedicineData.Approval.IsApproved = false;
            rejectedMedicine.MedicineData.Approval.Doctor = DoctorController.Instance.GetByID(DoctorHome.selectedDoctorId);
            rejectedMedicine.MedicineData.Approval.Message = ReasonTextBox.Text;
        }

        private void CancelBtnClick(object sender, RoutedEventArgs e)       //ne pomeraj
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
    }
}
