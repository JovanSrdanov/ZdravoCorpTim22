using System.ComponentModel;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments
{
    public partial class CustomTimePicker : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public int hours;
        public int Hours
        {
            get => hours;
            set {
                hours = value;
                OnPropertyChanged("Hours");
            }
        }
        public int minutes;
        public int Minutes
        {
            get => minutes;
            set
            {
                minutes = value;
                OnPropertyChanged("Minutes");
            }
        }

        public CustomTimePicker()
        {
            InitializeComponent();
            DataContext = this;
            for (int i = 0; i < 24; i++)
            {
                HourComboBox.Items.Add(i);
            }
            for (int i = 0; i < 60; i++)
            {
                MinuteComboBox.Items.Add(i);
            }
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
