using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows;
using System.Windows.Markup;
using System.ComponentModel;

namespace StandartObjectLibrary
{
   public class LinearGradientColorFill: ColorFill
    {
        #region Properties

        private LinearGradientBrush fill;

        private Point m_StartPoint = new Point(0, 0);
        public Point StartPoint
        {
            get { return m_StartPoint; }
            set 
            { 
                m_StartPoint = value;
                NotifyPropertyChanged("StartPoint");
            }
        }


        private Point m_EndPoint = new Point(0, 0);
        public Point EndPoint
        {
            get { return m_EndPoint; }
            set 
            { 
                m_EndPoint = value;
                NotifyPropertyChanged("EndPoint");
            }
        }

        #endregion

        public LinearGradientColorFill()
        {
            PropertyChanged += new PropertyChangedEventHandler(LinearGradientColorFill_PropertyChanged);
            CreateGradient();
        }

        void LinearGradientColorFill_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Items":
                    CreateGradient();
                    break;
            }
        }

        private void CreateGradient()
        {
            double lowerLimit = double.MaxValue;
            double upperLimit = double.MinValue;

            foreach (ColorInterval colorInterval in Items)
            {
                if (colorInterval.Value < lowerLimit)
                    lowerLimit = colorInterval.Value;

                if (colorInterval.Value > upperLimit)
                    upperLimit = colorInterval.Value;
            }


            LinearGradientBrush gradientFill = new LinearGradientBrush();

            double difference = upperLimit - lowerLimit;

            if (difference != 0)
            {
                gradientFill.StartPoint = StartPoint;
                gradientFill.EndPoint = EndPoint;

                for (int i = 0; i < Items.Count; i++)
                {
                    ColorInterval col = Items[i];
                    double offset = (Items[i].Value - lowerLimit) / difference;

                    gradientFill.GradientStops.Add(new GradientStop(col.Color, offset));
                }
            }

            fill = gradientFill;
        }

        private Color GetColorAt(double value)
        {
            Color resultColor = new Color();

            for (int i = 0; i < Items.Count; i++)
            {
                ColorInterval interval1 = Items[i];
                ColorInterval interval2 = Items[i];

                if (i < Items.Count - 1)
                    interval2 = Items[i + 1];

                if (interval1.Value <= value && (value < interval2.Value || i == Items.Count - 1))
                {
                    double interval = interval2.Value - interval1.Value;

                    if (interval != 0)
                    {
                        double dA = interval2.Color.A - interval1.Color.A;
                        double dR = interval2.Color.R - interval1.Color.R;
                        double dG = interval2.Color.G - interval1.Color.G;
                        double dB = interval2.Color.B - interval1.Color.B;

                        double kA = (double)dA / interval;
                        double kR = (double)dR / interval;
                        double kG = (double)dG / interval;
                        double kB = (double)dB / interval;

                        Color res = new Color();
                        double temp = (double)value - interval1.Value;

                        try { res.A = checked((byte)(interval1.Color.A + (temp * kA))); }
                        catch { res.A = byte.MaxValue; }

                        try { res.R = checked((byte)(interval1.Color.R + (temp * kR))); }
                        catch { res.R = byte.MaxValue; }

                        try { res.G = checked((byte)(interval1.Color.G + (temp * kG))); }
                        catch { res.G = byte.MaxValue; }

                        try { res.B = checked((byte)(interval1.Color.B + (temp * kB))); }
                        catch { res.B = byte.MaxValue; }

                        resultColor = res;
                    }
                    else
                    {
                        resultColor = interval1.Color;
                    }
                }
            }

            return resultColor;
        }

        public override Brush GetBrushAt(double value)
        {
            CreateGradient();
            return fill;
        }

        public override Color this[double value]
        {
            get { return GetColorAt(value); }
        }
    }
}