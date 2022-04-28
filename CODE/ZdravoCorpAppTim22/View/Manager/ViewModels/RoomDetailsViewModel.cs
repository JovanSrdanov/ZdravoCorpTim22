using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels
{
    public class RoomDetailsViewModel
    {
        Room Room;
        public ObservableCollection<Equipment> EquipmentCollection { get; set; }
        public RoomDetailsViewModel(Room room)
        {
            Room = room;
            List<Equipment> equipment = EquipmentController.Instance.GetRoomEquipment(room.Id);
            EquipmentCollection = new ObservableCollection<Equipment>(equipment);
            EquipmentController.Instance.GetAll().CollectionChanged += EquipmentListChangedEvent;
        }

        private void EquipmentListChangedEvent(object sender, NotifyCollectionChangedEventArgs e)
        {
            List<Equipment> equipment = EquipmentController.Instance.GetRoomEquipment(Room.Id);
            EquipmentCollection.Clear();
            foreach (Equipment eq in equipment)
            {
                EquipmentCollection.Add(eq);
            }
        }
    }
}
