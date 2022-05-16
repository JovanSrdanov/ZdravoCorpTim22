using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.Manager.Views
{
    public partial class AmountModal : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly int Min;
        private readonly int Max;

        public bool IsCanceled { get; private set; }

        private int amount;
        public int Amount
        {
            get => amount;
            set
            {
                if (value < Min)
                {
                    amount = Min;
                    OnPropertyChanged("Amount");
                }
                else if (value > Max)
                {
                    amount = Max;
                    OnPropertyChanged("Amount");
                }
                else
                {
                    amount = value;
                    OnPropertyChanged("Amount");
                }
            }
        }
        public AmountModal(int min, int max)
        {
            InitializeComponent();
            DataContext = this;
            Min = min;
            Max = max;
            Slider.Minimum = min;
            Slider.Maximum = max;
            IsCanceled = false;
        }

        private void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ButtonConfirm_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            IsCanceled = true;
            Close();
        }
    }
}
