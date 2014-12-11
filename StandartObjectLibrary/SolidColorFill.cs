using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace StandartObjectLibrary
{
    public class SolidColorFill : ColorFill
    {
        private Color GetColorAt(double value)
        {
            Color resultColor = new Color();
            
            if (Items != null)
            {
                if (value < Items[0].Value)
                    resultColor = Items[0].Color;

                for (int i = 0; i < Items.Count; i++)
                {
                    ColorInterval interval1 = Items[i];
                    ColorInterval interval2 = Items[i];

                    if (i < Items.Count - 1)
                        interval2 = Items[i + 1];

                    if (interval1.Value <= value && (value < interval2.Value || i == Items.Count - 1))
                        resultColor = interval1.Color;
                }
            }

            return resultColor;
        }

        public override Brush GetBrushAt(double value)
        {
            Color color = GetColorAt(value);

            return new SolidColorBrush(color);
        }

        public override Color this[double value]
        {
            get { return GetColorAt(value); }
        }
    }
}
