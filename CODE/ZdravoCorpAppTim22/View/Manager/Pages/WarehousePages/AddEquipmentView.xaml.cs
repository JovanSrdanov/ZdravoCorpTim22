using Model;
using System;
using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels.WarehouseViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages
{
    public partial class AddEquipmentView : Page
    {
        public AddEquipmentViewModel ViewModel;
        public AddEquipmentView()
        {
            InitializeComponent();
            ViewModel = new AddEquipmentViewModel();
            DataContext = ViewModel;
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(EquipmentType));
        }
    }
}
