using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.DataModel;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;
using ZdravoCorpAppTim22.View.Manager.Views;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels
{
    public class RoomWarehouseRelocationViewModel
    {
        public ObservableCollection<Room> RoomList { get; set; }
        public ObservableCollection<EquipmentDataModel> EquipmentList { get; set; }
        public Interval Interval;
        public Room SourceRoom;

        public RelayCommand RelocateCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }

        public RoomWarehouseRelocationViewModel(List<Equipment> eq, Room room)
        {
            SourceRoom = room;
            RelocateCommand = new RelayCommand(Relocate, CanRelocate);
            NavigateBackCommand = new RelayCommand(NavigateBack, null);

            List<EquipmentDataModel> temp = new List<EquipmentDataModel>();
            foreach (Equipment eqItem in eq)
            {
                temp.Add(new EquipmentDataModel(eqItem.Amount, eqItem));
            }
            EquipmentList = new ObservableCollection<EquipmentDataModel>(temp);
            RoomList = new RoomViewModel().RoomList;
        }

        public void Relocate(object obj)
        {
            if (RoomController.Instance.GetByID(SourceRoom.Id) == null)
            {
                InfoModal.Show("Room was deleted in the meantime");
                ManagerHome.NavigationService.Navigate(new RoomView());
                return;
            }
            if (!SourceRoom.IsAvailable(Interval))
            {
                InfoModal.Show("Room isn't available");
                return;
            }
            EquipmentRelocationController.Instance.MoveRoomToWarehouse(SourceRoom, new List<EquipmentDataModel>(EquipmentList), Interval);

            ManagerHome.NavigationService.Navigate(new RoomDetailsView(SourceRoom));
        }
        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new RoomDetailsView(SourceRoom));
        }

        private bool CanRelocate(object obj)
        {
            bool valid = true;
            foreach (EquipmentDataModel eq in EquipmentList)
            {
                if (!eq.IsAmountValid())
                {
                    valid = false;
                }
            }
            return valid;
        }
    }
}
