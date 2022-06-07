using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Controller;
using ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.RecordTab;

namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews.PresonalNoteTab
{

    public partial class CreatePersonalNotePageView : Page
    {
        public string Reason { get; set; }
        public CreatePersonalNotePageView(MedicalReportsViewModel medicalReportsViewModel)
        {
            Reason = "Anamneza:\n" + medicalReportsViewModel.Anamnesis + "\nKomentar:\n" + medicalReportsViewModel.ReportComment;
            InitializeComponent();

            ReasonTextBlock.Text = Reason;

            ComboBoxFreq.ItemsSource = new List<int>() { 1, 2, 3 };
            DatePickerEnd.SelectedDate=DateTime.Now;
            DatePickerEnd.DisplayDateStart = DateTime.Now;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            int frequency = (int)ComboBoxFreq.SelectedItem;
            string message = MessageTextBox.Text;
            DateTime EndOfShowing = (DateTime)DatePickerEnd.SelectedDate;
            DateTime HoursAndMinutes = (DateTime)TimePicker.Value;

            PersonalNoteController.Instance.MakeNote(frequency,message,Reason,EndOfShowing,HoursAndMinutes);

            NavigationService.Navigate(new SuccessPersonalNoteCreated());
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("osk.exe");
        }
    }
}
