using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace StandartObjectLibrary
{
    public class ColorInterval: Interval
    {
        private Color color;
        public Color Color
        {
            get { return color; }
            set { color = value; }
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

        public ColorInterval() : this(0, Colors.Black, false, 0) { }

        public ColorInterval(double value, Color color) : this(value, color, false, 0) { }

        public ColorInterval(double value, Color color, bool blinking, int blinkingInterval)
        {
            this.Value = value;
            this.color = color;
            this.blinking = blinking;
            this.blinkingSpeedRatio = blinkingInterval;
        }
    }
}
