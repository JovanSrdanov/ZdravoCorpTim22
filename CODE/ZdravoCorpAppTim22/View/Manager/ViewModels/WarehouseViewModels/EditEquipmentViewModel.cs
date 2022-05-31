using Model;
using System;
using System.ComponentModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages;
using ZdravoCorpAppTim22.View.Manager.Views;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.WarehouseViewModels
{
    public class EditEquipmentViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public RelayCommand EditEquipmentCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }

        Equipment OldEquipment { get; }
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
        public EditEquipmentViewModel(Equipment oldEquipment)
        {
            EditEquipmentCommand = new RelayCommand(EditEquipment, CanEditEquipment);
            NavigateBackCommand = new RelayCommand(NavigateBack, null);
            OldEquipment = oldEquipment;
            Amount = oldEquipment.Amount;
            EquipmentName = oldEquipment.EquipmentData.Name;
            Type = oldEquipment.EquipmentData.Type.ToString();
        }
        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void EditEquipment(object obj)
        {
            if (!OldEquipment.EquipmentData.Name.Equals(name) && EquipmentDataController.Instance.GetByName(name) != null)
            {
                InfoModal.Show("Equipment with that name already exists");
                return;
            }
            EquipmentType et = (EquipmentType)Enum.Parse(typeof(EquipmentType), type);

            EquipmentController.Instance.Update(OldEquipment, EquipmentName, Amount, et);

            ManagerHome.NavigationService.Navigate(new WarehouseView());
        }
        public bool CanEditEquipment(object obj)
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
        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new WarehouseView());
        }
    }
}
