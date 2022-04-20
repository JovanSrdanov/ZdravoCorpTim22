using Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RenovateView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private int level;
        private string name;
        private string type;

        private Room OldRoom { get; }

        private Interval renovationInterval;

        public RenovateView(Room room)
        {
            InitializeComponent();
            DataContext = this;
            OldRoom = room;
            ID = room.Id;
            Level = room.Level;
            RoomName = room.Name;
            type = room.Type.ToString();

            TypeComboBox.ItemsSource = Enum.GetValues(typeof(RoomType));
            EndTimeGroup.Visibility = Visibility.Hidden;
            RoomEdit.Visibility = Visibility.Hidden;
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

        public void StartDateSelected(object sender, EventArgs e)
        {
            renovationInterval.Start = ((CustomDatePicker)sender).SelectedDate;
            if (DateTime.Compare(renovationInterval.Start, new DateTime()) > 0)
            {
                SelectStartTimeContent.Content = renovationInterval.Start.ToString();
                EndTimeGroup.Visibility = Visibility.Visible;
                RoomEdit.Visibility = Visibility.Hidden;
                renovationInterval.End = new DateTime();
                SelectEndTimeContent.Content = "";
            }
            else
            {
                EndTimeGroup.Visibility = Visibility.Hidden;
            }
        }

        public void EndDateSelected(object sender, EventArgs e)
        {
            renovationInterval.End = ((CustomDatePicker)sender).SelectedDate;
            if (DateTime.Compare(renovationInterval.End, new DateTime()) > 0)
            {
                SelectEndTimeContent.Content = renovationInterval.End.ToString();
                RoomEdit.Visibility = Visibility.Visible;
            }
            else
            {
                RoomEdit.Visibility = Visibility.Hidden;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RoomView());
        }

        private void ButtonSelectStartTime_Click(object sender, RoutedEventArgs e)
        {
            var RenovateStartDateView = new SelectTimePage(this, OldRoom);
            RenovateStartDateView.CustomDatePicker.DateSelectedEvent += StartDateSelected;
            this.NavigationService.Navigate(RenovateStartDateView);
        }

        private void ButtonSelectEndTime_Click(object sender, RoutedEventArgs e)
        {
            var RenovateEndDateView = new SelectTimePage(this, OldRoom, renovationInterval.Start);
            RenovateEndDateView.CustomDatePicker.DateSelectedEvent += EndDateSelected;
            this.NavigationService.Navigate(RenovateEndDateView);
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            if (type == null)
            {
                return;
            }
            if (name == null || name.Equals(""))
            {
                MessageBox.Show("Name can't be empty");
                return;
            }
            if (level < 0)
            {
                MessageBox.Show("Level can't be nagative");
                return;
            }

            RoomType rt = (RoomType)Enum.Parse(typeof(RoomType), type);
            Room room = new Room(id, level, rt, name);
            if (room.IsAvailable(RenovationInterval.Start, RenovationInterval.End))
            {
                Renovation renovation = new Renovation(0, OldRoom, room, RenovationInterval);
                RenovationController.Instance.Create(renovation);
                this.NavigationService.Navigate(new RoomView());
            }
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RoomView());
        }
    }
}
