using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace DashboardEngine
{
    public class ColorHelper
    {
        public static Color ChangeColor(Color color, float percent, int sign)
        {
            int[] colors = new int[] { color.R, color.G, color.B };

            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] += (int)((double)colors[i] * (double)percent * (double)Math.Sign(sign));
                if (colors[i] > 255)
                    colors[i] = 255;
            }

            Color returnColor = Color.FromRgb((byte)colors[0], (byte)colors[1], (byte)colors[2]);

            return returnColor;
        }
    }
}
