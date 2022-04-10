using System.Text.RegularExpressions;
using System.Windows;
namespace ZdravoCorpAppTim22.View.Secretary
{
    public partial class SecretaryAccountsNew : Window
    {
        private SecretaryAccounts callerScreen;
        public SecretaryAccountsNew(SecretaryAccounts callerScreen)
        {
            InitializeComponent();
            this.callerScreen = callerScreen;
            this.Show();
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
                    break;
            }
        }

        private void ConfirmBtn_Click(object sender, RoutedEventArgs e)
        {
            if (NameTextBox.Text == "")
            {
                MessageBox.Show("Must enter name!");
                return;
            }

            if (SurnameTextBox.Text == "")
            {
                MessageBox.Show("Must enter surname!");
                return;
            }

            if (JMBGTextBox.Text == "")
            {
                MessageBox.Show("Must enter JMBG!");
                return;
            }

            if (PasswordTextBox.Password == "")
            {
                MessageBox.Show("Must enter password!");
                return;
            }

            Model.Gender genderTemp;

            if ((bool)MaleRB.IsChecked)
            {
                genderTemp = Model.Gender.male;
            }
            else
            {
                genderTemp = Model.Gender.female;
            }

            Model.Address addressTemp = new Model.Address();
            Model.DoctorSpecialisationType doctorSpecialisationType = new Model.DoctorSpecialisationType();

            Model.Doctor doctor = new Model.Doctor(NameTextBox.Text, SurnameTextBox.Text, EMailTextBox.Text, JMBGTextBox.Text, PasswordTextBox.Password, BirthDayPicker.DisplayDate, PhoneTextBox.Text, genderTemp, addressTemp, doctorSpecialisationType);

            MessageBoxResult result = MessageBox.Show("Are you sure?", "Confirm new account", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    MessageBox.Show("New Account created!");
                    break;
                case MessageBoxResult.No:
                    return;
                    break;
            }

            callerScreen.Show();
            this.Close();
        }

        private static readonly Regex _regex = new Regex("[^0-9.-]+");
        private static bool IsOnlyNumbersText(string text)
        {
            return !_regex.IsMatch(text);
        }
        private void JMBGTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsOnlyNumbersText(e.Text);
        }
    }
}
