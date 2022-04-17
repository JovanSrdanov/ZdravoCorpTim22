using Controller;
using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class RoomViewModel
    {
        public ObservableCollection<Room> RoomList { get; set; }

        public RoomViewModel()
        {
            List<Room> roomRep = RoomController.Instance.GetAll();
            RoomList = new ObservableCollection<Room>(roomRep);
        }
    }


}
