using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Xml;

namespace DashboardEngine.Converters
{
    [ValueConversion(typeof(XmlElement), typeof(String))]
    public class XmlElementToString : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string result = "";
            if (value is XmlElement)
                result = ((XmlElement)value).InnerText;

            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
