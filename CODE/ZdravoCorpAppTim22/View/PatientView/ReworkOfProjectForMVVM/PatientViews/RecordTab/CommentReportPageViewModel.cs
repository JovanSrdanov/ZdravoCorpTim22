using MVVM1;
using ZdravoCorpAppTim22.Controller;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab
{
    internal class CommentReportPageViewModel : ViewModel
    {
        public int Id { get; set; }
        private string reportComment { get; set; }
        public string ReportComment
        {
            get { return reportComment; }
            set
            {
                reportComment = value;
                OnPropertyChanged();
            }
        }

        public MyICommand SubmitCommentCommand { get; set; }

        public CommentReportPageViewModel(int id, string reportComment)
        {
            Id = id;
            ReportComment = reportComment;
            SubmitCommentCommand = new MyICommand(SubmitComment,null);

        }

        public void SubmitComment()
        {
            MedicalReportController.Instance.CommentTheReport(Id, ReportComment);

        }

    }
}