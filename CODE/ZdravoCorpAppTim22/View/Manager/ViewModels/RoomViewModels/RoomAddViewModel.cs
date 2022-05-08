using Controller;
using Model;
using System;
using System.ComponentModel;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels
{
    public class RoomAddViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand AddRoomCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }

        private int level;
        private string name;
        private string type;
        public int Level
        {
            get => level;
            set
            {
                level = value;
                OnPropertyChanged("Level");
            }
        }
        public string RoomName
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("RoomName");
            }
        }
        public string Type
        {
            get => type;
            set
            {
                type = value;
                OnPropertyChanged("Type");
            }
        }

        public RoomAddViewModel()
        {
            AddRoomCommand = new RelayCommand(AddRoom, CanAddRoom);
            NavigateBackCommand = new RelayCommand(NavigateBack, null);
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddRoom(object obj)
        {
            RoomType rt = (RoomType)Enum.Parse(typeof(RoomType), type);
            Room room = new Room(0, level, rt, name);
            RoomController.Instance.Create(room);
            ManagerHome.NavigationService.Navigate(new RoomView());
        }
        public bool CanAddRoom(object obj)
        {
            if (type == null)
            {
                return false;
            }
            if (name == null || name.Equals(""))
            {
                return false;
            }
            if (level < 0)
            {
                return false;
            }
            return true;
        }
        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new RoomView());
        }
    }
}
