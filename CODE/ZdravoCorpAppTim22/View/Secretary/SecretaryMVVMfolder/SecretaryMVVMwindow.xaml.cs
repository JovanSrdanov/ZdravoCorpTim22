using System;
using System.Windows;

namespace ZdravoCorpAppTim22.View.Secretary.SecretaryMVVMfolder
{
    /// <summary>
    /// Interaction logic for SecretaryMVVMwindow.xaml
    /// </summary>
    public partial class SecretaryMVVMwindow : Window
    {
        private SecretaryHome secretaryHome;
        public SecretaryMVVMwindow(SecretaryHome secretaryHome)
        {
            InitializeComponent();
            this.secretaryHome = secretaryHome;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            secretaryHome.Show();
            this.Close();
        }
    }
}
