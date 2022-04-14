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
    public partial class EditRoomView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int id;
        private int level;
        private string name;
        private string type;

        private bool add;

        public EditRoomView()
        {
            init();
            add = true;
            id = 0;
        }

        public EditRoomView(Room room)
        {
            init();
            add = false;
            ID = room.id;
            Level = room.level;
            RoomName = room.name;
            type = room.type.ToString();

            //IDInput.IsEnabled = false;
        }

        private void init()
        {
            InitializeComponent();
            DataContext = this;
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(RoomType));
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

            if (add)
            {
                RoomController.Instance.CreateRoom(room);
            }
            else
            {
                RoomController.Instance.UpdateRoom(room);
            }

            this.NavigationService.GoBack();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.GoBack();
        }

        private bool checkIfRoomExists(int id)
        {
            foreach (Room room in RoomController.Instance.GetAllRooms())
            {
                if (room.id == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
