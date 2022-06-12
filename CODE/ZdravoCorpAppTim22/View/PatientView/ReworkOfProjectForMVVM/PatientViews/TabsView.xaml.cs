using System.Windows.Controls;


namespace ZdravoCorpAppTim22.View.PatientView.ReworkOfProjectForMVVM.PatientViews
{
 
    public partial class TabsView : UserControl
    {
       
        public TabsView()
        {
            InitializeComponent();
            
        }

        private void WizardButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            WizardWindow wizardWindow = new WizardWindow();
            wizardWindow.ShowDialog();
        }
    }
}
