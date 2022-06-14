using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class RoomViewModel : ViewModel
    {
        public string RoomName { get; set; }
        public Room Room { get; set; }

        public RoomViewModel(Room room)
        {
            RoomName = room.Name;
            Room = room;
        }
    }
}
