using Model;
using System.ComponentModel;

namespace ZdravoCorpAppTim22.View.Manager.DataModel
{
    public class EquipmentDataModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public Equipment Equipment { get; set; }
        private int amount;
        public int Amount
        {
            get => amount;
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }

        public EquipmentDataModel(int amount, Equipment equipment)
        {
            Amount = amount;
            Equipment = equipment;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsAmountValid()
        {
            return amount > 0 && amount <= Equipment.Amount;
        }
    }
}
