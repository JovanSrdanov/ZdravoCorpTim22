using Controller;
using Model;
using System;
using System.ComponentModel;
using System.Windows;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;
using ZdravoCorpAppTim22.View.Manager.Views;

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
        private double surface;
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
        public double Surface
        {
            get => surface;
            set
            {
                if (surface != value)
                {
                    surface = value;
                    OnPropertyChanged("Surface");
                }
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
            if(RoomController.Instance.GetByName(name) != null)
            {
                InfoModal.Show("Room with that name already exists");
                return;
            }
            RoomType rt = (RoomType)Enum.Parse(typeof(RoomType), type);
            Room room = new Room(0, level, rt, name, surface);
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
            if (surface < 0)
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
