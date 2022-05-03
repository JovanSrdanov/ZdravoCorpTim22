using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class WarehouseViewModel
    {
        public ObservableCollection<Equipment> EquipmentCollection { get; set; }
        public WarehouseViewModel()
        {
            List<Equipment> equipment = EquipmentController.Instance.GetWarehouseEquipment();
            EquipmentCollection = new ObservableCollection<Equipment>(equipment);
            EquipmentController.Instance.GetAll().CollectionChanged += EquipmentListChangedEvent;
        }
        private void EquipmentListChangedEvent(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<Equipment> equipment = EquipmentController.Instance.GetWarehouseEquipment();
            EquipmentCollection.Clear();
            foreach (Equipment eq in equipment)
            {
                EquipmentCollection.Add(eq);
            }
        }
    }
}
