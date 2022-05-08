using Model;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.WarehouseViewModels
{
    public class WarehouseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public RelayCommand OpenAddCommand { get; private set; }
        public RelayCommand OpenEditCommand { get; private set; }
        public RelayCommand DeleteEquipmentCommand { get; private set; }
        public RelayCommand OpenRelocationCommand { get; private set; }

        public ObservableCollection<Equipment> EquipmentCollection { get; set; }
        public ObservableCollection<Equipment> FilteredEquipment
        {
            get
            {
                if (SearchText == null || SearchText == "") return EquipmentCollection;
                IEnumerable<Equipment> result = EquipmentCollection.Where(x => x.EquipmentData.Name.ToUpper().StartsWith(SearchText.ToUpper()));
                return new ObservableCollection<Equipment>(result);
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                OnPropertyChanged("FilteredEquipment");
            }
        }

        public WarehouseViewModel()
        {
            OpenAddCommand = new RelayCommand(OpenAdd, null);
            OpenEditCommand = new RelayCommand(OpenEdit, IsSelected);
            DeleteEquipmentCommand = new RelayCommand(DeleteEquipment, IsSelected);
            OpenRelocationCommand = new RelayCommand(OpenRelocation, CanRelocate);

            List<Equipment> equipment = EquipmentController.Instance.GetWarehouseEquipment();
            EquipmentCollection = new ObservableCollection<Equipment>(equipment);
            EquipmentController.Instance.GetAll().CollectionChanged += EquipmentListChangedEvent;
        }

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        private void EquipmentListChangedEvent(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<Equipment> equipment = EquipmentController.Instance.GetWarehouseEquipment();
            EquipmentCollection.Clear();
            foreach (Equipment eq in equipment)
            {
                EquipmentCollection.Add(eq);
            }
            OnPropertyChanged("FilteredEquipment");
        }

        public void OpenAdd(object obj)
        {
            ManagerHome.NavigationService.Navigate(new AddEquipmentView());
        }
        public void OpenEdit(object obj)
        {
            Equipment equipment = (Equipment)obj;
            EditEquipmentView editEquipment = new EditEquipmentView(equipment);
            ManagerHome.NavigationService.Navigate(editEquipment);
        }
        public void DeleteEquipment(object obj)
        {
            Equipment equipment = (Equipment)obj;
            EquipmentCollection.Remove(equipment);

            List<Equipment> eqToRemove = new List<Equipment>();
            foreach (Equipment eq in EquipmentController.Instance.GetAll())
            {
                if (eq.EquipmentData.Id == equipment.EquipmentData.Id)
                {
                    eqToRemove.Add(eq);
                }
            }
            foreach (Equipment eq in eqToRemove)
            {
                eq.Room = null;
                EquipmentController.Instance.DeleteByID(eq.Id);
            }
            EquipmentDataController.Instance.DeleteByID(equipment.EquipmentData.Id);
        }
        public void OpenRelocation(object obj)
        {
            List<Equipment> selectedEquipment = ((IList)obj).Cast<Equipment>().ToList();
            ManagerHome.NavigationService.Navigate(new WarehouseRelocationView(selectedEquipment));   
        }

        private bool CanRelocate(object obj)
        {
            if(obj == null)
            {
                return false;
            }
            List<Equipment> selectedEquipment = ((IList)obj).Cast<Equipment>().ToList();
            if (selectedEquipment.Count > 0)
            {
                return true;
            }
            return false;
        }
        private bool IsSelected(object obj)
        {
            if ((Equipment)obj == null)
            {
                return false;
            }
            return true;
        }
    }
}
