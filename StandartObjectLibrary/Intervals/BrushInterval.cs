using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace StandartObjectLibrary
{
    public class BrushInterval: Interval
    {
        private Brush brush;
        public Brush Brush
        {
            get { return brush; }
            set { brush = value; }
        }

        private bool blinking;
        public bool Blinking
        {
            get { return blinking; }
            set { blinking = value; }
        }

        private int blinkingSpeedRatio;
        public int BlinkingSpeedRatio
        {
            get { return blinkingSpeedRatio; }
            set { blinkingSpeedRatio = value; }
        }

        public BrushInterval() : this(0, Brushes.Black, false, 0) { }

        public BrushInterval(double value, Brush brush) : this(value, brush, false, 0) { }

        public BrushInterval(double value, Brush brush, bool blinking, int blinkingInterval)
        {
            this.Value = value;
            this.brush = brush;
            this.blinking = blinking;
            this.blinkingSpeedRatio = blinkingInterval;
        }
    }
}
