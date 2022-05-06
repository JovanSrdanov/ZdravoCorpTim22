using System.ComponentModel;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments
{
    public partial class CustomTimePicker : UserControl, INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
