using Model;
using System;
using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class AddRoomView : Page
    {
        public RoomAddViewModel ViewModel;

        public AddRoomView()
        {
            InitializeComponent();
            ViewModel = new RoomAddViewModel();
            DataContext = ViewModel;
            TypeComboBox.ItemsSource = Enum.GetValues(typeof(RoomType));
        }
    }
}
