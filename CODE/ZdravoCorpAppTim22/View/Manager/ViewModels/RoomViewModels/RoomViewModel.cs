using Controller;
using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;
using ZdravoCorpAppTim22.View.Manager.Views;

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
        public RelayCommand OpenDivergeCommand { get; private set; }

        public RoomViewModel()
        {
            OpenAddCommand = new RelayCommand(OpenAdd, null);
            OpenRenovateCommand = new RelayCommand(OpenRenovate, IsSelected);
            DeleteRoomCommand = new RelayCommand(DeleteRoom, IsSelected);
            OpenDetailsCommand = new RelayCommand(OpenDetails, IsSelected);
            OpenMergeCommand = new RelayCommand(OpenMerge, CanMerge);
            OpenDivergeCommand = new RelayCommand(OpenDiverge, IsSelected);

            RoomList = new ObservableCollection<Room>(RoomController.Instance.GetAll());
            RoomController.Instance.DataChanged += RoomDataChangedHandler;
        }
        public RoomViewModel(Room room)
        {
            if(room != null)
            {
                List<Room> list = new List<Room>(RoomController.Instance.GetAll());
                int index = list.FindIndex(r => r.Id == room.Id);
                list.RemoveAt(index);
                RoomList = new ObservableCollection<Room>(list);
                RoomController.Instance.DataChanged += RoomDataChangedHandler;
            }
        }

        public void RoomDataChangedHandler(object sender, EventArgs e)
        {
            List<Room> rooms = RoomController.Instance.GetAll();
            RoomList.Clear();
            foreach (Room room in rooms)
            {
                RoomList.Add(room);
            }
        }

        public void OpenAdd(object obj)
        {
            ManagerHome.NavigationService.Navigate(new AddRoomView());
        }

        public void OpenRenovate(object obj)
        {
            List<Room> selectedRooms = ((IList)obj).Cast<Room>().ToList();
            Room room = selectedRooms[0];
            ManagerHome.NavigationService.Navigate(new RenovateView(room));
        }

        public void DeleteRoom(object obj)
        {
            string msg = "Are you sure?";
            if (ManagerHome.CurrentLanguage == 1)
            {
                msg = "Da li ste sigurni?";
            }
            if (ConfirmModal.Show(msg))
            {
                List<Room> selectedRooms = ((IList)obj).Cast<Room>().ToList();
                Room room = selectedRooms[0];
                RoomList.Remove(room);
                RoomController.Instance.DeleteByID(room.Id);
            }
        }

        public void OpenDetails(object obj)
        {
            List<Room> selectedRooms = ((IList)obj).Cast<Room>().ToList();
            Room room = selectedRooms[0];
            ManagerHome.NavigationService.Navigate(new RoomDetailsView(room));
        }

        public void OpenMerge(object obj)
        {
            List<Room> selectedRooms = ((IList)obj).Cast<Room>().ToList();
            ManagerHome.NavigationService.Navigate(new RoomMergeView(selectedRooms[0], selectedRooms[1]));
        }

        public void OpenDiverge(object obj)
        {
            List<Room> selectedRooms = ((IList)obj).Cast<Room>().ToList();
            Room room = selectedRooms[0];
            ManagerHome.NavigationService.Navigate(new RoomDivergeView(room));
        }

        private bool IsSelected(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            List<Room> selectedRooms = ((IList)obj).Cast<Room>().ToList();
            if (selectedRooms.Count == 1)
            {
                return true;
            }
            return false;
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
