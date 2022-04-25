using Model;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages
{
    public partial class AddEquipmentView : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int amount;
        private string name;
        private string type;
        public int Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public string EquipmentName
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("EquipmentName");
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

        public AddEquipmentView()
        {
            InitializeComponent();
            DataContext = this;
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(EquipmentType));
        }
        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WarehouseView());
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
            if (Amount < 0)
            {
                MessageBox.Show("Amount can't be nagative");
                return;
            }

            EquipmentType et = (EquipmentType)Enum.Parse(typeof(EquipmentType), type);
            Equipment equipment = new Equipment(0, name, amount, et, null);
            EquipmentController.Instance.Create(equipment);

            NavigationService.Navigate(new WarehouseView());
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new WarehouseView());
        }
    }
}
