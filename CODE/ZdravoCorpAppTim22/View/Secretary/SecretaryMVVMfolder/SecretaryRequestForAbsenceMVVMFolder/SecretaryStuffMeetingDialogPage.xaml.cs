using Controller;
using Model;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;
using ZdravoCorpAppTim22.Model.Utility;

namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder.SecretaryRequestForAbsenceMVVMFolder
{
    /// <summary>
    /// Interaction logic for SecretaryStuffMeetingDialogPage.xaml
    /// </summary>
    public partial class SecretaryStuffMeetingDialogPage : Page
    {
        StuffMeeting StuffMeeting;
        List<Doctor> doctors = new List<Doctor>();
        List<SecretaryClass> secretaries = new List<SecretaryClass>();
        List<ManagerClass> managers = new List<ManagerClass>();
        public SecretaryStuffMeetingDialogPage(StuffMeeting stuffMeeting)
        {
            InitializeComponent();
            StuffMeeting = stuffMeeting;
            comboBoxRoom.ItemsSource = RoomController.Instance.GetAll();
            for (int i = Constants.Constants.WORK_DAY_START_TIME; i < Constants.Constants.WORK_DAY_END_TIME; i++)
            {
                comboBoxTimeStart.Items.Add(i + ":00");
                comboBoxTimeEnd.Items.Add(i + ":00");
            }
            comboBoxRoom.SelectedItem = StuffMeeting.Room;
            datePicker.SelectedDate = StuffMeeting.Interval.Start;
            comboBoxTimeStart.SelectedIndex = stuffMeeting.Interval.Start.Hour - 7;
            comboBoxTimeEnd.SelectedIndex = stuffMeeting.Interval.End.Hour - 7;
            dataGridInvited.Items.Clear();
            for (int i = 0; i < StuffMeeting.Doctors.Count; i++)
            {
                dataGridInvited.Items.Add(StuffMeeting.Doctors[i]);
            }
            for (int i = 0; i < StuffMeeting.Managers.Count; i++)
            {
                dataGridInvited.Items.Add(StuffMeeting.Managers[i]);
            }
            for (int i = 0; i < StuffMeeting.Secretaries.Count; i++)
            {
                dataGridInvited.Items.Add(StuffMeeting.Secretaries[i]);
            }
            dataGridAll.Items.Clear();
            for (int i = 0; i < DoctorController.Instance.GetAll().Count; i++)
            {
                if (!StuffMeeting.Doctors.Contains(DoctorController.Instance.GetAll()[i]))
                {
                    dataGridAll.Items.Add(DoctorController.Instance.GetAll()[i]);
                }
            }
            doctors = StuffMeeting.Doctors;
            managers = StuffMeeting.Managers;
            secretaries = StuffMeeting.Secretaries;
        }


        public bool CheckIfAllDataIsValid()
        {
            if (comboBoxRoom.SelectedItem == null)
            {
                MessageBox.Show("You must select room!");
                return false;
            }
            if (datePicker.SelectedDate == null)
            {
                if (datePicker.SelectedDate < System.DateTime.Now)
                {
                    MessageBox.Show("You must choose date of meeting which is in future");
                    return false;
                }
                MessageBox.Show("You must choose date of meeting");
                return false;
            }
            if (comboBoxTimeEnd.SelectedItem == null)
            {
                MessageBox.Show("You must start time of meeting");
                return false;
            }
            if (comboBoxTimeEnd.SelectedItem == null)
            {
                MessageBox.Show("You must end time of meeting");
                return false;
            }
            if (comboBoxTimeEnd.SelectedIndex <= comboBoxTimeStart.SelectedIndex)
            {
                MessageBox.Show("Start time must be before end time!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (dataGridInvited.Items.Count < 2)
            {
                MessageBox.Show("Meeting must have 2 or more participants");
                return false;
            }

            return true;
        }

        private void btnMakeAppointment_Click(object sender, RoutedEventArgs e)
        {
            if (CheckIfAllDataIsValid())
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Update this meeting", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    Interval interval = new Interval();
                    interval.Start = datePicker.SelectedDate.Value.AddHours(comboBoxTimeStart.SelectedIndex + 7);
                    interval.End = datePicker.SelectedDate.Value.AddHours(comboBoxTimeEnd.SelectedIndex + 7);
                    StuffMeeting.Interval = interval;
                    StuffMeeting.Room = (Room)comboBoxRoom.SelectedItem;
                    StuffMeeting.Doctors = doctors;
                    StuffMeeting.Managers = managers;
                    StuffMeeting.Secretaries = secretaries;
                    StuffMeetingController.Instance.Update(StuffMeeting);

                    NavigationService.GoBack();
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Cancel edit of this meeting", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                NavigationService.GoBack();
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

        private void btnUnInvite_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridInvited.SelectedItem != null)
            {
                try
                {
                    doctors.Remove((Doctor)dataGridInvited.SelectedItem);
                    if (rbDoctors.IsChecked == true)
                    {
                        dataGridAll.Items.Add(dataGridInvited.SelectedItem);
                    }

                }
                catch { }

                try
                {
                    managers.Remove((ManagerClass)dataGridInvited.SelectedItem);
                    if (rbManagers.IsChecked == true)
                    {
                        dataGridAll.Items.Add(dataGridInvited.SelectedItem);
                    }

                }
                catch { }

                try
                {
                    secretaries.Remove((SecretaryClass)dataGridInvited.SelectedItem);
                    if (rbSecretary.IsChecked == true)
                    {
                        dataGridAll.Items.Add(dataGridInvited.SelectedItem);
                    }

                }
                catch { }
                dataGridInvited.Items.Remove(dataGridInvited.SelectedItem);
            }
        }
    }
}
