using Model;
using System;
using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels.WarehouseViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages
{
    public partial class EditEquipmentView : Page
    {
        public EditEquipmentViewModel ViewModel;
        public EditEquipmentView(Equipment equipment)
        {
            InitializeComponent();
            ViewModel = new EditEquipmentViewModel(equipment);
            DataContext = ViewModel;
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(EquipmentType));
        }
    }
}
