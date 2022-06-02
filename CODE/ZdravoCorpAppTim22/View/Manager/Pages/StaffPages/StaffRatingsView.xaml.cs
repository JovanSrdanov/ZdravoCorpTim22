using Model;
using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels.StaffViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.StaffPages
{
    public partial class StaffRatingsView : Page
    {
        readonly StaffRatingsViewModel ViewModel;
        public StaffRatingsView(Doctor doctor)
        {
            InitializeComponent();
            ViewModel = new StaffRatingsViewModel(doctor);
            DataContext = ViewModel;
        }
    }
}
