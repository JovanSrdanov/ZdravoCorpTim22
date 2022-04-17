using Controller;
using Model;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.ViewModels;
using ZdravoCorpAppTim22.View.Manager.Views;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RenovateView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private int level;
        private string name;
        private string type;
        
        public Interval RenovationInterval { get; set; }
        
        private Room OldRoom { get; }

        public RenovateView(Room room)
        {
            init();
            OldRoom = room;
            ID = room.Id;
            Level = room.Level;
            RoomName = room.Name;
            type = room.Type.ToString();
            Debug.WriteLine(RenovationInterval.Start);
            Debug.WriteLine(RenovationInterval.End);
        }

        private void init()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RoomView());
        }

        private void ButtonSelectStartTime_Click(object sender, RoutedEventArgs e)
        {
            var RenovateStartDateView = new RenovateStartDateView(this, OldRoom);
            this.NavigationService.Navigate(RenovateStartDateView);
        }
    }
}
