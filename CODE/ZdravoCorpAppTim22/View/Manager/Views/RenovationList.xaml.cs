using Model;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Views
{
    public partial class RenovationList : UserControl
    {
        public RenovationViewModel ViewModel;
        public Appointment SelectedAppointment;
        public event EventHandler ListSelectionChanged;

        public RenovationList(Room room, DateTime date)
        {
            InitializeComponent();
            ViewModel = new RenovationViewModel(room, date);
            DataContext = ViewModel;
        }

        public RenovationList(Room room, DateTime date, DateTime startDate)
        {
            InitializeComponent();
            ViewModel = new RenovationViewModel(room, date, startDate);
            DataContext = ViewModel;
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SelectedAppointment = (Appointment)DataGrid.SelectedItem;
            ListSelectionChanged(this, EventArgs.Empty);
        }
    }
}
