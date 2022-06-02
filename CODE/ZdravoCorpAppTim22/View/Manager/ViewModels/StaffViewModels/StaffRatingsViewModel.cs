using Model;
using System.Collections.Generic;
using System.Diagnostics;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.View.Manager.Commands;
using ZdravoCorpAppTim22.View.Manager.Pages.StaffPages;

namespace ZdravoCorpAppTim22.View.Manager.ViewModels.StaffViewModels
{
    public class StaffRatingsViewModel
    {
        public RelayCommand NavigateBackCommand { get; private set; }
        public Doctor Doctor { get; private set; }

        public List<int> StaffGrades = new List<int>();
        public List<int> AccessibilityGrades = new List<int>();
        public List<int> HygieneGrades = new List<int>();
        public List<int> AppearanceGrades = new List<int>();
        public List<int> ApplicationGrades = new List<int>();

        public StaffRatingsViewModel(Doctor doctor)
        {
            NavigateBackCommand = new RelayCommand(NavigateBack);
            Doctor = doctor;
            
            
        }
        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new StaffView());
        }
    }
}
