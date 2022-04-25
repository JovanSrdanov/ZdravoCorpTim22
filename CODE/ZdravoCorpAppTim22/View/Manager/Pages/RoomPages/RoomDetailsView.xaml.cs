using Model;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.Manager.Pages.RoomPages
{
    public partial class RoomDetailsView : Page
    {
        public RoomDetailsView(Room room)
        {
            InitializeComponent();
            MessageBox.Show(EquipmentController.Instance.GetRoomEquipment(room.Id).Count.ToString());
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
