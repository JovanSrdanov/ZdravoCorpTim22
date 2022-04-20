using Model;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ZdravoCorpAppTim22.View.Manager.Views.RoomAppointments
{
    public partial class SelectTimePage : Page
    {
        private Page PageParent { get; set; }
        public CustomDatePicker CustomDatePicker { get; set; }

        public SelectTimePage(Page parent, Room room)
        {
            InitializeComponent();
            PageParent = parent;
            CustomDatePicker = new CustomDatePicker(room);
            SelectTimeContent.Content = CustomDatePicker;
            CustomDatePicker.DateSelectedEvent += BackButton;
            CustomDatePicker.CancelEvent += BackButton;
        }
        public SelectTimePage(Page parent, Room room, DateTime start)
        {
            InitializeComponent();
            PageParent = parent;
            CustomDatePicker = new CustomDatePicker(room, start);
            SelectTimeContent.Content = CustomDatePicker;
            CustomDatePicker.DateSelectedEvent += BackButton;
            CustomDatePicker.CancelEvent += BackButton;
        }

        public void BackButton(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(PageParent);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(PageParent);
        }

    }
}
