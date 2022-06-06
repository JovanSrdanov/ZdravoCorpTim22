using Model;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab
{

    public partial class ProfileView : Page
    {
        public ProfileView()
        {
            InitializeComponent();
            ProfileViewModel profileViewModel = new ProfileViewModel((Patient)AuthenticationController.Instance.GetLoggedUser());
            this.DataContext = profileViewModel;
        }
    }
}
