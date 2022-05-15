using Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace ZdravoCorpAppTim22.View.PatientView.Converter
{
    public class ConverterToSerbian : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (AppointmentType)value;
            switch (v)
            {
                case AppointmentType.Checkup:
                    return "Kontrola";

                case AppointmentType.Examination:
                    return "Pregled";

                case AppointmentType.Operation:
                    return "Operacija";

                default:
                    return "NEDEFINISANO";

            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (string)value;
            switch (v)
            {
                case "Kontrola":
                    return AppointmentType.Checkup;

                case "Pregled":
                    return AppointmentType.Examination;

                case "Operacija":
                    return AppointmentType.Operation;

                default:
                    return null;

            }
        }
    }
}

