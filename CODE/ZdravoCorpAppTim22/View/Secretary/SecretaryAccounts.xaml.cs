using System.Windows;

namespace ZdravoCorpAppTim22.View.Secretary
{
    public partial class SecretaryAccounts : Window
    {
        private SecretaryHome secretaryHome;

        public SecretaryAccounts(SecretaryHome secretaryHome)
        {
            InitializeComponent();
            this.secretaryHome = secretaryHome;
        }

        private void Doctor_RB_Checked(object sender, RoutedEventArgs e)
        {
            if (listBoxOfUsers != null)
            {
                listBoxOfUsers.Items.Clear();

                listBoxOfUsers.Items.Add("Doctor");
            }
        }

        private void Manager_RB_Checked(object sender, RoutedEventArgs e)
        {
            if (listBoxOfUsers != null)
            {
                listBoxOfUsers.Items.Clear();

                listBoxOfUsers.Items.Add("Manager");
            }
        }

        private void Patient_RB_Checked(object sender, RoutedEventArgs e)
        {
            if (listBoxOfUsers != null)
            {
                listBoxOfUsers.Items.Clear();

                listBoxOfUsers.Items.Add("Patient");
            }
        }

        private void Secretary_RB_Checked(object sender, RoutedEventArgs e)
        {
            if (listBoxOfUsers != null)
            {
                listBoxOfUsers.Items.Clear();

                listBoxOfUsers.Items.Add("Secretary");
            }
        }

        private void NewAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            SecretaryAccountsNew secretaryAccountsNew = new SecretaryAccountsNew(this);
            this.Hide();
        }

        private void EditAccountBtn_Click(object sender, RoutedEventArgs e)
        {
            if (listBoxOfUsers.SelectedIndex == -1)
            {
                MessageBox.Show("Must select account from list!");
                return;
            }
            SecretaryAccountsEdit secretaryAccountsEdit = new SecretaryAccountsEdit(this);
            this.Hide();
        }


        private void Window_Closed(object sender, System.EventArgs e)
        {
            secretaryHome.Show();
        }
    }
}
