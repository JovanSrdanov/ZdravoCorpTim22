using Controller;
using Model;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using ZdravoCorpAppTim22.Controller;
using ZdravoCorpAppTim22.Model;

namespace ZdravoCorpAppTim22.View.Secretary
{
    public partial class SecretaryAccountsNew : Window
    {
        private SecretaryAccounts callerScreen;
        private MedicalRecord medicalRecordTemp;
        public SecretaryAccountsNew(SecretaryAccounts callerScreen)
        {
            InitializeComponent();
            this.callerScreen = callerScreen;
            this.Show();
            SetSpecialisationComboBox(Visibility.Visible);
            SetMedicalReportButton(Visibility.Hidden);
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            callerScreen.Show();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure?", "Cancel new account", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    callerScreen.Show();
                    this.Close();
                    break;
                case MessageBoxResult.No:
                    return;
            }
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (EMailTextBox.Text == "")
            {
                MessageBox.Show("Must enter unique email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ObservableCollection<Patient> patients = new ObservableCollection<Patient>(PatientController.Instance.GetAll());
                for (int i = 0; i < patients.Count; i++)
                {
                    if (EMailTextBox.Text == patients[i].Email)
                    {
                        MessageBox.Show("Must enter unique email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                ObservableCollection<Doctor> doctors = new ObservableCollection<Doctor>(DoctorController.Instance.GetAll());
                for (int i = 0; i < doctors.Count; i++)
                {
                    if (EMailTextBox.Text == doctors[i].Email)
                    {
                        MessageBox.Show("Must enter unique email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                ObservableCollection<ManagerClass> managers = new ObservableCollection<ManagerClass>(ManagerController.Instance.GetAll());
                for (int i = 0; i < managers.Count; i++)
                {
                    if (EMailTextBox.Text == managers[i].Email)
                    {
                        MessageBox.Show("Must enter unique email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                ObservableCollection<SecretaryClass> secretaries = new ObservableCollection<SecretaryClass>(SecretaryController.Instance.GetAll());
                for (int i = 0; i < secretaries.Count; i++)
                {
                    if (EMailTextBox.Text == secretaries[i].Email)
                    {
                        MessageBox.Show("Must enter unique email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
            }

            if (NameTextBox.Text == "")
            {
                MessageBox.Show("Must enter name!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                NameTextBox.Focus();
                return;
            }

            if (SurnameTextBox.Text == "")
            {
                MessageBox.Show("Must enter surname!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                SurnameTextBox.Focus();
                return;
            }

            if (JMBGTextBox.Text == "")
            {
                MessageBox.Show("Must enter JMBG!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                JMBGTextBox.Focus();
                return;
            }

            if (PasswordTextBox.Password == "")
            {
                MessageBox.Show("Must enter password!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                PasswordTextBox.Focus();
                return;
            }

            Gender genderTemp;
            {
                if ((bool)MaleRB.IsChecked)
                {
                    genderTemp = Gender.male;
                }
                else if ((bool)FemaleRB.IsChecked)
                {
                    genderTemp = Gender.female;
                }
                else
                {
                    genderTemp = Gender.other;
                }
            } //Getting gender

            Address addressTemp = new Address();

            if (AddressTextBox.Text != "")
            {
                int counter = 0;
                for (int i = 0; i < AddressTextBox.Text.Length; i++)
                {
                    if (AddressTextBox.Text[i] == '/')
                    {
                        counter++;
                    }

                }

                if (counter == 3)
                {

                    string[] tokens = AddressTextBox.Text.Split('/');
                    addressTemp.Street = tokens[0];
                    addressTemp.Number = tokens[1];
                    addressTemp.City = tokens[2];
                    addressTemp.Country = tokens[3];

                }
                else
                {
                    MessageBox.Show("Address must be in format Street/Number/City/Country", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    AddressTextBox.Focus();
                    return;
                }
            }

            AddressController.Instance.Create(addressTemp);

            //Doctor account
            if ((bool)DoctorRB.IsChecked)
            {
                DoctorSpecialization doctorSpecialisationType;
                if (SpecialisationComboBox.SelectedItem != null)
                {
                    doctorSpecialisationType = (DoctorSpecialization)SpecialisationComboBox.SelectedItem;
                }
                else
                {
                    MessageBox.Show("Must choose specialisation");
                    return;
                }
                Doctor doctor = new Doctor(NameTextBox.Text, SurnameTextBox.Text, EMailTextBox.Text, JMBGTextBox.Text, PasswordTextBox.Password, BirthDayPicker.DisplayDate, PhoneTextBox.Text, genderTemp, addressTemp, doctorSpecialisationType, null);

                MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm new account", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MessageBox.Show("New Account created!");
                        DoctorController.Instance.Create(doctor);
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }

            //Patient account
            if ((bool)PatientRB.IsChecked)
            {
                Patient patient = new Patient(NameTextBox.Text, SurnameTextBox.Text, EMailTextBox.Text, JMBGTextBox.Text, PasswordTextBox.Password, BirthDayPicker.DisplayDate, PhoneTextBox.Text, genderTemp, addressTemp, null, null);
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm new account", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MessageBox.Show("New Account created!");
                        if (medicalRecordTemp == null)
                        {
                            medicalRecordTemp = new MedicalRecord();
                            MedicalRecordController.Instance.Create(medicalRecordTemp);
                        }

                        patient.MedicalRecord = medicalRecordTemp;

                        PatientController.Instance.Create(patient);
                        PatientController.Instance.GetPatient(patient).MedicalRecord.Patient = PatientController.Instance.GetPatient(patient);

                        PatientController.Instance.Update(PatientController.Instance.GetPatient(patient));
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }

            //Manager account
            if ((bool)ManagerRB.IsChecked)
            {
                ManagerClass manager = new ManagerClass(NameTextBox.Text, SurnameTextBox.Text, EMailTextBox.Text, JMBGTextBox.Text, PasswordTextBox.Password, BirthDayPicker.DisplayDate, PhoneTextBox.Text, genderTemp, addressTemp);
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm new account", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MessageBox.Show("New Account created!");
                        ManagerController.Instance.Create(manager);
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }

            //Secretary account
            if ((bool)SecretaryRB.IsChecked)
            {
                SecretaryClass secretary = new SecretaryClass(NameTextBox.Text, SurnameTextBox.Text, EMailTextBox.Text, JMBGTextBox.Text, PasswordTextBox.Password, BirthDayPicker.DisplayDate, PhoneTextBox.Text, genderTemp, addressTemp);
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm new account", MessageBoxButton.YesNo);
                switch (result)
                {
                    case MessageBoxResult.Yes:
                        MessageBox.Show("New Account created!");
                        SecretaryController.Instance.Create(secretary);
                        break;
                    case MessageBoxResult.No:
                        return;
                }
            }

            callerScreen.Show();
            this.Close();
        }



        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsOnlyNumbersText(string text)
        {
            return _regex.IsMatch(text);
        }
        private void JMBGTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = IsOnlyNumbersText(e.Text);
        }

        private void PhoneTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {

            e.Handled = IsOnlyNumbersText(e.Text);
        }

        public void SetSpecialisationComboBox(Visibility visible)
        {
            if (SpecialisationComboBox != null && SpecialisationLbl != null)
            {
                if (visible == Visibility.Visible)
                {
                    SpecialisationComboBox.ItemsSource = DoctorSpecializationController.Instance.GetAll();
                }
                SpecialisationComboBox.Visibility = visible;
                SpecialisationLbl.Visibility = visible;
            }
        }
        public void SetMedicalReportButton(Visibility visible)
        {
            if (MedicalRecordBtn != null)
            {
                MedicalRecordBtn.Visibility = visible;
            }
        }
        private void DoctorRB_Checked(object sender, RoutedEventArgs e)
        {
            SetSpecialisationComboBox(Visibility.Visible);
            SetMedicalReportButton(Visibility.Hidden);
        }

        private void PatientRB_Checked(object sender, RoutedEventArgs e)
        {
            SetSpecialisationComboBox(Visibility.Hidden);
            SetMedicalReportButton(Visibility.Visible);

        }

        private void SecretaryRB_Checked(object sender, RoutedEventArgs e)
        {
            SetSpecialisationComboBox(Visibility.Hidden);
            SetMedicalReportButton(Visibility.Hidden);

        }

        private void ManagerRB_Checked(object sender, RoutedEventArgs e)
        {
            SetSpecialisationComboBox(Visibility.Hidden);
            SetMedicalReportButton(Visibility.Hidden);

        }

        private void MedicalRecordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (medicalRecordTemp == null)
            {
                medicalRecordTemp = new MedicalRecord();
            }
            SecretaryAccountsMedicalRecord secretaryAccountsMedicalRecord = new SecretaryAccountsMedicalRecord(medicalRecordTemp);
            secretaryAccountsMedicalRecord.ShowDialog();
        }
    }
}
