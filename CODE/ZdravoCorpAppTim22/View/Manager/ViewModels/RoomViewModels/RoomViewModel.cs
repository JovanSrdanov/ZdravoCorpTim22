using Controller;
using Model;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels
{
    public class RoomViewModel
    {
        public ObservableCollection<Room> RoomList { get; set; }

        public RelayCommand OpenAddCommand { get; private set; }
        public RelayCommand OpenRenovateCommand { get; private set; }
        public RelayCommand DeleteRoomCommand { get; private set; }
        public RelayCommand OpenDetailsCommand { get; private set; }
        public RelayCommand OpenMergeCommand { get; private set; }
        
        public RoomViewModel()
        {
            OpenAddCommand = new RelayCommand(OpenAdd, null);
            OpenRenovateCommand = new RelayCommand(OpenRenovate, IsSelected);
            DeleteRoomCommand = new RelayCommand(DeleteRoom, IsSelected);
            OpenDetailsCommand = new RelayCommand(OpenDetails, IsSelected);
            OpenMergeCommand = new RelayCommand(OpenMerge, CanMerge);
            RoomList = RoomController.Instance.GetAll();
        }
        public RoomViewModel(Room room)
        {
            if(room != null)
            {
                List<Room> list = new List<Room>(RoomController.Instance.GetAll());
                int index = list.FindIndex(r => r.Id == room.Id);
                list.RemoveAt(index);
                RoomList = new ObservableCollection<Room>(list);
            }
        }

        public void OpenAdd(object obj)
        {
            ManagerHome.NavigationService.Navigate(new AddRoomView());
        }

        public void OpenRenovate(object obj)
        {
            Room room = (Room)obj;
            ManagerHome.NavigationService.Navigate(new RenovateView(room));
        }

        public void DeleteRoom(object obj)
        {
            Room room = (Room)obj;
            RoomController.Instance.DeleteByID(room.Id);
        }

        public void OpenDetails(object obj)
        {
            Room room = (Room)obj;
            ManagerHome.NavigationService.Navigate(new RoomDetailsView(room));
        }

        public void OpenMerge(object obj)
        {
            List<Room> selectedRooms = ((IList)obj).Cast<Room>().ToList();
            ManagerHome.NavigationService.Navigate(new RoomMergeView(selectedRooms[0], selectedRooms[1]));
        }

        private bool IsSelected(object obj)
        {
            if ((Room)obj == null)
            {
                return false;
            }
            return true;
        }
        private bool CanMerge(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            List<Room> selectedRooms = ((IList)obj).Cast<Room>().ToList();
            if (selectedRooms.Count == 2)
            {
                return true;
            }
            return false;
        }
    }
}
