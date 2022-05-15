using Model;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ZdravoCorpAppTim22.Model.Utility;
using ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels;
using ZdravoCorpAppTim22.View.Manager.Views;
using ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RoomDivergeView : Page
    {
        readonly RoomDivergeViewModel ViewModel;
        Point StartPoint = new Point();
        public RoomDivergeView(Room room)
        {
            InitializeComponent();
            ViewModel = new RoomDivergeViewModel(room);
            DataContext = ViewModel;

            TypeComboBox_1.ItemsSource = Enum.GetValues(typeof(RoomType));
            TypeComboBox_2.ItemsSource = Enum.GetValues(typeof(RoomType));
            ButtonSelectEndTime.IsEnabled = false;
            RoomEdit.IsEnabled = false;
            DataGridPanel.IsEnabled = false;
            DataGridEquipment_1.SelectedIndex = -1;
            DataGridEquipment_2.SelectedIndex = -1;
        }

        public void StartDateSelected(object sender, EventArgs e)
        {
            ViewModel.Interval = new Interval
            {
                Start = ((CustomDatePicker)sender).SelectedDate,
                End = ViewModel.Interval.End
            };

            if (DateTime.Compare(ViewModel.Interval.Start, new DateTime()) > 0)
            {
                SelectStartTimeContent.Content = ViewModel.Interval.Start.ToString();
                ButtonSelectEndTime.IsEnabled = true;
                RoomEdit.IsEnabled = false;
                DataGridPanel.IsEnabled = false;
                DataGridEquipment_1.SelectedIndex = -1;
                DataGridEquipment_2.SelectedIndex = -1;
                ViewModel.Interval = new Interval
                {
                    Start = ViewModel.Interval.Start,
                    End = new DateTime()
                };
                SelectEndTimeContent.Content = "";
            }
            else
            {
                ButtonSelectEndTime.IsEnabled = false;
            }
        }
        public void EndDateSelected(object sender, EventArgs e)
        {
            ViewModel.Interval = new Interval
            {
                Start = ViewModel.Interval.Start,
                End = ((CustomDatePicker)sender).SelectedDate

            };
            if (DateTime.Compare(ViewModel.Interval.End, new DateTime()) > 0)
            {
                SelectEndTimeContent.Content = ViewModel.Interval.End.ToString();
                DataGridPanel.IsEnabled = true;
                RoomEdit.IsEnabled = true;
            }
            else
            {
                RoomEdit.IsEnabled = false;
                DataGridPanel.IsEnabled = false;
            }
        }
        private void ButtonSelectStartTime_Click(object sender, RoutedEventArgs e)
        {
            var RenovateStartDateView = new SelectTimePage(this, ViewModel.Room);
            RenovateStartDateView.CustomDatePicker.DateSelectedEvent += StartDateSelected;
            this.NavigationService.Navigate(RenovateStartDateView);
        }
        private void ButtonSelectEndTime_Click(object sender, RoutedEventArgs e)
        {
            var RenovateEndDateView = new SelectTimePage(this, ViewModel.Room, ViewModel.Interval.Start);
            RenovateEndDateView.CustomDatePicker.DateSelectedEvent += EndDateSelected;
            this.NavigationService.Navigate(RenovateEndDateView);
        }


        #region drag&drop event handlers
        private void PreviewMouseLeftButtonDownHandler(object sender, MouseButtonEventArgs e)
        {
            StartPoint = e.GetPosition(null);
        }
        private void MouseMoveHandler(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = StartPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed && (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                DataGrid grid = sender as DataGrid;
                DataGridRow gridItem = ManagerHome.FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

                if (gridItem == null) return;
                Equipment equipment = (Equipment)grid.ItemContainerGenerator.ItemFromContainer(gridItem);

                DataObject dragData = new DataObject("equipmentFormat", equipment);
                DragDrop.DoDragDrop(gridItem, dragData, DragDropEffects.Move);
            }
        }
        private void DragOverHandler(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent("equipmentFormat") || e.Source == sender)
            {
                e.Effects = DragDropEffects.None;
            }
        }
        private void DataGridEquipment_1_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("equipmentFormat"))
            {
                Equipment equipment = e.Data.GetData("equipmentFormat") as Equipment;

                if (ViewModel.Equipment_1.Where(x => x == equipment).FirstOrDefault() == null)
                {
                    AmountModal amountModal = new AmountModal(0, equipment.Amount);
                    amountModal.Owner = ManagerHome.Instance;
                    amountModal.ShowDialog();
                    if(amountModal.IsCanceled == false && amountModal.Amount > 0)
                    {
                        ViewModel.AddToCollection(ViewModel.Equipment_2, ViewModel.Equipment_1, equipment, amountModal.Amount);
                    }
                }
            }
        }
        private void DataGridEquipment_2_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("equipmentFormat"))
            {
                Equipment equipment = e.Data.GetData("equipmentFormat") as Equipment;

                if (ViewModel.Equipment_2.Where(x => x == equipment).FirstOrDefault() == null)
                {
                    AmountModal amountModal = new AmountModal(0, equipment.Amount);
                    amountModal.Owner = ManagerHome.Instance;
                    amountModal.ShowDialog();
                    if (amountModal.IsCanceled == false && amountModal.Amount > 0)
                    {
                        ViewModel.AddToCollection(ViewModel.Equipment_1, ViewModel.Equipment_2, equipment, amountModal.Amount);
                    }
                }
            }
        }
        #endregion
    }
}
