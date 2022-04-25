using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.View.Manager.DataModel;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class RelocationViewModel
    {
        public ObservableCollection<Room> RoomList { get; set; }
        public ObservableCollection<EquipmentDataModel> EquipmentList { get; set; }
        public RelocationViewModel(List<Equipment> eq)
        {
            List<EquipmentDataModel> temp = new List<EquipmentDataModel>();
            foreach (Equipment eqItem in eq)
            {
                temp.Add(new EquipmentDataModel(eqItem.Amount, eqItem));
            }
            EquipmentList = new ObservableCollection<EquipmentDataModel>(temp);
            RoomList = new RoomViewModel().RoomList;
        }
    }
}
