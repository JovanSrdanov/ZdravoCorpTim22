using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Shapes;
using ZdravoCorpAppTim22.View.Manager.Converters;

namespace ZdravoCorpAppTim22.View.Manager.Views
{
    public partial class GradesBlock : UserControl
    {
        public GradesBlock(string title, int[] grades)
        {
            InitializeComponent();
            this.DataContext = this;
            Title.Text = title;

            int[] gradeNumbers = { 0, 0, 0, 0, 0 };
            int total = 0;

            for(int i = 0; i < grades.Length; i++)
            {
                gradeNumbers[grades[i] - 1]++;
                total += grades[i];
            }
            double average = (double)total / grades.Length;
            
            Average.Text = ((double)(int)(average*100)/100).ToString();
            SetLineWidth(rect_1, "canvas_1", (double)gradeNumbers[4] / grades.Length);
            SetLineWidth(rect_2, "canvas_2", (double)gradeNumbers[3] / grades.Length);
            SetLineWidth(rect_3, "canvas_3", (double)gradeNumbers[2] / grades.Length);
            SetLineWidth(rect_4, "canvas_4", (double)gradeNumbers[1] / grades.Length);
            SetLineWidth(rect_5, "canvas_5", (double)gradeNumbers[0] / grades.Length);
        }

        public void SetLineWidth(Rectangle rect, string canvasName, double percentage)
        {
            Binding newBinding = new Binding()
            {
                Converter = new PercentageConverter(),
                ElementName = canvasName,
                Path = new PropertyPath("ActualWidth"),
                ConverterParameter = percentage
            };
            rect.SetBinding(Rectangle.WidthProperty, newBinding);
        }

    }
}
