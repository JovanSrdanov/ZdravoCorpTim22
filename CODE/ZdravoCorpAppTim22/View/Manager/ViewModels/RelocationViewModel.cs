using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class RelocationViewModel
    {
        public ObservableCollection<Room> RoomList { get; set; }
        public ObservableCollection<Equipment> EquipmentList { get; set; }
        public RelocationViewModel(List<Equipment> eq)
        {
            EquipmentList = new ObservableCollection<Equipment>(eq);
            RoomList = new RoomViewModel().RoomList;
        }
    }
}
