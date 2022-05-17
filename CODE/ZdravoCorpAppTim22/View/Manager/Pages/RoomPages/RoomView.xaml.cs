using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels.RoomViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RoomView : Page
    {
        public RoomViewModel ViewModel;
        public RoomView()
        {
            InitializeComponent();
            ViewModel = new RoomViewModel();
            DataContext = ViewModel;
        }
    }
}
