using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ZdravoCorpAppTim22.View.DoctorView
{
    public partial class RequestDeniedView : Window
    {
        public RequestDeniedView()
        {
            InitializeComponent();
            ReasonTextBlock.Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
                "Suspendisse accumsan nunc non enim aliquet, in aliquam lacus dictum. " +
                "Aliquam felis ipsum, sodales quis pretium in, porttitor tempus mi. " +
                "Donec tristique fringilla arcu semper tristique. Mauris a sapien eleifend, " +
                "maximus lectus eu, eleifend dolor. Quisque a enim sit amet eros imperdiet " +
                "euismod eu euismod dolor. In nec quam congue, efficitur est eget, molestie mauris. " +
                "Maecenas sed ipsum sit amet augue posuere aliquam. Nulla sit amet bibendum neque, ut " +
                "vehicula lorem. Donec consectetur nisl ut mollis ullamcorper. Praesent odio erat, " +
                "pulvinar sit amet placerat et, ullamcorper quis mauris. Phasellus egestas, " +
                "augue efficitur malesuada porta, erat lacus pharetra ante, sed volutpat ante ex sed felis. " +
                "Vestibulum sit amet nulla eu nunc convallis dapibus.";
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)     //ne pomeraj
        {
            this.Owner.Show();
            this.Close();
        }

        private void LogOutBtn(object sender, RoutedEventArgs e)        //ne pomeraj
        {
            Application.Current.MainWindow.Show();
            foreach (Window item in App.Current.Windows)
            {
                if (item != Application.Current.MainWindow)
                {
                    item.Close();
                }
            }
        }
        private void HomeButtonClick(object sender, RoutedEventArgs e)      //ne pomeraj
        {
            DoctorHomeScreen.doctorHomeScreen.Show();
            this.Close();
        }
    }
}
