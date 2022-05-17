using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace ZdravoCorpAppTim22.View.PatientView.Converter
{
    public class ConvertReportReviewed : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (bool)value;
            switch (v)
            {
                case false:
                    return "Neocenjen";

                case true:
                    return "Ocenjen";
                default:
                    return "Neocenjen";
                    
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (string)value;
            switch (v)
            {
                case "Neocenjen":
                    return false;

                case "Ocenjen":
                    return false;

                default:
                    return false;

            }
        }
    }
}
