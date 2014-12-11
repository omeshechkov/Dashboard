using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;

namespace DashboardEngine
{
    public class XmlHelper
    {
        public static bool GetAttribute(XmlNode node, string attributeName, out double value)
        {
            NumberFormatInfo nfi = new NumberFormatInfo() { NumberDecimalSeparator = "." };
            value = 0;
            XmlAttribute attr = (XmlAttribute)node.Attributes.GetNamedItem(attributeName);

            if (attr != null)
                return double.TryParse(attr.Value, NumberStyles.Float, nfi, out value);

            return false;
        }

        public static bool GetAttribute(XmlNode node, string attributeName, out string value)
        {
            value = string.Empty;
            XmlAttribute attr = (XmlAttribute)node.Attributes.GetNamedItem(attributeName);

            if (attr != null)
            {
                value = attr.Value;
                return true;
            }

            return false;
        }

        public static bool GetAttribute(XmlNode node, string attributeName, out bool value)
        {
            value = false;
            XmlAttribute attr = (XmlAttribute)node.Attributes.GetNamedItem(attributeName);

            if (attr != null)
                return bool.TryParse(attr.Value, out value);

            return false;
        }
    }
}
