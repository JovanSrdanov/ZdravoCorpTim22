using Model;
using System;
using System.ComponentModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages;
using ZdravoCorpAppTim22.View.Manager.Views;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.WarehouseViewModels
{
    public class AddEquipmentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand AddEquipmentCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }

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
        
        public AddEquipmentViewModel()
        {
            AddEquipmentCommand = new RelayCommand(AddEquipment, CanAddEquipment);
            NavigateBackCommand = new RelayCommand(NavigateBack, null);
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void AddEquipment(object obj)
        {
            EquipmentType et = (EquipmentType)Enum.Parse(typeof(EquipmentType), type);

            EquipmentData equipmentData = EquipmentDataController.Instance.GetByName(name);
            if (equipmentData == null)
            {
                equipmentData = new EquipmentData(-1, name, et);
                EquipmentDataController.Instance.Create(equipmentData);
            }
            else
            {
                string msg = "Equipment with that name already exists";
                if (ManagerHome.CurrentLanguage == 1)
                {
                    msg = "Oprema sa tim imenom već postoji";
                }
                InfoModal.Show(msg);
                return;
            }

            Equipment equipment = new Equipment
            {
                EquipmentData = equipmentData,
                Amount = amount
            };
            EquipmentController.Instance.AddWarehouseEquipment(equipment);

            ManagerHome.NavigationService.Navigate(new WarehouseView());
        }

        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new WarehouseView());
        }
        private bool CanAddEquipment(object obj)
        {
            if (type == null)
            {
                return false;
            }
            if (name == null || name.Equals(""))
            {
                return false;
            }
            if (Amount < 0)
            {
                return false;
            }
            return true;
        }
    }
}
