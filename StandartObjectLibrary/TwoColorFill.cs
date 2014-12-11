using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows;

namespace StandartObjectLibrary
{
    public class TwoColorFill: Fill<TwoColorIntervalCollection, Color[]>
    {
        private double m_Length = 100;
        public double Length
        {
            get { return m_Length; }
            set 
            { 
                m_Length = value;
                NotifyPropertyChanged("Length");
            }
        }

        public TwoColorInterval GetInterval(double value)
        {
            TwoColorInterval interval = null;

            if (value < Items[0].Value)
                interval = Items[0];

            for (int i = 0; i < Items.Count; i++)
            {
                TwoColorInterval colorInterval = Items[i];
                TwoColorInterval nextColorInterval = new TwoColorInterval();

                if (i < Items.Count - 1)
                    nextColorInterval = Items[i + 1];

                if (colorInterval.Value <= value && (value < nextColorInterval.Value || i == Items.Count - 1))
                {
                    interval = colorInterval;
                    break;
                }
            }

            return interval;
        }

        public static Color[] BuildGradient(Color color1, Color color2, int Length)
        {
            List<Color> gradient = new List<Color>();

            double incR = (double)(color2.R - color1.R) / (double)100;
            double incG = (double)(color2.G - color1.G) / (double)100;
            double incB = (double)(color2.B - color1.B) / (double)100;
            double incA = (double)(color2.A - color1.A) / (double)100;

            double curR = color1.R;
            double curG = color1.G;
            double curB = color1.B;
            double curA = color1.A;

            for (int i = 0; i < Length; i++)
            {
                curR += incR;
                curG += incG;
                curB += incB;
                curA += incA;

                gradient.Add(Color.FromArgb((byte)curA, (byte)curR, (byte)curG, (byte)curB));

                if (incR < 0)
                {
                    if (Math.Round(curR) < color2.R)
                        curR = color1.R;
                }
                else
                {
                    if (Math.Round(curR) > color2.R)
                        curR = color1.R;
                }

                if (incG < 0)
                {
                    if (Math.Round(curG) < color2.G)
                        curG = color1.G;
                }
                else
                {
                    if (Math.Round(curG) > color2.G)
                        curG = color1.G;
                }

                if (incB < 0)
                {
                    if (Math.Round(curB) < color2.B)
                        curB = color1.B;
                }
                else
                {
                    if (Math.Round(curB) > color2.B)
                        curB = color1.B;
                }

                if (incA < 0)
                {
                    if (Math.Round(curA) < color2.A)
                        curA = color1.A;
                }
                else
                {
                    if (Math.Round(curA) > color2.A)
                        curA = color1.A;
                }
            }

            return gradient.ToArray();
        }

        public override Color[] this[double value]
        {
            get
            {
                TwoColorInterval twoColorInterval = GetInterval(value);
                return BuildGradient(twoColorInterval.Color1, twoColorInterval.Color2, (int)Length);
            }
        }
    }
}