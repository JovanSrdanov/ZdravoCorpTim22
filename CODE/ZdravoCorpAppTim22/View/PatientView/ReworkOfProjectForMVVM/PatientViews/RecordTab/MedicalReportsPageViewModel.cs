using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVM1;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab
{
    public class MedicalReportsPageViewModel : ViewModel
    {
        public ObservableCollection<MedicalReportsViewModel> MedicalReportsViewModels { get; set; }


        public MyICommand CommentReportCommand { get; set; }
        public MyICommand ReviewReportCommand { get; set; }


        private MedicalReportsViewModel selectedMedicalReportViewModel;
        public MedicalReportsViewModel SelectedMedicalReportViewModel
        {
            get { return selectedMedicalReportViewModel; }
            set
            {
                selectedMedicalReportViewModel = value;
                CommentReportCommand.RaiseCanExecuteChanged();
                ReviewReportCommand.RaiseCanExecuteChanged();
            }
        }

        public MedicalReportsPageViewModel(ObservableCollection<MedicalReportsViewModel> medicalReportsViewModels)
        {
            MedicalReportsViewModels = medicalReportsViewModels;
            CommentReportCommand = new MyICommand(null, IsMedicalReportSelected);

            ReviewReportCommand = new MyICommand(null, IsMedicalReportSelectedAndNotReviewed);

        }
        public bool IsMedicalReportSelected()
        {
            return SelectedMedicalReportViewModel != null;

        }

        public bool IsMedicalReportSelectedAndNotReviewed()
        {
            return IsMedicalReportSelected() && SelectedMedicalReportViewModel.ReportReviewed == false;
        }

    }
}
