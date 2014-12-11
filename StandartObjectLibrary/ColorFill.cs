using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Markup;

namespace StandartObjectLibrary
{
    public abstract class ColorFill : Fill<ColorIntervalCollection, Color>
    {
        public abstract Brush GetBrushAt(double value);
    }
}