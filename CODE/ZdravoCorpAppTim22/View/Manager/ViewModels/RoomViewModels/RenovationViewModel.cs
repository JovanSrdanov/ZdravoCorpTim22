using Controller;
using Model;
using System;
using System.ComponentModel;
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

        private int id;
        private int level;
        private string name;
        private string type;

        public Room OldRoom { get; }

        private Interval renovationInterval;

        public RenovationViewModel(Room room)
        {
            OldRoom = room;
            ID = room.Id;
            Level = room.Level;
            RoomName = room.Name;
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
        public int ID
        {
            get => id;
            set
            {
                id = value;
                OnPropertyChanged("ID");
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

        public void AddRenovation(object obj)
        {
            RoomType rt = (RoomType)Enum.Parse(typeof(RoomType), type);
            if (RenovationInterval.End <= DateTime.Now)
            {
                OldRoom.Name = name;
                OldRoom.Level = level;
                OldRoom.Type = rt;
                RoomController.Instance.Update(OldRoom);
                ManagerHome.NavigationService.Navigate(new RoomView());
            }
            else if (OldRoom.IsAvailable(RenovationInterval))
            {
                Room room = new Room(id, level, rt, name);
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
            return true;
        }
        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new RoomView());
        }
    }
}
