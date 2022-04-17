using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZdravoCorpAppTim22.View.Manager.Views
{
    public partial class CustomTimePicker : UserControl, INotifyPropertyChanged
    {
        public CustomTimePicker()
        {
            InitializeComponent();
            DataContext = this;
            for (int i = 0; i < 24; i++)
            {
                HourComboBox.Items.Add(i.ToString());
            }
            for (int i = 0; i < 60; i++)
            {
                MinuteComboBox.Items.Add(i.ToString());
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
