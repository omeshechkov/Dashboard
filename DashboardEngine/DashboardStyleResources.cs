using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;
using System.Windows;
using DashboardEngine;

namespace DashboardEngine
{
    public static class DashboardStyleResources
    {
        public static Color Background
        {
            get { return Color.FromRgb(0x14, 0x17, 0x1d); }
        }


        public static Dictionary<Severity, Color> FillColors
        {
            get
            {
                Dictionary<Severity, Color> colors = new Dictionary<Severity, Color>();
                colors.Add(Severity.Normal, Color.FromRgb(0, 128, 0));
                colors.Add(Severity.Information, Color.FromRgb(6, 159, 171));
                colors.Add(Severity.Low, Color.FromRgb(191, 168, 6));
                colors.Add(Severity.Medium, Color.FromRgb(181, 100, 0));
                colors.Add(Severity.High, Color.FromRgb(199, 21, 12));
                colors.Add(Severity.Disabled, Color.FromRgb(108, 108, 108));
                
                return colors;
            }
        }

        public static Dictionary<Severity, SolidColorBrush> SolidFillBrushes
        {
            get
            {
                Dictionary<Severity, SolidColorBrush> brushes = new Dictionary<Severity, SolidColorBrush>();
                brushes.Add(Severity.Normal, new SolidColorBrush(FillColors[Severity.Normal]));
                brushes.Add(Severity.Information, new SolidColorBrush(FillColors[Severity.Information]));
                brushes.Add(Severity.Low, new SolidColorBrush(FillColors[Severity.Low]));
                brushes.Add(Severity.Medium, new SolidColorBrush(FillColors[Severity.Medium]));
                brushes.Add(Severity.High, new SolidColorBrush(FillColors[Severity.High]));
                brushes.Add(Severity.Disabled, new SolidColorBrush(FillColors[Severity.Disabled]));

                return brushes;
            }
        }

        private static RadialGradientBrush CreateEllipseBrush(Color color)
        {
            Color ligthColor = ColorHelper.ChangeColor(color, 0.8F, 1);
            Color darkColor = ColorHelper.ChangeColor(color, 0.4F, -1);

            GradientStopCollection gradientStops = new GradientStopCollection();
            gradientStops.Add(new GradientStop(ligthColor, 0));
            gradientStops.Add(new GradientStop(darkColor, 1));

            RadialGradientBrush brush = new RadialGradientBrush(gradientStops);
            brush.Center = new Point(0.5, 0.5);
            brush.GradientOrigin = new Point(0.3, 0.3);
            brush.RadiusX = 0.5;
            brush.RadiusY = 0.5;

            return brush;
        }

        private static LinearGradientBrush CreateLinearBrush(Color color)
        {
            Color ligthColor = ColorHelper.ChangeColor(color, 0.1F, 1);
            Color darkColor = ColorHelper.ChangeColor(color, 0.1F, -1);

            GradientStopCollection gradientStops = new GradientStopCollection();
            gradientStops.Add(new GradientStop(ligthColor, 0));
            gradientStops.Add(new GradientStop(darkColor, 1));

            LinearGradientBrush brush = new LinearGradientBrush(gradientStops);
            brush.StartPoint = new Point(0, 0);
            brush.EndPoint = new Point(0, 1);

            return brush;
        }

        public static Dictionary<Severity, RadialGradientBrush> RadialFillBrushes
        {
            get
            {
                Dictionary<Severity, RadialGradientBrush> brushes = new Dictionary<Severity, RadialGradientBrush>();

                brushes.Add(Severity.Normal, CreateEllipseBrush(FillColors[Severity.Normal]));
                brushes.Add(Severity.Information, CreateEllipseBrush(FillColors[Severity.Information]));
                brushes.Add(Severity.Low, CreateEllipseBrush(FillColors[Severity.Low]));
                brushes.Add(Severity.Medium, CreateEllipseBrush(FillColors[Severity.Medium]));
                brushes.Add(Severity.High, CreateEllipseBrush(FillColors[Severity.High]));
                brushes.Add(Severity.Disabled, CreateEllipseBrush(FillColors[Severity.Disabled]));

                return brushes;
            }
        }

        public static Dictionary<Severity, LinearGradientBrush> LinearFillBrushes
        {
            get
            {
                Dictionary<Severity, LinearGradientBrush> brushes = new Dictionary<Severity, LinearGradientBrush>();

                brushes.Add(Severity.Normal, CreateLinearBrush(FillColors[Severity.Normal]));
                brushes.Add(Severity.Information, CreateLinearBrush(FillColors[Severity.Information]));
                brushes.Add(Severity.Low, CreateLinearBrush(FillColors[Severity.Low]));
                brushes.Add(Severity.Medium, CreateLinearBrush(FillColors[Severity.Medium]));
                brushes.Add(Severity.High, CreateLinearBrush(FillColors[Severity.High]));
                brushes.Add(Severity.Disabled, CreateLinearBrush(FillColors[Severity.Disabled]));

                return brushes;
            }
        }
    }
}