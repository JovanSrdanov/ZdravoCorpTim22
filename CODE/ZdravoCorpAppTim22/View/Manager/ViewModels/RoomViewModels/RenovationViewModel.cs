using Controller;
using Model;
using System;
using System.ComponentModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels
{
    public class RenovationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand AddRenovationCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }

        private int level;
        private string name;
        private string type;
        private double surface;
        public Room OldRoom { get; }

        private Interval renovationInterval;

        public RenovationViewModel(Room room)
        {
            OldRoom = room;
            Level = room.Level;
            RoomName = room.Name;
            Surface = room.Surface;
            Type = room.Type.ToString();

            AddRenovationCommand = new RelayCommand(AddRenovation, CanAddRenovation);
            NavigateBackCommand = new RelayCommand(NavigateBack, null);
        }
        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Interval RenovationInterval
        {
            get => renovationInterval;
            set
            {
                renovationInterval = value;
                OnPropertyChanged("RenovationInterval");
            }
        }
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
                if(surface != value)
                {
                    surface = value;
                    OnPropertyChanged("Surface");
                }
            }
        }

        public void AddRenovation(object obj)
        {
            if (RoomController.Instance.GetByID(OldRoom.Id) == null)
            {
                MessageBox.Show("Room was deleted in the meantime");
                ManagerHome.NavigationService.Navigate(new RoomView());
                return;
            }
            if (!OldRoom.IsAvailable(RenovationInterval))
            {
                MessageBox.Show("Room isn't available");
                return;
            }
            if (!OldRoom.Name.Equals(name) && RoomController.Instance.GetByName(name) != null)
            {
                MessageBox.Show("Room with that name already exists");
                return;
            }
            RoomType rt = (RoomType)Enum.Parse(typeof(RoomType), type);
            if (RenovationInterval.End <= DateTime.Now)
            {
                OldRoom.Name = name;
                OldRoom.Level = level;
                OldRoom.Type = rt;
                OldRoom.Surface = surface;
                RoomController.Instance.Update(OldRoom);
                ManagerHome.NavigationService.Navigate(new RoomView());
            }
            else
            {
                Room room = new Room(0, level, rt, name, surface);
                Renovation renovation = new Renovation(0, OldRoom, room, RenovationInterval);
                RenovationController.Instance.Create(renovation);
                ManagerHome.NavigationService.Navigate(new RoomView());
            }
        }
        public bool CanAddRenovation(object obj)
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
