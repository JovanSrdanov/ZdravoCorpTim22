using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;
using ZdravoCorpAppTim22.View.Manager.Views;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels
{
    public class RoomDivergeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand AddDivergeCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }
        private int level_1;
        private string name_1;
        private string type_1;
        private double surface_1;

        private int level_2;
        private string name_2;
        private string type_2;
        private double surface_2;

        public readonly double SurfaceMax;

        public ObservableCollection<Equipment> Equipment_1 { get; set; }
        public ObservableCollection<Equipment> Equipment_2 { get; set; }

        public Room Room { get; private set; }
        public Interval Interval { get; set; }
        public RoomDivergeViewModel(Room room)
        {
            SurfaceMax = room.Surface;

            Room = room;
            Level_1 = room.Level;
            RoomName_1 = room.Name;
            Type_1 = room.Type.ToString();
            Surface_1 = room.Surface;

            Equipment_1 = new ObservableCollection<Equipment>();

            foreach(Equipment item in room.Equipment)
            {
                Equipment_1.Add(new Equipment(item));
            }

            Equipment_2 = new ObservableCollection<Equipment>();

            AddDivergeCommand = new RelayCommand(AddDiverge, CanAddDiverge);
            NavigateBackCommand = new RelayCommand(NavigateBack, null);
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Level_1
        {
            get => level_1;
            set
            {
                level_1 = value;
                OnPropertyChanged("Level_1");
            }
        }
        public string RoomName_1
        {
            get => name_1;
            set
            {
                name_1 = value;
                OnPropertyChanged("RoomName_1");
            }
        }
        public string Type_1
        {
            get => type_1;
            set
            {
                type_1 = value;
                OnPropertyChanged("Type_1");
            }
        }
        public double Surface_1
        {
            get => surface_1;
            set
            {
                if (surface_1 != value)
                {
                    if(value < 0)
                    {
                        surface_1 = 0;
                        Surface_2 = SurfaceMax;
                        OnPropertyChanged("Surface_1");
                    }
                    else if (value > SurfaceMax)
                    {
                        surface_1 = SurfaceMax;
                        Surface_2 = 0;
                        OnPropertyChanged("Surface_1");
                    }
                    else
                    {
                        surface_1 = value;
                        Surface_2 = SurfaceMax - value;
                        OnPropertyChanged("Surface_1");
                    }
                }
            }
        }
        public int Level_2
        {
            get => level_2;
            set
            {
                level_2 = value;
                OnPropertyChanged("Level_2");
            }
        }
        public string RoomName_2
        {
            get => name_2;
            set
            {
                name_2 = value;
                OnPropertyChanged("RoomName_2");
            }
        }
        public string Type_2
        {
            get => type_2;
            set
            {
                type_2 = value;
                OnPropertyChanged("Type_2");
            }
        }
        public double Surface_2
        {
            get => surface_2;
            set
            {
                if (surface_2 != value)
                {
                    if (value < 0)
                    {
                        surface_2 = 0;
                        Surface_1 = SurfaceMax;
                        OnPropertyChanged("Surface_2");
                    }
                    else if (value > SurfaceMax)
                    {
                        surface_2 = SurfaceMax;
                        Surface_1 = 0;
                        OnPropertyChanged("Surface_2");
                    }
                    else
                    {
                        surface_2 = value;
                        Surface_1 = SurfaceMax - value;
                        OnPropertyChanged("Surface_2");
                    }
                }
            }
        }

        public void AddToCollection(ObservableCollection<Equipment> sourceCollection, ObservableCollection<Equipment> targetCollection, Equipment equipment, int amount)
        {
            if (amount == equipment.Amount)
            {
                sourceCollection.Remove(equipment);
                Equipment temp = targetCollection.Where(x => x.EquipmentData.Id == equipment.EquipmentData.Id).FirstOrDefault();
                if (temp != null)
                {
                    temp.Amount += amount;
                    UpdateCollection(targetCollection, temp);
                }
                else
                {
                    targetCollection.Add(equipment);
                }
            }
            else
            {
                Equipment temp = targetCollection.Where(x => x.EquipmentData.Id == equipment.EquipmentData.Id).FirstOrDefault();
                if (temp != null)
                {
                    temp.Amount += amount;
                    UpdateCollection(targetCollection, temp);
                }
                else
                {
                    temp = new Equipment(equipment)
                    {
                        Id = -1,
                        Amount = amount
                    };
                    targetCollection.Add(temp);
                }
                equipment.Amount -= amount;

                UpdateCollection(sourceCollection, equipment);
            }
        }
        private void UpdateCollection(ObservableCollection<Equipment> collection, Equipment eq)
        {
            int index = collection.IndexOf(eq);
            collection.RemoveAt(index);
            collection.Insert(index, eq);
        }

        public void AddDiverge(object obj)
        {
            if (RoomController.Instance.GetByID(Room.Id) == null)
            {
                InfoModal.Show("Room was deleted in the meantime");
                ManagerHome.NavigationService.Navigate(new RoomView());
                return;
            }
            if (!Room.IsAvailable(Interval) || !Room.CanMergeOrDiverge())
            {
                InfoModal.Show("Room isn't available");
                return;
            }
            if ((!Room.Name.Equals(name_1) && RoomController.Instance.GetByName(name_1) != null) || (!Room.Name.Equals(name_2) && RoomController.Instance.GetByName(name_2) != null))
            {
                InfoModal.Show("Room with that name already exists");
                return;
            }
            RoomType rt_1 = (RoomType)Enum.Parse(typeof(RoomType), type_1);
            RoomType rt_2 = (RoomType)Enum.Parse(typeof(RoomType), type_2);
            Room first = new Room(-1, level_1, rt_1, name_1, surface_1);
            Room second = new Room(-1, level_2, rt_2, name_2, surface_2);

            RoomDivergeController.Instance.Create(first, second, Room, new List<Equipment>(Equipment_1), new List<Equipment>(Equipment_2), Interval);
            
            ManagerHome.NavigationService.Navigate(new RoomView());
        }

        public bool CanAddDiverge(object obj)
        {
            if (type_1 == null || type_2 == null)
            {
                return false;
            }
            if (name_1 == null || name_1.Equals("") || name_2 == null || name_2.Equals("") || name_1.Equals(name_2))
            {
                return false;
            }
            if (level_1 < 0 || level_2 < 0 || surface_1 < 0 || surface_2 < 0)
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
