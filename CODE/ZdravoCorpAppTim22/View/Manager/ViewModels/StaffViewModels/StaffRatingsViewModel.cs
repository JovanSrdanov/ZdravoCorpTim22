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

        public List<int> KindnessGrades = new List<int>();
        public List<int> ExpertiseGrades = new List<int>();
        public List<int> DiscretionGrades = new List<int>();

        public StaffRatingsViewModel(Doctor doctor)
        {
            NavigateBackCommand = new RelayCommand(NavigateBack);
            Doctor = doctor;

            foreach(MedicalReport report in MedicalReportController.Instance.GetAll())
            {
                if (report.DoctorId == doctor.Id && report.ReportReview != null)
                {
                    KindnessGrades.Add(report.ReportReview.DoctorKindness);
                    ExpertiseGrades.Add(report.ReportReview.DoctorExpertise);
                    DiscretionGrades.Add(report.ReportReview.DoctorDiscretion);
                }
            }
        }
        public void NavigateBack(object obj)
        {
            ManagerHome.NavigationService.Navigate(new StaffView());
        }
    }
}
