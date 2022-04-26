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
        public RoomViewModel(Room room)
        {
            if(room != null)
            {
                List<Room> list = new List<Room>(RoomController.Instance.GetAll());
                int index = list.FindIndex(r => r.Id == room.Id);
                list.RemoveAt(index);
                RoomList = new ObservableCollection<Room>(list);
            }
        }
    }
}
