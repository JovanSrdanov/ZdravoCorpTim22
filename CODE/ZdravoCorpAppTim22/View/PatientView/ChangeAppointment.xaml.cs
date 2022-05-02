using Model;
using System;
using System.Windows;

namespace ZdravoCorpAppTim22.View.PatientView
{

    public partial class ChangeAppointment : Window
    {

        public static DateTime selectedDateTime;
        public ChangeAppointment()
        {
            MedicalAppointment medicalAppointmentToChange = ZdravoCorpTabs.MedicalAppointmentSelected;
            InitializeComponent();

            SelectedAppointmentDoctor.Content = "Lekar: " + medicalAppointmentToChange.doctor.Name + " " + medicalAppointmentToChange.doctor.Surname;
            SelectedAppointmentRoom.Content = "Šifra sobe: " + medicalAppointmentToChange.room.Id;
            SelectedAppointmentDate.Content = "Originalni datum: " + medicalAppointmentToChange.Interval.Start.ToString("dd.MM.yyyy");
            SelectedAppointmentStartTime.Content = "Početak termina: " + medicalAppointmentToChange.Interval.Start.ToString("HH:mm");
            SelectedAppointmentEndTime.Content = "Kraj termina: " + medicalAppointmentToChange.Interval.End.ToString("HH:mm");

            DatePickerChangeAppoinment.DisplayDateStart = medicalAppointmentToChange.Interval.Start.Date.AddDays(-2);
            if (DateTime.Now.AddDays(4).Date > medicalAppointmentToChange.Interval.Start.Date)
            {

                DatePickerChangeAppoinment.DisplayDateStart = medicalAppointmentToChange.Interval.Start.Date.AddDays(-1);
            }
            if (DateTime.Now.AddDays(3).Date > medicalAppointmentToChange.Interval.Start.Date)
            {

                DatePickerChangeAppoinment.DisplayDateStart = medicalAppointmentToChange.Interval.Start.Date;
            }
            DatePickerChangeAppoinment.SelectedDate = medicalAppointmentToChange.Interval.Start.Date;
            DatePickerChangeAppoinment.DisplayDateEnd = medicalAppointmentToChange.Interval.Start.Date.AddDays(2);

        }



        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ChooseChangeAppointment_Click(object sender, RoutedEventArgs e)
        {
            Close();
            selectedDateTime = (DateTime)DatePickerChangeAppoinment.SelectedDate;
            ChoosingChangeAppointment choosingChangeAppointment = new ChoosingChangeAppointment();
            choosingChangeAppointment.ShowDialog();
        }
    }
}
