using Model;
using MVVM1;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.Views;

namespace ZdravoCorpAppTim22.View.DoctorView.Novi_MVVM.ViewModels
{
    public class RequestListViewModel : ViewModel
    {
        private NavigationService navigationService;
        public ObservableCollection<RequestViewModel> allRequests { get; set; }

        private RequestViewModel selectedRequest;
        public RequestViewModel SelectedRequestBackup { get; set; }

        public RequestViewModel SelectedRequest
        {
            get { return selectedRequest; }
            set
            {
                selectedRequest = value;
                if (value != null)
                {
                    SelectedRequestBackup = value;
                }
                DetailsCommand.RaiseCanExecuteChanged();
            }
        }

        //KONSTRUKTOR
        public RequestListViewModel(NavigationService navigationService)
        {
            this.navigationService = navigationService;
            allRequests = new ObservableCollection<RequestViewModel>(GenerateRequests());

            BackCommand = new MyICommand(ExecuteBackCommand);
            DetailsCommand = new MyICommand(ExecuteDetailsCommand, CanExecuteDetailsCommand);
        }

        private List<RequestViewModel> GenerateRequests()
        {
            List<RequestViewModel> tempList = new List<RequestViewModel>();

            Doctor _loggedInDoctor = (Doctor)AuthenticationController.Instance.GetLoggedUser();
            List<Model.RequestForAbsence> requests = RequestForAbsenceController.Instance.GetAll();
            
            foreach (var request in requests)
            {
                if (request.Doctor.Id == _loggedInDoctor.Id)
                {
                    tempList.Add(new RequestViewModel(request));
                }
            }

            return tempList;
        }

        //KOMANDE
        public MyICommand DetailsCommand { get; set; }
        public MyICommand BackCommand { get; set; }

        private void ExecuteDetailsCommand()
        {
            RequestDeniedViewModel requestDeniedViewModel = new RequestDeniedViewModel(navigationService);
            RequestDenied requestDenied = new RequestDenied(requestDeniedViewModel);
            this.navigationService.Navigate(requestDenied);
            SelectedRequest = null;
            SelectedRequestBackup = null;
        }

        private bool CanExecuteDetailsCommand()
        {
            if (SelectedRequest == null)
            {
                return false;
            }
            else if (SelectedRequest.RequestState != RequestState.rejected)
            {
                return false;
            }
            return true;
        }

        private void ExecuteBackCommand()
        {
            this.navigationService.GoBack();
        }
    }
}
