using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.DataModel;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;
using ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages;
using ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.WarehouseViewModels
{
    public class WarehouseRelocationViewModel
    {
        public ObservableCollection<Room> RoomList { get; set; }
        public ObservableCollection<EquipmentDataModel> EquipmentList { get; set; }
        public Interval Interval;
        public Room DestinationRoom;

        public RelayCommand RelocateCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }
        public WarehouseRelocationViewModel(List<Equipment> eq)
        {
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
            WarehouseToRoom(DestinationRoom, Interval);
            ManagerHome.NavigationService.Navigate(new WarehouseView());
        }
        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new WarehouseView());
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

        private void WarehouseToRoom(Room destination, Interval interval)
        {
            if (!destination.IsAvailable(interval))
            {
                return;
            }
            List<Equipment> equipment = new List<Equipment>();
            foreach (EquipmentDataModel eq in EquipmentList)
            {
                Equipment temp = new Equipment(eq.Equipment)
                {
                    Amount = eq.Amount
                };
                eq.Equipment.Amount -= temp.Amount;
                EquipmentController.Instance.Update(eq.Equipment);

                if (interval.End <= DateTime.Now || temp.EquipmentData.Type == EquipmentType.consumable)
                {
                    EquipmentController.Instance.AddRoomEquipment(destination, temp);
                }
                else
                {
                    equipment.Add(temp);
                }
            }
            EquipmentRelocationController.Instance.Create(null, destination, interval, equipment);
        }
    }
}
