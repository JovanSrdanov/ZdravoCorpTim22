using Controller;
using Model;
using System.Windows;
using System.Windows.Controls;
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
            AddRoomView addRoom = new AddRoomView();
            this.NavigationService.Navigate(addRoom);
        }

        private void RenovateRoom_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)dataGrid.SelectedItem;
            if(room == null)
            {
                return;
            }
            RenovateView renovateRoom = new RenovateView(room);
            this.NavigationService.Navigate(renovateRoom);
        }

        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)dataGrid.SelectedItem;
            if (room == null)
            {
                return;
            }
            ViewModel.RoomList.Remove(room);
            RoomController.Instance.DeleteByID(room.Id);
        }

        private void ButtonDetails_Click(object sender, RoutedEventArgs e)
        {
            Room room = (Room)dataGrid.SelectedItem;
            if (room == null)
            {
                return;
            }
            NavigationService.Navigate(new RoomDetailsView(room));
        }
    }
}
