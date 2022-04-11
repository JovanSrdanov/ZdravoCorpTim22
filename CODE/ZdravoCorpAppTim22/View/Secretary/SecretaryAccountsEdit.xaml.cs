using System.Windows;

namespace ZdravoCorpAppTim22.View.Secretary
{
    public partial class SecretaryAccountsEdit : Window
    {
        private SecretaryAccounts callerScreen;
        public SecretaryAccountsEdit(SecretaryAccounts callerScreen)
        {
            InitializeComponent();
            this.callerScreen = callerScreen;
            this.Show();
        }

        private void Window_Closed_1(object sender, System.EventArgs e)
        {
            callerScreen.Show();
        }
    }
}
