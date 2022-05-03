using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class RoomDetailsViewModel
    {
        public ObservableCollection<Equipment> EquipmentCollection { get; set; }
        public RoomDetailsViewModel(Room room)
        {
            EquipmentCollection = room.Equipment;
        }
    }
}
