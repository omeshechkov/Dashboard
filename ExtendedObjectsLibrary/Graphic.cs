using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Xml.Linq;
using System.Globalization;

namespace ExtendedObjectsLibrary
{
    public class Graphic
    {
        public string Name { get; set; }
        public Pen Pen { get; set; }
        public SortedDictionary<DateTime, double> Values { get; set; }
        public double Scale { get; set; }

        public Graphic()
        {
            Values = new SortedDictionary<DateTime, double>();
            Pen = null;
        }

        public Graphic(string Name, Pen Pen): this()
        {
            this.Name = Name;
            this.Pen = Pen;
        }

        public Graphic(XElement xGraphic): this()
        {
            if (xGraphic == null)
                return;

            NumberFormatInfo nfi = (CultureInfo.CurrentCulture.Clone() as CultureInfo).NumberFormat;
            nfi.NumberDecimalSeparator = ".";

            uint colorRGB = 0xffffff;
            if (xGraphic.Attribute("color") != null && uint.TryParse(xGraphic.Attribute("color").Value, NumberStyles.AllowHexSpecifier, nfi, out colorRGB))
            {
                Color color = Color.FromArgb(255, (byte)(colorRGB >> 16 & 0xFF), (byte)(colorRGB >> 8 & 0xFF), (byte)(colorRGB & 0xFF));
                Pen = new Pen(new SolidColorBrush(color), 1);
            }

            Name = xGraphic.Attribute("name").Value;

            string scaleString = xGraphic.Attribute("scale") == null ? "1" : xGraphic.Attribute("scale").Value;

            Scale = double.Parse(scaleString, nfi);

            Values = new SortedDictionary<DateTime, double>();

            foreach (XElement value in xGraphic.Element("Values").Elements("Value"))
            {
                double val;
                try
                {
                    val = double.Parse(value.Value, nfi);
                }
                catch
                {
                    val = double.NaN;
                }

                try
                {
                    Values.Add(DateTime.Parse(value.Attribute("timeStamp").Value), val);
                }
                catch { }
            }
        }
    }
}
