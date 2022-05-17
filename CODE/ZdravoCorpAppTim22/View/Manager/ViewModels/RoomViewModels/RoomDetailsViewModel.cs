using Model;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.RoomPages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels
{
    public class RoomDetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand OpenRoomRelocationCommand { get; private set; }
        public RelayCommand OpenWarehouseRelocationCommand { get; private set; }
        public RelayCommand NavigateBackCommand { get; private set; }
        public Room SourceRoom { get; set; }

        public ObservableCollection<Equipment> EquipmentCollection { get; set; }
        public ObservableCollection<Equipment> FilteredEquipment
        {
            get
            {
                List<Equipment> temp = new List<Equipment>();
                if (Filter != 0)
                {
                    if (Filter == 2)
                    {
                        temp.AddRange(EquipmentCollection.Where(x => x.EquipmentData.Type == EquipmentType.consumable));
                    }
                    else
                    {
                        temp.AddRange(EquipmentCollection.Where(x => x.EquipmentData.Type != EquipmentType.consumable));
                    }
                }
                else
                {
                    temp = new List<Equipment>(EquipmentCollection);
                }

                List<Equipment> result = new List<Equipment>();
                if (SearchText != null && SearchText != "")
                {

                    result.AddRange(temp.Where(x => x.EquipmentData.Name.ToUpper().StartsWith(SearchText.ToUpper())));
                }
                else
                {
                    result = temp;
                }
                return new ObservableCollection<Equipment>(result);
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                OnPropertyChanged("SearchText");
                OnPropertyChanged("FilteredEquipment");
            }
        }

        private int filter;
        public int Filter
        {
            get { return filter; }
            set
            {
                if (filter != value)
                {
                    filter = value;
                    OnPropertyChanged("Filter");
                    OnPropertyChanged("FilteredEquipment");
                }
            }
        }

        public RoomDetailsViewModel(Room room)
        {
            OpenRoomRelocationCommand = new RelayCommand(OpenRoomRelocation, CanRelocate);
            OpenWarehouseRelocationCommand = new RelayCommand(OpenWarehouseRelocation, CanRelocate);
            NavigateBackCommand = new RelayCommand(NavigateBack, null);

            Filter = 0;

            SourceRoom = room;
            EquipmentCollection = room.Equipment;
        }

        public void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public void OpenRoomRelocation(object obj)
        {
            List<Equipment> selectedEquipment = ((IList)obj).Cast<Equipment>().ToList();
            ManagerHome.NavigationService.Navigate(new RoomRelocationView(SourceRoom, selectedEquipment));
        }
        public void OpenWarehouseRelocation(object obj)
        {
            List<Equipment> selectedEquipment = ((IList)obj).Cast<Equipment>().ToList();
            ManagerHome.NavigationService.Navigate(new RoomWarehouseRelocationView(SourceRoom, selectedEquipment));
        }
        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new RoomView());
        }
        
        private bool CanRelocate(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            List<Equipment> selectedEquipment = ((IList)obj).Cast<Equipment>().ToList();
            if (selectedEquipment.Count > 0)
            {
                return true;
            }
            return false;
        }
    }
}
