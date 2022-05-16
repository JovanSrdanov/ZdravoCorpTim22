using System.Windows;

namespace ZdravoCorpAppTim22.View.Manager.Views
{
    public partial class InfoModal : Window
    {
        public string InfoText { get; private set; }
        private InfoModal(string infoText)
        {
            InitializeComponent();
            DataContext = this;
            InfoText = infoText;
        }

        public static void Show(string message)
        {
            InfoModal infoModal = new InfoModal(message)
            {
                Owner = ManagerHome.Instance
            };
            infoModal.ShowDialog();
        }

        private void CloseEvent(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
