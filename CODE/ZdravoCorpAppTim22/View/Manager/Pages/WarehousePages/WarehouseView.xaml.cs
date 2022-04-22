using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
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
            NavigationService.Navigate(new RelocationView(this));
        }
    }
}
