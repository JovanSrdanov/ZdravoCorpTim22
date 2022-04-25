using Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.View.Manager.ViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages
{
    public partial class WarehouseView : Page
    {
        WarehouseViewModel ViewModel;
        public List<Equipment> SelectedEquipment;
        public WarehouseView()
        {
            InitializeComponent();
            ViewModel = new WarehouseViewModel();
            DataContext = ViewModel;
            SelectedEquipment = new List<Equipment>();
        }

        private void AddEquipment_Click(object sender, RoutedEventArgs e)
        {
            AddEquipmentView addEquipment = new AddEquipmentView();
            NavigationService.Navigate(addEquipment);
        }

        private void EditEquipment_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Not yet implemented");
        }

        private void DeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = (Equipment)DataGrid.SelectedItem;
            if (equipment == null)
            {
                return;
            }
            ViewModel.EquipmentCollection.Remove(equipment);
            EquipmentController.Instance.DeleteByID(equipment.Id);
        }
        
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedEquipment = DataGrid.SelectedItems.Cast<Equipment>().ToList();
        }
        private void RelocateEquipment_Click(object sender, RoutedEventArgs e)
        {
            if(SelectedEquipment.Count > 0)
            {
                NavigationService.Navigate(new RelocationView(this));
            }
        }
    }
}
