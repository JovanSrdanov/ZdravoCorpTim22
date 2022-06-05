using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Shapes;
using ZdravoCorpAppTim22.View.Manager.Converters;

namespace ZdravoCorpAppTim22.View.Manager.Views
{
    public partial class GradesBlock : UserControl
    {
        public GradesBlock(string title, List<int> grades)
        {
            InitializeComponent();
            this.DataContext = this;
            Title.Text = title;

            int[] gradeNumbers = { 0, 0, 0, 0, 0 };
            int total = 0;

            for (int i = 0; i < grades.Count; i++)
            {
                gradeNumbers[grades[i] - 1]++;
                total += grades[i];
            }

            if(grades.Count != 0)
            {
                double average = (double)total / grades.Count;

                Average.Text = string.Format("{0:F2}", average);
                SetLineWidth(rect_1, "canvas_1", (double)gradeNumbers[4] / grades.Count);
                SetLineWidth(rect_2, "canvas_2", (double)gradeNumbers[3] / grades.Count);
                SetLineWidth(rect_3, "canvas_3", (double)gradeNumbers[2] / grades.Count);
                SetLineWidth(rect_4, "canvas_4", (double)gradeNumbers[1] / grades.Count);
                SetLineWidth(rect_5, "canvas_5", (double)gradeNumbers[0] / grades.Count);
            }
            else
            {
                Average.Text = string.Format("{0:F2}", 0);
            }
            SetGradeNumbers(gradeNumbers);
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

        public void SetGradeNumbers(int[] gradeNumbers) {

            Brush ActiveBrush = (Brush)FindResource("ActivePrimary");
            TextBlock[] blocks = { grade_number_1, grade_number_2, grade_number_3, grade_number_4, grade_number_5 };
            int max = FindMaxGrades(gradeNumbers);
            for (int i = 0; i < gradeNumbers.Length; i++)
            {
                blocks[i].Text = gradeNumbers[gradeNumbers.Length - i - 1].ToString();
                if(max != 0 && gradeNumbers[gradeNumbers.Length - i - 1] == max)
                {
                    blocks[i].Foreground = ActiveBrush;
                }
            }
        }

        public int FindMaxGrades(int[] gradeNumbers)
        {
            int max = 0;
            for (int i = 0; i < gradeNumbers.Length; i++)
            {
                if (gradeNumbers[i] > max)
                {
                    max = gradeNumbers[i];
                }
            }
            return max;
        }
    }
}
