using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml;
using System.Collections;
using System.ComponentModel;
using DashboardEngine;

namespace ExtendedObjectsLibrary
{
    /// <summary>
    /// Interaction logic for GraphicControl.xaml
    /// </summary>
    public partial class GraphicControl : UserControl
    {
        #region Properties

        private DateTime m_MinTimeStamp = DateTime.MaxValue;
        private DateTime m_MaxTimeStamp = DateTime.MinValue;
        private double m_MinValue = double.MaxValue;
        private double m_MaxValue = double.MinValue;
        private double m_kx = 0;
        private double m_ky = 0;

        private static double topOffset = 5;
        private static double leftOffset = 1;
        private static double rightOffset = 1;
        private static double bottomOffset = 4;

        private Size ImageResolution { get; set; }
        private Dictionary<string, Graphic> graphics = new Dictionary<string, Graphic>();
        private string CheckedGraphicName = null;
        private bool dontRedrawGrid = false;

        private XElement Data = null;

        private Brush[] m_AvailableBrushes = new Brush[] { 
            Brushes.Aqua, Brushes.Blue, Brushes.BlueViolet, Brushes.Brown, Brushes.BurlyWood, 
            Brushes.CadetBlue, Brushes.Chartreuse, Brushes.Chocolate, Brushes.Coral, Brushes.CornflowerBlue, Brushes.Cornsilk, 
            Brushes.Crimson,  Brushes.Cyan,  Brushes.DarkBlue, Brushes.DarkCyan, Brushes.DarkGoldenrod, Brushes.DarkGray, 
            Brushes.DarkGreen, Brushes.DarkKhaki, Brushes.DarkMagenta, Brushes.DarkOliveGreen, Brushes.DarkOrange, Brushes.DarkOrchid, Brushes.DarkRed, 
            Brushes.DarkSalmon, Brushes.DarkSeaGreen, Brushes.DarkSlateBlue, Brushes.DarkSlateGray, Brushes.DarkTurquoise, Brushes.DarkViolet, 
            Brushes.DeepPink, Brushes.DeepSkyBlue, Brushes.DimGray, Brushes.DodgerBlue, Brushes.Firebrick, Brushes.FloralWhite, 
            Brushes.ForestGreen, Brushes.Fuchsia, Brushes.Gainsboro, Brushes.GhostWhite, Brushes.Gold, Brushes.Goldenrod, Brushes.Gray,
            Brushes.Green, Brushes.GreenYellow, Brushes.Honeydew, Brushes.HotPink, Brushes.IndianRed, Brushes.Indigo, Brushes.Ivory, Brushes.Khaki, 
            Brushes.Lavender, Brushes.LavenderBlush, Brushes.LawnGreen, Brushes.LemonChiffon, Brushes.LightBlue, Brushes.LightCoral,
            Brushes.LightCyan, Brushes.LightGoldenrodYellow, Brushes.LightGray, Brushes.LightGreen, Brushes.LightPink, Brushes.LightSalmon,
            Brushes.LightSeaGreen, Brushes.LightSkyBlue, Brushes.LightSlateGray, Brushes.LightSteelBlue, Brushes.LightYellow, Brushes.Lime,
            Brushes.LimeGreen, Brushes.Linen, Brushes.Magenta, Brushes.Maroon, Brushes.MediumAquamarine, Brushes.MediumBlue, Brushes.MediumOrchid,
            Brushes.MediumPurple, Brushes.MediumSeaGreen, Brushes.MediumSlateBlue, Brushes.MediumSpringGreen, Brushes.MediumTurquoise, 
            Brushes.MediumVioletRed, Brushes.MidnightBlue, Brushes.MintCream, Brushes.MistyRose, Brushes.Moccasin, Brushes.NavajoWhite,
            Brushes.Navy, Brushes.OldLace, Brushes.Olive, Brushes.OliveDrab, Brushes.Orange, Brushes.OrangeRed, Brushes.Orchid, Brushes.PaleGoldenrod, 
            Brushes.PaleGreen, Brushes.PaleTurquoise, Brushes.PaleVioletRed, Brushes.PapayaWhip, Brushes.PeachPuff, Brushes.Peru, Brushes.Pink, 
            Brushes.Plum, Brushes.PowderBlue, Brushes.Purple, Brushes.Red, Brushes.RosyBrown, Brushes.RoyalBlue, Brushes.SaddleBrown,
            Brushes.Salmon, Brushes.SandyBrown, Brushes.SeaGreen, Brushes.SeaShell, Brushes.Sienna, Brushes.Silver, Brushes.SkyBlue,
            Brushes.SlateBlue, Brushes.SlateGray, Brushes.Snow, Brushes.SpringGreen, Brushes.SteelBlue, Brushes.Tan, Brushes.Teal, Brushes.Thistle, 
            Brushes.Tomato, Brushes.Transparent, Brushes.Turquoise, Brushes.Violet, Brushes.Wheat, Brushes.White, Brushes.WhiteSmoke, 
            Brushes.Yellow, Brushes.YellowGreen};

        private int m_CurrentColorIndex = 0;

        [Category("Graphic Properties")]
        public Pen AxisPen { get; set; }

        [Category("Graphic Properties")]
        public Pen ScalePen { get; set; }

        [Category("Graphic Properties")]
        public Brush AxisTextBrush { get; set; }

        [Category("Graphic Properties")]
        public Typeface AxisTypeface { get; set; }

        [Category("Graphic Properties")]
        public double AxisFontSize { get; set; }

        [Category("Graphic Properties")]
        public double MinValue { get; set; }

        [Category("Graphic Properties")]
        public double MaxValue { get; set; }

        [Category("Graphic Properties")]
        public int YAxisDivBy { get; set; }

        [Category("Graphic Properties")]
        public LegendPosition LegendPosition { get; set; }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(GraphicControl),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDataChangedCallback)));

        [Bindable(true), Category("Graphic Properties")]
        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        #endregion

        #region Callbacks

        private static void OnDataChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(DataChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent DataChangedEvent =
            EventManager.RegisterRoutedEvent("DataChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(GraphicControl));

        public event RoutedEventHandler DataChanged
        {
            add { AddHandler(DataChangedEvent, value); }
            remove { RemoveHandler(DataChangedEvent, value); }
        }

        #endregion

        public GraphicControl()
        {
            InitializeComponent();

            AxisPen = new Pen(Brushes.White, 1);
            ScalePen = new Pen(Brushes.White, 0.5);
            AxisTextBrush = Brushes.White;
            AxisTypeface = new Typeface("Verdana");
            AxisFontSize = 8;

            MinValue = double.MaxValue;
            MaxValue = double.MinValue;

            YAxisDivBy = 5;

            LegendPosition = LegendPosition.Right;

            Loaded += new RoutedEventHandler(GraphicControl_Loaded);
        }

        private void UpdateData()
        {
            XElement res = null;

            if (DataSource != null)
            {
                foreach (XmlElement item in DataSource)
                {
                    StringBuilder sb = new StringBuilder();
                    using (XmlWriter writer = XmlWriter.Create(sb))
                        item.WriteTo(writer);

                    if (res == null)
                        res = new XElement("Graphics");

                    res.Add(XElement.Parse(sb.ToString()));
                }
            }

            Data = res;
        }

        private void GraphicControl_DataChanged(object sender, RoutedEventArgs e)
        {
            UpdateData();
            DrawGrid();
        }

        private Point LogicalToPhysical(DateTime TimeStamp, double Value)
        {
            double timeStampSecond = (TimeStamp - m_MinTimeStamp).TotalSeconds;

            Point result = new Point(leftOffset + timeStampSecond * m_kx, topOffset + (m_MaxValue - Value) * m_ky);

            return result;
        }

        private void LoadLegend()
        {
            if (Data == null)
                return;

            NumberFormatInfo nfi = (CultureInfo.CurrentCulture.Clone() as CultureInfo).NumberFormat;
            nfi.NumberDecimalSeparator = ".";

            List<string> currentListOfGraphics = new List<string>();

            #region Add Graphics

            foreach (XElement xGraphic in Data.Elements("Graphic"))
            {
                Graphic graphic = new Graphic(xGraphic);

                currentListOfGraphics.Add(graphic.Name);

                if (!graphics.ContainsKey(graphic.Name))
                {
                    if (graphic.Pen == null)
                        graphic.Pen = new Pen(m_AvailableBrushes[m_CurrentColorIndex++], 1);

                    if (m_CurrentColorIndex > m_AvailableBrushes.Length - 1)
                        m_CurrentColorIndex = 0;

                    graphics.Add(graphic.Name, graphic);

                    GraphicLegendElement graphicLegend = new GraphicLegendElement(graphic.Name);
                    graphicLegend.Text = graphic.Name;
                    graphicLegend.Color = ((SolidColorBrush)graphic.Pen.Brush).Color;
                    graphicLegend.Scale = graphic.Scale;
                    graphicLegend.CheckedStateChanged += LegendElementStateChanged;

                    legendPanel.Children.Add(graphicLegend);
                }
                else
                {
                    graphic.Pen = graphics[graphic.Name].Pen;
                    graphics[graphic.Name] = graphic;

                    for (int i = 0; i < legendPanel.Children.Count; i++)
                    {
                        GraphicLegendElement graphicLegendElement = legendPanel.Children[i] as GraphicLegendElement;

                        if (graphicLegendElement.OwnName == graphic.Name)
                        {
                            graphicLegendElement.Text = graphic.Name;
                            graphicLegendElement.Color = ((SolidColorBrush)graphic.Pen.Brush).Color;
                            graphicLegendElement.Scale = graphic.Scale;
                            break;
                        }
                    }
                }
            }

            #endregion

            #region Remove graphics

            List<int> itemsToRemove = new List<int>();

            for (int i = legendPanel.Children.Count - 1; i >= 0; i--)
            {
                GraphicLegendElement graphicLegendElement = legendPanel.Children[i] as GraphicLegendElement;

                if (!currentListOfGraphics.Contains(graphicLegendElement.OwnName))
                {
                    itemsToRemove.Add(i);
                    graphics.Remove(graphicLegendElement.OwnName);
                }
            }

            foreach (int itemToRemove in itemsToRemove)
            {
                GraphicLegendElement element = legendPanel.Children[itemToRemove] as GraphicLegendElement;

                if (element.OwnName == CheckedGraphicName)
                {
                    dontRedrawGrid = true;
                    element.IsChecked = false;
                    dontRedrawGrid = false;
                }

                legendPanel.Children.RemoveAt(itemToRemove);
            }

            #endregion

            layoutGrid.UpdateLayout();
            ImageResolution = new Size(imageBorder.ActualWidth, imageBorder.ActualHeight);
        }

        private void CalculateCoefs()
        {
            LoadLegend();

            NumberFormatInfo nfi = (CultureInfo.CurrentCulture.Clone() as CultureInfo).NumberFormat;
            nfi.NumberDecimalSeparator = ".";

            m_MinValue = MinValue;
            m_MaxValue = MaxValue;

            m_MinTimeStamp = DateTime.MaxValue;
            m_MaxTimeStamp = DateTime.MinValue;

            if (Data == null)
                return;

            foreach (XElement xGraphic in Data.Elements("Graphic"))
            {
                string scaleString = xGraphic.Attribute("scale") == null ? "1" : xGraphic.Attribute("scale").Value;
                double scale = double.Parse(scaleString, nfi);

                foreach (XElement value in xGraphic.Element("Values").Elements("Value"))
                {
                    DateTime timeStamp;
                    double val;

                    try
                    {
                        val = double.Parse(value.Value, NumberStyles.Number, nfi);
                    }
                    catch
                    {
                        val = double.NaN;
                    }


                    if (value != null && value.Attribute("timeStamp") != null && DateTime.TryParse(value.Attribute("timeStamp").Value, out timeStamp))
                    {
                        if (!double.IsNaN(val))
                            val *= scale;

                        if (timeStamp < m_MinTimeStamp)
                            m_MinTimeStamp = timeStamp;

                        if (timeStamp > m_MaxTimeStamp)
                            m_MaxTimeStamp = timeStamp;

                        if (!double.IsNaN(val))
                        {
                            m_MinValue = Math.Min(val, m_MinValue);
                            m_MaxValue = Math.Max(val, m_MaxValue);
                        }
                    }
                }
            }

            if (m_MinValue == m_MaxValue)
            {
                m_MinValue--;
                m_MaxValue++;
            }
        }

        private void DrawGrid()
        {
            if (Data == null)
                return;

            CalculateCoefs();

            FormattedText axisValuesText = new FormattedText("0", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, AxisTypeface, AxisFontSize, Brushes.White);

            #region Calc Max Height

            double maxHeight = 0;

            string measureUnit = "";
            double totalSeconds = (m_MaxTimeStamp - m_MinTimeStamp).TotalSeconds;
            double maxTime = 0;

            if (totalSeconds < 60)
            {
                measureUnit = "s";
                maxTime = totalSeconds;
            }
            else if (totalSeconds < 3600)
            {
                measureUnit = "m";
                maxTime = Math.Round(TimeSpan.FromSeconds(totalSeconds).TotalMinutes, 1);
            }
            else if (totalSeconds < 86400)
            {
                measureUnit = "h";
                maxTime = Math.Round(TimeSpan.FromSeconds(totalSeconds).TotalHours, 1);
            }
            else
            {
                measureUnit = "d";
                maxTime = Math.Round(TimeSpan.FromSeconds(totalSeconds).TotalDays, 1);
            }

            if (maxTime > 0)
            {
                for (double i = 0; i <= maxTime; i += (maxTime / 5))
                {
                    double value = Math.Round(maxTime - i, 1);
                    string str = value + " " + measureUnit;

                    axisValuesText = new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, AxisTypeface, AxisFontSize, AxisTextBrush);
                    maxHeight = Math.Max(axisValuesText.Height, maxHeight);
                }
            }

            maxHeight += 10;

            #endregion

            #region Calc Max Width

            double maxWidth = 0;

            double yStep = (m_MaxValue - m_MinValue) / (double)(YAxisDivBy - 1);

            for (int i = 0; i < YAxisDivBy; i++)
            {
                double value = i == (YAxisDivBy - 1) ? m_MaxValue : m_MinValue + (double)i * yStep;
                axisValuesText = new FormattedText(Math.Round(value, 2).ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, AxisTypeface, AxisFontSize, AxisTextBrush);
                maxWidth = Math.Max(maxWidth, axisValuesText.Width);
            }

            maxWidth += 8;

            #endregion

            double imageWidth = imagesBorder.ActualWidth - maxWidth - 10;
            double imageHeight = imagesBorder.ActualHeight - maxHeight - 2;
            if (imageWidth < 0 || imageHeight < 0)
                return;

            ImageResolution = new Size(imageWidth, imageHeight);

            m_kx = (ImageResolution.Width - leftOffset - rightOffset - 1) / (m_MaxTimeStamp - m_MinTimeStamp).TotalSeconds;
            m_ky = (ImageResolution.Height - topOffset - bottomOffset) / (m_MaxValue - m_MinValue);

            #region Draw Y Axis

            List<double> yGridValues = new List<double>();

            if (totalSeconds > 0)
            {
                DrawingVisual yAxisVisual = new DrawingVisual();

                using (DrawingContext drawingContext = yAxisVisual.RenderOpen())
                {
                    for (int i = 0; i < YAxisDivBy; i++)
                    {
                        double value = i == (YAxisDivBy - 1) ? m_MaxValue : m_MinValue + (double)i * yStep;

                        double y = topOffset + (m_MaxValue - value) * m_ky;
                        yGridValues.Add(y);

                        drawingContext.DrawLine(ScalePen, new Point(maxWidth - 4, y), new Point(maxWidth, y));

                        axisValuesText = new FormattedText(Math.Round(value, 2).ToString(), CultureInfo.CurrentCulture, FlowDirection.LeftToRight, AxisTypeface, AxisFontSize, AxisTextBrush) { TextAlignment = TextAlignment.Right };
                        drawingContext.DrawText(axisValuesText, new Point(maxWidth - 6, y - axisValuesText.Height / 2));
                    }
                }

                RenderTargetBitmap yAxisBmp = new RenderTargetBitmap((int)maxWidth, (int)ImageResolution.Height, 96, 96, PixelFormats.Pbgra32);
                yAxisBmp.Render(yAxisVisual);
                yAxisImage.Source = yAxisBmp;
            }

            #endregion

            #region Draw X Axis

            List<double> xGridValues = new List<double>();

            if (totalSeconds > 0)
            {
                DrawingVisual xAxisVisual = new DrawingVisual();

                using (DrawingContext drawingContext = xAxisVisual.RenderOpen())
                {
                    for (double i = 0; i <= maxTime; i += (maxTime / 5))
                    {
                        string str = Math.Round(maxTime - i, 1) + " " + measureUnit;

                        double x = i * (ImageResolution.Width - leftOffset - rightOffset - 1) / maxTime;

                        xGridValues.Add(x + 1);

                        axisValuesText = new FormattedText(str, CultureInfo.CurrentCulture, FlowDirection.LeftToRight, AxisTypeface, AxisFontSize, AxisTextBrush);
                        axisValuesText.TextAlignment = TextAlignment.Center;

                        if (i == 0)
                            axisValuesText.TextAlignment = TextAlignment.Left;

                        if (i == maxTime)
                            axisValuesText.TextAlignment = TextAlignment.Right;

                        drawingContext.DrawText(axisValuesText, new Point(leftOffset + x, 4));
                        drawingContext.DrawLine(ScalePen, new Point(leftOffset + x, 0), new Point(leftOffset + x, 3));
                    }
                }

                RenderTargetBitmap xAxisBmp = new RenderTargetBitmap((int)ImageResolution.Width, (int)maxHeight, 96, 96, PixelFormats.Pbgra32);
                xAxisBmp.Render(xAxisVisual);
                xAxisTextImage.Source = xAxisBmp;
            }

            #endregion

            #region Draw Graphics

            DrawingVisual visual = new DrawingVisual();

            using (DrawingContext drawingContext = visual.RenderOpen())
            {
                #region Draw Grid Lines

                SolidColorBrush gridLinesBrush = new SolidColorBrush();
                gridLinesBrush.Color = Color.FromArgb(0x60, 0xff, 0xff, 0xff);

                foreach (double yGridValue in yGridValues)
                    drawingContext.DrawLine(new Pen(gridLinesBrush, 0.5), new Point(0, yGridValue), new Point(ImageResolution.Width, yGridValue));

                foreach (double xGridValue in xGridValues)
                    drawingContext.DrawLine(new Pen(gridLinesBrush, 0.5), new Point(xGridValue, ImageResolution.Height), new Point(xGridValue, 0));

                #endregion

                DrawGraphics(drawingContext);
            }

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)ImageResolution.Width, (int)ImageResolution.Height, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(visual);
            image.Source = bmp;

            #endregion
        }

        private Pen ChangePenOpacity(Pen pen, byte a)
        {
            Color color = (pen.Brush as SolidColorBrush).Color;
            color.A = a;

            pen = new Pen(new SolidColorBrush(color), pen.Thickness);

            return pen;
        }

        private void DrawGraphics(DrawingContext drawingContext)
        {
            NumberFormatInfo nfi = (CultureInfo.CurrentCulture.Clone() as CultureInfo).NumberFormat;
            nfi.NumberDecimalSeparator = ".";

            foreach (KeyValuePair<string, Graphic> graphic in graphics)
            {
                string graphicName = graphic.Key;

                if (string.IsNullOrEmpty(CheckedGraphicName))
                {
                    graphic.Value.Pen = ChangePenOpacity(graphic.Value.Pen, 0xFF);
                    DrawGraphic(drawingContext, graphic.Value);
                }
                else if (CheckedGraphicName != graphicName)
                {
                    graphic.Value.Pen = ChangePenOpacity(graphic.Value.Pen, 0x77);
                    DrawGraphic(drawingContext, graphic.Value);
                }
            }

            //Move to front if checked
            if (!string.IsNullOrEmpty(CheckedGraphicName))
            {
                graphics[CheckedGraphicName].Pen.Thickness = 2;
                DrawGraphic(drawingContext, graphics[CheckedGraphicName]);
            }
        }

        private void DrawGraphic(DrawingContext drawingContext, Graphic graphic)
        {
            Point prevPoint = new Point();
            Point lastNormalPoint = new Point();
            double prevValue = 0;
            
            bool firstPoint = true;

            int c = 0;
            foreach (KeyValuePair<DateTime, double> value in graphic.Values)
            {
                double val = value.Value;

                Point p = new Point();
                if (!double.IsNaN(val))
                    p = LogicalToPhysical(value.Key, val * graphic.Scale);

                if (!firstPoint && !double.IsNaN(val) && !double.IsNaN(prevValue))
                    drawingContext.DrawLine(graphic.Pen, prevPoint, p);

                if (!firstPoint && !double.IsNaN(val) && double.IsNaN(prevValue))
                {
                    Pen dashPen = graphic.Pen.Clone();
                    dashPen.DashStyle = new DashStyle(new double[] { 2, 8}, 0);
                    dashPen.Thickness = 0.5;

                    drawingContext.DrawLine(dashPen, lastNormalPoint, p);

                    if (c == graphic.Values.Count - 1)
                        drawingContext.DrawEllipse(graphic.Pen.Brush, graphic.Pen, p, 1, 1);
                }

                prevPoint = p;
                prevValue = val;

                if (!double.IsNaN(val))
                    lastNormalPoint = p;

                firstPoint = false;
                c++;
            }
        }

        private void SetupLegendPosition()
        {
            if (LegendPosition == LegendPosition.Bottom)
            {
                legendPanel.Orientation = Orientation.Horizontal;
                Grid.SetRow(legendBorder, 1);
            }
            else
            {
                Grid.SetColumn(legendBorder, 1);
            }

        }

        private void GraphicControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataChanged += new RoutedEventHandler(GraphicControl_DataChanged);
            SizeChanged += new SizeChangedEventHandler(GraphicControl_SizeChanged);

            UpdateData();

            SetupLegendPosition();

            DrawGrid();
        }

        

        private void GraphicControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateData();
            DrawGrid();
        }

        private void LegendElementStateChanged(object sender, EventArgs e)
        {
            GraphicLegendElement currentElement = sender as GraphicLegendElement;

            if (currentElement.IsChecked)
            {
                for (int i = 0; i < legendPanel.Children.Count; i++)
                {
                    GraphicLegendElement element = legendPanel.Children[i] as GraphicLegendElement;

                    if (sender != element)
                        element.IsChecked = false;
                }

                CheckedGraphicName = currentElement.OwnName;
                DrawGrid();
            }
            else
            {
                if (!string.IsNullOrEmpty(CheckedGraphicName))
                    graphics[CheckedGraphicName].Pen.Thickness = 1;

                CheckedGraphicName = null;

                if (!dontRedrawGrid)
                    DrawGrid();
            }
        }
    }
}