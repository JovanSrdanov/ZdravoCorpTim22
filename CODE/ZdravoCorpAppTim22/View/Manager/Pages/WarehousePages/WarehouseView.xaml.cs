using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels.WarehouseViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.WarehousePages
{
    public partial class WarehouseView : Page
    {
        readonly WarehouseViewModel ViewModel;
        public WarehouseView()
        {
            InitializeComponent();
            ViewModel = new WarehouseViewModel();
            DataContext = ViewModel;
        }
    }
}
