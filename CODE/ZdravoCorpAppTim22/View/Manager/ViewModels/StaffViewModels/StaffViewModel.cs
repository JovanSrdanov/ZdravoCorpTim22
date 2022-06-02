using Controller;
using Model;
using System.Collections.ObjectModel;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.StaffPages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.StaffViewModels
{
    public class StaffViewModel
    {
        public ObservableCollection<Doctor> StaffList { get; set; }
        public RelayCommand OpenRatingsCommand { get; private set; }
        
        public StaffViewModel()
        {
            StaffList = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
            OpenRatingsCommand = new RelayCommand(OpenRatings, IsSelected);
        }

        public void OpenRatings(object obj)
        {
            ManagerHome.NavigationService.Navigate(new StaffRatingsView((Doctor)obj));
        }

        private bool IsSelected(object obj)
        {
            Doctor doctor = (Doctor)obj;
            if (doctor == null)
            {
                return false;
            }
            return true;
        }
    }
}
