using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        }
    }
}
