using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class EquipmentViewModel
    {
        public ObservableCollection<Equipment> EquipmentCollection { get; set; }
        public EquipmentViewModel(Room room)
        {
            List<Equipment> equipment = EquipmentController.Instance.GetRoomEquipment(room.Id);
            EquipmentCollection = new ObservableCollection<Equipment>(equipment);
        }
    }
}
