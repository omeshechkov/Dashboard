using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace StandartObjectLibrary
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class EllipseVisibilityConverter: IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (bool)value ? Visibility.Visible : Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (Visibility)value == Visibility.Visible;
        }

        #endregion
    }
}
