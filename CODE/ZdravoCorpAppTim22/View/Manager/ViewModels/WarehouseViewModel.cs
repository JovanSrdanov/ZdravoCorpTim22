using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class WarehouseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
    }
}
