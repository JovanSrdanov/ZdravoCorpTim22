using Model;
using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RoomDetailsView : Page
    {
        public RoomDetailsViewModel ViewModel { get; }
        public RoomDetailsView(Room room)
        {
            InitializeComponent();
            ViewModel = new RoomDetailsViewModel(room);
            DataContext = ViewModel;
        }
    }
}
