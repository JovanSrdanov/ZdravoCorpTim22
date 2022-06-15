using Controller;
using Model;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder.SecretaryRequestForAbsenceMVVMFolder
{
    /// <summary>
    /// Interaction logic for SecretaryNewStuffMeetingPage.xaml
    /// </summary>
    public partial class SecretaryNewStuffMeetingPage : Page
    {
        List<Doctor> doctors = new List<Doctor>();
        List<SecretaryClass> secretaries = new List<SecretaryClass>();
        List<ManagerClass> managers = new List<ManagerClass>();
        public SecretaryNewStuffMeetingPage()
        {
            InitializeComponent();
            for (int i = Constants.Constants.WORK_DAY_START_TIME; i < Constants.Constants.WORK_DAY_END_TIME; i++)
            {
                comboBoxTimeStart.Items.Add(i + ":00");
                comboBoxTimeEnd.Items.Add(i + ":00");
            }
            comboBoxRoom.ItemsSource = RoomController.Instance.GetAll();
        }

        public bool CheckIfAllDataIsValid()
        {
            if (comboBoxRoom.SelectedItem == null)
            {
                MessageBox.Show("You must select room!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (datePicker.SelectedDate == null)
            {
                MessageBox.Show("You must choose date of meeting", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (datePicker.SelectedDate <= System.DateTime.Now)
            {
                MessageBox.Show("You must choose date of meeting which is in future", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (comboBoxTimeEnd.SelectedItem == null)
            {
                MessageBox.Show("You must start time of meeting", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (comboBoxTimeEnd.SelectedItem == null)
            {
                MessageBox.Show("You must end time of meeting", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if(comboBoxTimeEnd.SelectedIndex<= comboBoxTimeStart.SelectedIndex)
            {
                MessageBox.Show("Start time must be before end time!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (dataGridInvited.Items.Count < 2)
            {
                MessageBox.Show("Meeting must have 2 or more participants", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void btnMakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfAllDataIsValid())
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Create this meeting", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    Random r = new Random();
                    int id = r.Next(100000);
                    Interval interval = new Interval();
                    interval.Start = datePicker.SelectedDate.Value.AddHours(comboBoxTimeStart.SelectedIndex + 7);
                    interval.End = datePicker.SelectedDate.Value.AddHours(comboBoxTimeEnd.SelectedIndex + 7);
                    StuffMeeting stuffMeeting = new StuffMeeting(id, interval, (Room)comboBoxRoom.SelectedItem, secretaries, doctors, managers);
                    StuffMeetingController.Instance.Create(stuffMeeting);

                    NavigationService.GoBack();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete this meeting", MessageBoxButton.YesNo, MessageBoxImage.Warning, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
            }
        }

        private void rbDoctors_Checked(object sender, RoutedEventArgs e)
        {
            dataGridAll.Items.Clear();
            for (int i = 0; i < DoctorController.Instance.GetAll().Count; i++)
            {
                if (!doctors.Contains(DoctorController.Instance.GetAll()[i]))
                {
                    dataGridAll.Items.Add(DoctorController.Instance.GetAll()[i]);
                }
            }
        }

        private void btnUnInvite_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridInvited.SelectedItem != null)
            {
                try
                {
                    if (rbDoctors.IsChecked == true)
                    {
                        dataGridAll.Items.Add(dataGridInvited.SelectedItem);
                    }
                    doctors.Remove((Doctor)dataGridInvited.SelectedItem);

                }
                catch { }

                try
                {
                    if (rbManagers.IsChecked == true)
                    {
                        dataGridAll.Items.Add(dataGridInvited.SelectedItem);
                    }
                    managers.Remove((ManagerClass)dataGridInvited.SelectedItem);

                }
                catch { }

                try
                {
                    if (rbSecretary.IsChecked == true)
                    {
                        dataGridAll.Items.Add(dataGridInvited.SelectedItem);
                    }
                    secretaries.Remove((SecretaryClass)dataGridInvited.SelectedItem);

                }
                catch { }
                dataGridInvited.Items.Remove(dataGridInvited.SelectedItem);
            }
        }

        private void rbManagers_Checked(object sender, RoutedEventArgs e)
        {
            dataGridAll.Items.Clear();
            for (int i = 0; i < ManagerController.Instance.GetAll().Count; i++)
            {
                if (!managers.Contains(ManagerController.Instance.GetAll()[i]))
                {
                    dataGridAll.Items.Add(ManagerController.Instance.GetAll()[i]);
                }
            }
        }

        private void rbSecretary_Checked(object sender, RoutedEventArgs e)
        {
            dataGridAll.Items.Clear();
            for (int i = 0; i < SecretaryController.Instance.GetAll().Count; i++)
            {
                if (!secretaries.Contains(SecretaryController.Instance.GetAll()[i]))
                {
                    dataGridAll.Items.Add(SecretaryController.Instance.GetAll()[i]);
                }
            }
        }

        private void btnInvite_Click(object sender, RoutedEventArgs e)
        {
            if (rbDoctors.IsChecked == true)
            {
                if (dataGridAll.SelectedItem != null)
                {
                    doctors.Add((Doctor)dataGridAll.SelectedItem);
                    dataGridInvited.Items.Add(dataGridAll.SelectedItem);
                    dataGridAll.Items.Remove(dataGridAll.SelectedItem);
                }
            }
            else if (rbManagers.IsChecked == true)
            {
                if (dataGridAll.SelectedItem != null)
                {
                    managers.Add((ManagerClass)dataGridAll.SelectedItem);
                    dataGridInvited.Items.Add(dataGridAll.SelectedItem);
                    dataGridAll.Items.Remove(dataGridAll.SelectedItem);
                }
            }
            else if (rbSecretary.IsChecked == true)
            {
                if (dataGridAll.SelectedItem != null)
                {
                    secretaries.Add((SecretaryClass)dataGridAll.SelectedItem);
                    dataGridInvited.Items.Add(dataGridAll.SelectedItem);
                    dataGridAll.Items.Remove(dataGridAll.SelectedItem);
                }
            }
        }
    }
}
