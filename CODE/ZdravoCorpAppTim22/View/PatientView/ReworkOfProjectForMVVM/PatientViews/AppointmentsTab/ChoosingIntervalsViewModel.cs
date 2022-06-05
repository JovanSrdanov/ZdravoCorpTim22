using Controller;
using MVVM1;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.AppointmentsTab
{
    public class ChoosingIntervalsViewModel : ViewModel
    {
        public int Id { get; set; }
        private Interval selectedInterval { get; set; }
        public Interval SelectedInterval
        {
            get { return selectedInterval; }
            set
            {
                selectedInterval = value;

                UpdateMedicalAppointmentCommand.RaiseCanExecuteChanged();
            }
        }


        public ObservableCollection<Interval> Intervals { get; set; }
        public MyICommand UpdateMedicalAppointmentCommand { get; set; }

        public ChoosingIntervalsViewModel(int id, DateTime SelectedDate_)
        {
            Id = id;
            Intervals = new ObservableCollection<Interval>(MedicalAppointmentController.Instance.GetNewMedicalAppointments(id, SelectedDate_));
            UpdateMedicalAppointmentCommand = new MyICommand(UpdateMedicalAppointment, IsMedicalAppointmentIntervalSelected);

        }


        public void UpdateMedicalAppointment()
        {
          
            MedicalAppointmentController.Instance.ChangeToNew(Id, SelectedInterval);
          

        }

        public bool IsMedicalAppointmentIntervalSelected()
        {
            return SelectedInterval.Start != null;
        }
    }
}
