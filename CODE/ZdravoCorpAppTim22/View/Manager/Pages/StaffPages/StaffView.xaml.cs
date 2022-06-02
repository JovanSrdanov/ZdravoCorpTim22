using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels.StaffViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.StaffPages
{
    public partial class StaffView : Page
    {
        public StaffViewModel ViewModel;
        public StaffView()
        {
            InitializeComponent();
            ViewModel = new StaffViewModel();
            DataContext = ViewModel;
        }
    }
}
