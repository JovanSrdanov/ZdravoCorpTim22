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
        readonly WarehouseViewModel ViewModel;
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
            Equipment equipment = (Equipment)DataGrid.SelectedItem;
            if (equipment == null)
            {
                return;
            }
            EditEquipmentView editEquipment = new EditEquipmentView(equipment);
            NavigationService.Navigate(editEquipment);
        }

        private void DeleteEquipment_Click(object sender, RoutedEventArgs e)
        {
            Equipment equipment = (Equipment)DataGrid.SelectedItem;
            if (equipment == null)
            {
                return;
            }
            ViewModel.EquipmentCollection.Remove(equipment);

            List<Equipment> eqToRemove = new List<Equipment>();
            foreach (Equipment eq in EquipmentController.Instance.GetAll())
            {
                if(eq.EquipmentData.Id == equipment.EquipmentData.Id)
                {
                    eqToRemove.Add(eq);
                }
            }
            foreach(Equipment eq in eqToRemove)
            {
                eq.Room = null;
                EquipmentController.Instance.DeleteByID(eq.Id);
            }
            EquipmentDataController.Instance.DeleteByID(equipment.EquipmentData.Id);
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

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.SearchText = SearchTextBox.Text;
        }
    }
}
