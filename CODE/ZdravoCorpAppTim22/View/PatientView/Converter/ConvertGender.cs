using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Model;

namespace ZdravoCorpAppTim22.View.PatientView.Converter
{
    public class ConvertGender : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (Gender)value;
            switch (v)
            {
                case Gender.male:
                    return "Muški";

                case Gender.female:
                    return "Ženski";

                case Gender.other:
                    return "Neodredjeno";

                default:
                    return "Neodredjeno";

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (string)value;
            switch (v)
            {
                case "Muški":
                    return Gender.male;

                case "Ženski":
                    return Gender.female;

                case "Neodredjeno":
                    return Gender.other;

                default:
                    return Gender.other;
            }
        }
    }
}