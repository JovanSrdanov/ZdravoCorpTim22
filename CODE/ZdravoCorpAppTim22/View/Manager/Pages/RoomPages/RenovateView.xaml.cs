using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.View.Manager.ViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RenovateView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private int level;
        private string name;
        private string type;

        public RenovateView(Room room)
        {
            init();
            ID = room.Id;
            Level = room.Level;
            RoomName = room.Name;
            type = room.Type.ToString();
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
            this.NavigationService.GoBack();
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            return;

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

            RoomController.Instance.Update(room);
            

            this.NavigationService.GoBack();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }
    }
}
