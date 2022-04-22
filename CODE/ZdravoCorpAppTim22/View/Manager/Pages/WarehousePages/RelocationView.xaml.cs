using Model;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages
{
    public partial class RelocationView : Page
    {
        readonly WarehouseView ParentPage;
        public RelocationView(WarehouseView parent)
        {
            InitializeComponent();
            ParentPage = parent;
            foreach (Equipment eq in parent.SelectedEquipment)
            {
                Debug.WriteLine(eq.Name);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(ParentPage);
        }
    }
}
