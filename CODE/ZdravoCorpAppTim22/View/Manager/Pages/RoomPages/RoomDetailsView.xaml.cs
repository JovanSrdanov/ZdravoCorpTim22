using Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RoomDetailsView : Page
    {
        public Room Room { get; }
        public List<Equipment> SelectedEquipment { get; set; }
        public RoomDetailsViewModel ViewModel { get; }
        public RoomDetailsView(Room room)
        {
            InitializeComponent();
            ViewModel = new RoomDetailsViewModel(room);
            DataContext = ViewModel;
            Room = room;
            SelectedEquipment = new List<Equipment>();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedEquipment = DataGrid.SelectedItems.Cast<Equipment>().ToList();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RoomView());
        }

        private void ButtonRoomRelocate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEquipment.Count > 0)
            {
                NavigationService.Navigate(new RoomRelocationView(Room, this));
            }
        }

        private void ButtonWarehouseRelocate_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedEquipment.Count > 0)
            {
                NavigationService.Navigate(new RoomWarehouseRelocationView(Room, this));
            }
        }
    }
}
