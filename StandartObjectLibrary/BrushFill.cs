using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Markup;
using System.Windows;
using System.Windows.Media;

namespace StandartObjectLibrary
{
    public class BrushFill : Fill<BrushIntervalCollection, BrushInterval>
    {
        private BrushInterval GetBrushIntervalAt(double value)
        {
            BrushInterval resultInterval = new BrushInterval();

            if (Items != null)
            {
                if (value < Items[0].Value)
                    resultInterval = Items[0];

                for (int i = 0; i < Items.Count; i++)
                {
                    BrushInterval interval1 = Items[i];
                    BrushInterval interval2 = Items[i];

                    if (i < Items.Count - 1)
                        interval2 = Items[i + 1];

                    if (interval1.Value <= value && (value < interval2.Value || i == Items.Count - 1))
                        resultInterval = interval1;
                }
            }

            return resultInterval;
        }

        public override BrushInterval this[double value]
        {
            get { return GetBrushIntervalAt(value); }
        }
    }
}
