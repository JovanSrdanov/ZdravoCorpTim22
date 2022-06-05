using Controller;
using Model;
using System;
using System.ComponentModel;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;
using ZdravoCorpAppTim22.View.Manager.Views;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels
{
    public class RoomMergeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand AddMergeCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }

        private int level;
        private string name;
        private string type;
        private double surface;

        public Room Room_1 { get; private set; }
        public Room Room_2 { get; private set; }
        public Interval Interval { get; set; }
        public RoomMergeViewModel(Room room_1, Room room_2)
        {
            Room_1 = room_1;
            Room_2 = room_2;

            Level = room_1.Level;
            RoomName = room_1.Name;
            Surface = room_1.Surface + room_2.Surface;
            Type = room_1.Type.ToString();

            AddMergeCommand = new RelayCommand(AddMerge, CanAddMerge);
            NavigateBackCommand = new RelayCommand(NavigateBack, null);
        }
        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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
                if (surface != value)
                {
                    surface = value;
                    OnPropertyChanged("Surface");
                }
            }
        }

        public void AddMerge(object obj)
        {
            if (RoomController.Instance.GetByID(Room_1.Id) == null || RoomController.Instance.GetByID(Room_2.Id) == null)
            {
                string msg = "One of the rooms was deleted in the meantime";
                if (ManagerHome.CurrentLanguage == 1)
                {
                    msg = "Jedna od soba je izbrisana u medjuvremenu";
                }
                InfoModal.Show(msg);
                ManagerHome.NavigationService.Navigate(new RoomView());
                return;
            }
            if (!Room_1.IsAvailable(Interval) || !Room_2.IsAvailable(Interval) || !Room_1.CanMergeOrDiverge() || !Room_2.CanMergeOrDiverge())
            {
                string msg = "One of the rooms isn't available";
                if (ManagerHome.CurrentLanguage == 1)
                {
                    msg = "Jedna od soba nije dostupna";
                }
                InfoModal.Show(msg);
                return;
            }
            if (!Room_1.Name.Equals(name) && !Room_2.Name.Equals(name) && RoomController.Instance.GetByName(name) != null)
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
            Room newRoom = new Room(0, level, rt, name, surface);

            RoomMergeController.Instance.Create(Room_1, Room_2, newRoom, Interval);
            
            ManagerHome.NavigationService.Navigate(new RoomView());
        }

        public bool CanAddMerge(object obj)
        {
            if (type == null)
            {
                return false;
            }
            if (name == null || name.Equals(""))
            {
                return false;
            }
            if (level < 0 || surface < 0)
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
