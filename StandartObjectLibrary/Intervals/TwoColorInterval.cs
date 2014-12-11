using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace StandartObjectLibrary
{
    public class TwoColorInterval: Interval
    {
        private Color color1;
        public Color Color1
        {
            get { return color1; }
            set { color1 = value; }
        }

        private Color color2;
        public Color Color2
        {
            get { return color2; }
            set { color2 = value; }
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

        public TwoColorInterval() : this(0, Colors.Black, Colors.Black, false, 0) { }

        public TwoColorInterval(double value, Color color1, Color color2) : this(value, color1, color2, false, 0) { }

        public TwoColorInterval(double value, Color color1, Color color2, bool blinking, int blinkingInterval)
        {
            this.Value = value;
            this.color1 = color1;
            this.color2 = color2;
            this.blinking = blinking;
            this.blinkingSpeedRatio = blinkingInterval;
        }
    }
}
