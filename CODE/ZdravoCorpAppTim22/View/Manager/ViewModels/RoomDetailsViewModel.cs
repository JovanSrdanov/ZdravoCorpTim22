using Model;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class RoomDetailsViewModel
    {
        public RelayCommand AddRoomCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }
        public ObservableCollection<Equipment> EquipmentCollection { get; set; }
        public RoomDetailsViewModel(Room room)
        {
            EquipmentCollection = room.Equipment;
            NavigateBackCommand = new RelayCommand(NavigateBack, null);
        }

        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new RoomView());
        }
    }
}
