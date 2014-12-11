using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace StandartObjectLibrary
{
    [ValueConversion(typeof(object), typeof(double))]
    public class StringToValueConverter: IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            NumberFormatInfo nfi = new NumberFormatInfo() { NumberDecimalSeparator = "." };

            if (value == null)
                return double.NaN;

            double val;

            if (double.TryParse(value.ToString(), NumberStyles.Float, nfi, out val))
                return val;

            return double.NaN;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value.ToString();
        }

        #endregion
    }
}
