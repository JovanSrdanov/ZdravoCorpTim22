using System.Windows;

namespace ZdravoCorpAppTim22.View.Manager.Views
{

    public partial class ConfirmModal : Window
    {
        public string InfoText { get; private set; }
        public bool IsConfirmed { get; private set; }
        private ConfirmModal(string infoText)
        {
            InitializeComponent();
            DataContext = this;
            InfoText = infoText;
            IsConfirmed = false;
        }

        public static bool Show(string message)
        {
            ConfirmModal confirmModal = new ConfirmModal(message)
            {
                Owner = ManagerHome.Instance
            };
            confirmModal.ShowDialog();

            return confirmModal.IsConfirmed;
        }

        private void ConfirmEvent(object sender, RoutedEventArgs e)
        {
            IsConfirmed = true;
            this.Close();
        }

        private void CancelEvent(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
