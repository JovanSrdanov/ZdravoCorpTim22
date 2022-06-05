using Controller;
using Model;
using System;
using System.ComponentModel;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;
using ZdravoCorpAppTim22.View.Manager.Views;

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
                string msg = "Room was deleted in the meantime";
                if (ManagerHome.CurrentLanguage == 1)
                {
                    msg = "Soba je izbrisana u medjuvremenu";
                }
                InfoModal.Show(msg);
                ManagerHome.NavigationService.Navigate(new RoomView());
                return;
            }
            if (!OldRoom.IsAvailable(RenovationInterval))
            {
                string msg = "Room isn't available";
                if (ManagerHome.CurrentLanguage == 1)
                {
                    msg = "Soba nije dostupna";
                }
                InfoModal.Show(msg);
                return;
            }
            if (!OldRoom.Name.Equals(name) && RoomController.Instance.GetByName(name) != null)
            {
                string msg = "Room with that name already exists";
                if (ManagerHome.CurrentLanguage == 1)
                {
                    msg = "Soba sa tim imenom već postoji";
                }
                InfoModal.Show(msg);
                return;
            }
            RoomType rt = (RoomType)Enum.Parse(typeof(RoomType), type);

            RenovationController.Instance.Create(OldRoom, RoomName, Level, rt, Surface, RenovationInterval);

            ManagerHome.NavigationService.Navigate(new RoomView());
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
