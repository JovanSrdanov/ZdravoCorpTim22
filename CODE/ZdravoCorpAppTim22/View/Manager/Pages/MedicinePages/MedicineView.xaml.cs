using System.Windows.Controls;
using ZdravoCorpAppTim22.View.Manager.ViewModels.MedicineViewModels;

namespace ZdravoCorpAppTim22.View.Manager.Pages.MedicinePages
{
    public partial class MedicineView : Page
    {
        readonly MedicineViewModel ViewModel;
        public MedicineView()
        {
            InitializeComponent();
            ViewModel = new MedicineViewModel();
            DataContext = ViewModel;
        }
    }
}
