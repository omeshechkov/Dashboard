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
using System.Collections;
using System.Xml.Linq;
using System.Xml;
using System.Globalization;
using System.Windows.Media.Media3D;
using System.ComponentModel;

namespace ExtendedObjectsLibrary
{
    /// <summary>
    /// Interaction logic for CirclularHistogramControl.xaml
    /// </summary>
    public partial class CirclularHistogramControl : UserControl
    {
        #region Properties

        private Dictionary<string, double> m_HistogramElements = new Dictionary<string, double>();

        private Color[] m_AvailableColors = new Color[] { 
            Colors.Aqua, Colors.Blue, Colors.BlueViolet, Colors.Brown, Colors.BurlyWood, 
            Colors.CadetBlue, Colors.Chartreuse, Colors.Chocolate, Colors.Coral, Colors.CornflowerBlue, Colors.Cornsilk, 
            Colors.Crimson,  Colors.Cyan,  Colors.DarkBlue, Colors.DarkCyan, Colors.DarkGoldenrod, Colors.DarkGray, 
            Colors.DarkGreen, Colors.DarkKhaki, Colors.DarkMagenta, Colors.DarkOliveGreen, Colors.DarkOrange, Colors.DarkOrchid, Colors.DarkRed, 
            Colors.DarkSalmon, Colors.DarkSeaGreen, Colors.DarkSlateBlue, Colors.DarkSlateGray, Colors.DarkTurquoise, Colors.DarkViolet, 
            Colors.DeepPink, Colors.DeepSkyBlue, Colors.DimGray, Colors.DodgerBlue, Colors.Firebrick, Colors.FloralWhite, 
            Colors.ForestGreen, Colors.Fuchsia, Colors.Gainsboro, Colors.GhostWhite, Colors.Gold, Colors.Goldenrod, Colors.Gray,
            Colors.Green, Colors.GreenYellow, Colors.Honeydew, Colors.HotPink, Colors.IndianRed, Colors.Indigo, Colors.Ivory, Colors.Khaki, 
            Colors.Lavender, Colors.LavenderBlush, Colors.LawnGreen, Colors.LemonChiffon, Colors.LightBlue, Colors.LightCoral,
            Colors.LightCyan, Colors.LightGoldenrodYellow, Colors.LightGray, Colors.LightGreen, Colors.LightPink, Colors.LightSalmon,
            Colors.LightSeaGreen, Colors.LightSkyBlue, Colors.LightSlateGray, Colors.LightSteelBlue, Colors.LightYellow, Colors.Lime,
            Colors.LimeGreen, Colors.Linen, Colors.Magenta, Colors.Maroon, Colors.MediumAquamarine, Colors.MediumBlue, Colors.MediumOrchid,
            Colors.MediumPurple, Colors.MediumSeaGreen, Colors.MediumSlateBlue, Colors.MediumSpringGreen, Colors.MediumTurquoise, 
            Colors.MediumVioletRed, Colors.MidnightBlue, Colors.MintCream, Colors.MistyRose, Colors.Moccasin, Colors.NavajoWhite,
            Colors.Navy, Colors.OldLace, Colors.Olive, Colors.OliveDrab, Colors.Orange, Colors.OrangeRed, Colors.Orchid, Colors.PaleGoldenrod, 
            Colors.PaleGreen, Colors.PaleTurquoise, Colors.PaleVioletRed, Colors.PapayaWhip, Colors.PeachPuff, Colors.Peru, Colors.Pink, 
            Colors.Plum, Colors.PowderBlue, Colors.Purple, Colors.Red, Colors.RosyBrown, Colors.RoyalBlue, Colors.SaddleBrown,
            Colors.Salmon, Colors.SandyBrown, Colors.SeaGreen, Colors.SeaShell, Colors.Sienna, Colors.Silver, Colors.SkyBlue,
            Colors.SlateBlue, Colors.SlateGray, Colors.Snow, Colors.SpringGreen, Colors.SteelBlue, Colors.Tan, Colors.Teal, Colors.Thistle, 
            Colors.Tomato, Colors.Transparent, Colors.Turquoise, Colors.Violet, Colors.Wheat, Colors.White, Colors.WhiteSmoke, 
            Colors.Yellow, Colors.YellowGreen};

        private int m_CurrentColorIndex = 0;

        private XElement m_Data { get; set; }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty DataSourceProperty = DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(CirclularHistogramControl),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDataSourceChangedCallback)));

        [Bindable(true), Category("Histogram Properties")]
        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        #endregion

        #region Callbacks

        private static void OnDataSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(DataSourceChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent DataSourceChangedEvent =
            EventManager.RegisterRoutedEvent("DataChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(CirclularHistogramControl));

        public event RoutedEventHandler DataSourceChanged
        {
            add { AddHandler(DataSourceChangedEvent, value); }
            remove { RemoveHandler(DataSourceChangedEvent, value); }
        }

        #endregion

        public CirclularHistogramControl()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(CirclularHistogramControl_Loaded);
        }

        private void CirclularHistogramControl_Loaded(object sender, RoutedEventArgs e)
        {
            DataSourceChanged += new RoutedEventHandler(CirclularHistogramControl_DataSourceChanged);
            imageBorder.SizeChanged += new SizeChangedEventHandler(imageBorder_SizeChanged);

            UpdateData();
            LoadLegend();
            Redraw();
        }

        private void imageBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            UpdateData();
            LoadLegend();
            Redraw();

        }

        private void CirclularHistogramControl_DataSourceChanged(object sender, RoutedEventArgs e)
        {
            UpdateData();
            LoadLegend();
            Redraw();
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
                        res = new XElement("HistogramData");

                    res.Add(XElement.Parse(sb.ToString()));
                }
            }

            m_Data = res;
        }

        private void LoadLegend()
        {
            if (m_Data == null)
                return;

            NumberFormatInfo nfi = (CultureInfo.CurrentCulture.Clone() as CultureInfo).NumberFormat;
            nfi.NumberDecimalSeparator = ".";

            List<string> currentListOfGraphics = new List<string>();

            var histogramElements = from el in m_Data.Elements("HistogramElement")
                                    let name = el.Attribute("name") == null ? "<no name>" : el.Attribute("name").Value
                                    let value = double.Parse(el.Value)
                                    orderby value descending
                                    select new
                                    {
                                        Name = name,
                                        Value = value
                                    };

            #region Add Graphics

            int currentHistogramColorIndex = 0;

            foreach (var histogramElement in histogramElements)
            {
                currentListOfGraphics.Add(histogramElement.Name);

                if (!m_HistogramElements.ContainsKey(histogramElement.Name))
                {
                    m_HistogramElements.Add(histogramElement.Name, histogramElement.Value);

                    HistogramLegendElement histogramLegendElement = new HistogramLegendElement();
                    histogramLegendElement.Text = histogramElement.Name;
                    histogramLegendElement.FillBrush = new SolidColorBrush(m_AvailableColors[currentHistogramColorIndex++]);

                    legendPanel.Children.Add(histogramLegendElement);
                }
                else
                {
                    for (int i = 0; i < legendPanel.Children.Count; i++)
                    {
                        HistogramLegendElement histogramLegendElement = legendPanel.Children[i] as HistogramLegendElement;

                        if (histogramLegendElement.Text == histogramElement.Name)
                        {
                            histogramLegendElement.Text = histogramElement.Name;
                            histogramLegendElement.FillBrush = new SolidColorBrush(m_AvailableColors[currentHistogramColorIndex++]);
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
                HistogramLegendElement histogramLegendElement = legendPanel.Children[i] as HistogramLegendElement;

                if (!currentListOfGraphics.Contains(histogramLegendElement.Text))
                {
                    itemsToRemove.Add(i);
                    m_HistogramElements.Remove(histogramLegendElement.Text);
                }
            }

            foreach (int itemToRemove in itemsToRemove)
            {
                HistogramLegendElement element = legendPanel.Children[itemToRemove] as HistogramLegendElement;
                legendPanel.Children.RemoveAt(itemToRemove);
            }

            #endregion

            legendPanel.UpdateLayout();
        }

        private void Redraw()
        {
            if (m_Data != null)
            {
                m_CurrentColorIndex = 0;
                var histogramElements = from el in m_Data.Elements("HistogramElement")
                                        let name = el.Attribute("name") == null ? "<no name>" : el.Attribute("name").Value
                                        let value = double.Parse(el.Value)
                                        orderby value descending
                                        select new
                                        {
                                            Name = name,
                                            Value = value
                                        };

                double totalSum = histogramElements.Sum(el => el.Value);

                double angle = 0;

                for (int i = 1; i < mainViewport.Children.Count; i++)
                    mainViewport.Children.RemoveAt(2);

                foreach (var histogramElement in histogramElements)
                {
                    double percent = histogramElement.Value * 100 / totalSum;

                    CreateSlice(percent, angle, histogramElement.Value, m_AvailableColors[m_CurrentColorIndex++] );
                    angle += percent / 100;
                }
            } 
        }

        private ModelUIElement3D CreateSlice(double percent, double currentAngle, double value, Color clr)
        {
            percent /= 100;

            double popOutDistance = 5.6; // distance slices are from origin

            double size = 40;
            double textpos = size * .25;

            Point popOutPoint = new Point(((popOutDistance - percent * 10) * Math.Sin((currentAngle + percent / 2) * 2 * Math.PI)),
                                            (-(popOutDistance - percent * 10) * Math.Cos((currentAngle + percent / 2) * 2 * Math.PI)));

            Point midwayPoint = new Point((size * Math.Sin((currentAngle + percent / 2) * 2 * Math.PI)), (-size * Math.Cos((currentAngle + percent / 2) * 2 * Math.PI)));

            // ModelUIElement3D is used so we can listen to events
            ModelUIElement3D pieSlice = new ModelUIElement3D();

            GeometryModel3D gm = new GeometryModel3D();
            DiffuseMaterial dm = new DiffuseMaterial(new SolidColorBrush(clr));
            gm.Material = dm;

            MeshGeometry3D mg = new MeshGeometry3D();
            gm.Geometry = mg;

            int max = (int)(percent * 200);
            mg.Positions.Add(new Point3D(0, 0, 0));
            for (int i = 0; i <= max; i++)
            {
                Point ArcPoint = new Point((size * Math.Sin(currentAngle * 2 * Math.PI)), (-size * Math.Cos(currentAngle * 2 * Math.PI)));
                currentAngle += 0.005;
                mg.Positions.Add(new Point3D(ArcPoint.X, ArcPoint.Y, 0));
            }

            // draw top layer
            max = mg.Positions.Count;
            for (int i = 0; i < max; i++)
                CreateTriangle(mg, 0, i + 1, i + 2);

            // add bottom layer
            for (int i = 0; i < max; i++)
                mg.Positions.Add(new Point3D(mg.Positions[i].X, mg.Positions[i].Y, -3));

            #region Draw Sides

            // draw straight sides
            CreateTriangle(mg, 0, max + 1, 1);
            CreateTriangle(mg, max, max + 1, 0);
            CreateTriangle(mg, max, max - 1, mg.Positions.Count - 1);
            CreateTriangle(mg, max, 0, max - 1);

            // draw curved sides
            for (int i = 1; i < max; i++)
            {
                CreateTriangle(mg, i, i + max, i + 1);
                CreateTriangle(mg, i + 1, i + max, i + max + 1);
            }

            #endregion

            pieSlice.Model = gm;

            pieSlice.Transform = new TranslateTransform3D(popOutPoint.X, popOutPoint.Y, 0);
            mainViewport.Children.Add(pieSlice);

            return pieSlice;
        }

        private static void CreateTriangle(MeshGeometry3D mg, int index0, int index1, int index2)
        {
            mg.TriangleIndices.Add(index0);
            mg.TriangleIndices.Add(index1);
            mg.TriangleIndices.Add(index2);
        }
    }
}