using System.Windows;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.ViewModels.MedicineViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Views
{
    public partial class MedicineDetailsModal : Window
    {
        public MedicineDetailsViewModel ViewModel { get; private set; }
        private MedicineDetailsModal(Medicine medicine)
        {
            InitializeComponent();
            ViewModel = new MedicineDetailsViewModel(medicine);
            DataContext = ViewModel;
            if (medicine.MedicineData.Approval.Message == null || medicine.MedicineData.Approval.Message.Trim().Equals(""))
            {
                MessagePanel.Visibility = Visibility.Hidden;
            }
        }

        public static void Show(Medicine medicine)
        {
            MedicineDetailsModal modal = new MedicineDetailsModal(medicine)
            {
                Owner = ManagerHome.Instance
            };
            modal.ShowDialog();
        }

        private void CloseEvent(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
