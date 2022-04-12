using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RoomView : Page
    {
        public RoomViewModel ViewModel;
        public RoomView()
        {
            InitializeComponent();
            ViewModel = new RoomViewModel();
            DataContext = ViewModel;
        }

        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            EditRoomView addRoom = new EditRoomView();
            this.NavigationService.Navigate(addRoom);
        }

        private void EditRoom_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)dataGrid.SelectedItem;
            if(room == null)
            {
                return;
            }
            EditRoomView editRoom = new EditRoomView(room);
            this.NavigationService.Navigate(editRoom);
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)dataGrid.SelectedItem;
            if (room == null)
            {
                return;
            }
            ViewModel.RoomList.Remove(room);
            RoomController.Instance.DeleteRoomByID(room.id);
        }
    }
}
