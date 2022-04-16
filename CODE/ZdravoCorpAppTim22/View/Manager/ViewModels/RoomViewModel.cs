using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
