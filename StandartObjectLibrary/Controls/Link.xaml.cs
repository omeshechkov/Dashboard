using System;
using System.Collections.Generic;
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
using System.ComponentModel;
using System.Windows.Media.Animation;
using System.Collections;
using System.Globalization;
using System.Xml;

namespace StandartObjectLibrary
{
    /// <summary>
    /// Interaction logic for Link.xaml
    /// </summary>
    public partial class Link : UserControl
    {
        #region Properties

        private AnimationClock linkMoveAnimationClock;
        private AnimationClock linkBlinkAnimationClock;

        private DateTime m_MinTimeStamp = DateTime.MaxValue;
        private DateTime m_MaxTimeStamp = DateTime.MinValue;
        private double m_MinValue = double.MaxValue;
        private double m_MaxValue = double.MinValue;
        private double m_kx = 0;
        private double m_ky = 0;
        private bool m_PauseMoveAnimation = false;
        private SortedDictionary<DateTime, double> m_Graphic = new SortedDictionary<DateTime, double>();

        [Category("Link Properties")]
        public double MinValue { get; set; }

        [Category("Link Properties")]
        public double MaxValue { get; set; }

        [Category("Link Properties")]
        public TimeSpan Period { get; set; }

        [Category("Link Properties")]
        public Direction LinkDirection { get; set; }

        [Category("Link Properties")]
        public int LinkArrowLength { get; set; }

        [Category("Link Properties")]
        public bool IsVerticalOrientation
        {
            get { return LinkDirection == Direction.Down || LinkDirection == Direction.Up; }
        }

        [Category("Link Properties")]
        public bool IsHorizontalOrientation
        {
            get { return LinkDirection == Direction.Left || LinkDirection == Direction.Right; }
        }

        [Category("Link Properties")]
        public double StrokeHeight { get; set; }

        [Category("Link Properties")]
        public double LinkLength { get; set; }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty Color1Property =
            DependencyProperty.Register("Color1", typeof(Color), typeof(Link),
            new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColor1ChangedCallback)));

        [Bindable(true), Category("Link Properties")]
        public Color Color1
        {
            get { return (Color)GetValue(Color1Property); }
            set { SetValue(Color1Property, value); }
        }

        public static readonly DependencyProperty Color2Property =
            DependencyProperty.Register("Color2", typeof(Color), typeof(Link),
            new FrameworkPropertyMetadata(Colors.Gray, new PropertyChangedCallback(OnColor2ChangedCallback)));

        [Bindable(true), Category("Link Properties")]
        public Color Color2
        {
            get { return (Color)GetValue(Color2Property); }
            set { SetValue(Color2Property, value); }
        }

        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register("SpeedRatio", typeof(double), typeof(Link),
            new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnSpeedRatioChangedCallback), new CoerceValueCallback(SpeedRatioCoerceCallback)));

        [Bindable(true), Category("Link Properties")]
        public double SpeedRatio
        {
            get { return (double)GetValue(SpeedRatioProperty); }
            set { SetValue(SpeedRatioProperty, value); }
        }

        public static readonly DependencyProperty BlinkingProperty =
            DependencyProperty.Register("Blinking", typeof(bool), typeof(Link),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnBlinkingChangedCallback)));

        [Bindable(true), Category("Link Properties")]
        public bool Blinking
        {
            get { return (bool)GetValue(BlinkingProperty); }
            set { SetValue(BlinkingProperty, value); }
        }

        public static readonly DependencyProperty BlinkingSpeedRatioProperty =
            DependencyProperty.Register("BlinkingSpeedRatio", typeof(double), typeof(Link),
            new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnBlinkingSpeedRatioChangedCallback),
                new CoerceValueCallback(BlinkingSpeedRatioCoerceCallback)));


        [Bindable(true), Category("Link Properties")]
        public double BlinkingSpeedRatio
        {
            get { return (double)GetValue(BlinkingSpeedRatioProperty); }
            set { SetValue(BlinkingSpeedRatioProperty, value); }
        }


        public static readonly DependencyProperty GraphicValuesProperty =
            DependencyProperty.Register("GraphicValues", typeof(IEnumerable), typeof(Link),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnGraphicValuesChangedCallback)));

        [Bindable(true), Category("Link Properties")]
        public IEnumerable GraphicValues
        {
            get { return (IEnumerable)GetValue(GraphicValuesProperty); }
            set { SetValue(GraphicValuesProperty, value); }
        }

        #endregion

        #region Callbacks

        private static void OnColor1ChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(Color1ChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static void OnColor2ChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(Color2ChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static void OnSpeedRatioChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(SpeedRatioChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static object SpeedRatioCoerceCallback(DependencyObject d, object o)
        {
            return Math.Max((double)0, (double)o);
        }

        private static void OnBlinkingChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(BlinkingChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static void OnBlinkingSpeedRatioChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(BlinkingSpeedRatioChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static object BlinkingSpeedRatioCoerceCallback(DependencyObject d, object o)
        {
            return Math.Max((double)0, (double)o);
        }

        private static void OnGraphicValuesChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(GraphicValuesChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent Color1ChangedEvent =
            EventManager.RegisterRoutedEvent("Color1Changed", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Link));

        public event RoutedEventHandler Color1Changed
        {
            add { AddHandler(Color1ChangedEvent, value); }
            remove { RemoveHandler(Color1ChangedEvent, value); }
        }

        public static readonly RoutedEvent Color2ChangedEvent =
            EventManager.RegisterRoutedEvent("Color2Changed", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Link));

        public event RoutedEventHandler Color2Changed
        {
            add { AddHandler(Color2ChangedEvent, value); }
            remove { RemoveHandler(Color2ChangedEvent, value); }
        }

        public static readonly RoutedEvent SpeedRatioChangedEvent =
            EventManager.RegisterRoutedEvent("SpeedRatioChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Link));

        public event RoutedEventHandler SpeedRatioChanged
        {
            add { AddHandler(SpeedRatioChangedEvent, value); }
            remove { RemoveHandler(SpeedRatioChangedEvent, value); }
        }

        public static readonly RoutedEvent BlinkingChangedEvent =
            EventManager.RegisterRoutedEvent("BlinkingChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Link));

        public event RoutedEventHandler BlinkingChanged
        {
            add { AddHandler(BlinkingChangedEvent, value); }
            remove { RemoveHandler(BlinkingChangedEvent, value); }
        }

        public static readonly RoutedEvent BlinkingSpeedRatioChangedEvent =
            EventManager.RegisterRoutedEvent("BlinkingSpeedRatioChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Link));

        public event RoutedEventHandler BlinkingRatioSpeedChanged
        {
            add { AddHandler(BlinkingSpeedRatioChangedEvent, value); }
            remove { RemoveHandler(BlinkingSpeedRatioChangedEvent, value); }
        }

        public static readonly RoutedEvent GraphicValuesChangedEvent =
            EventManager.RegisterRoutedEvent("GraphicValuesChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Link));

        public event RoutedEventHandler GraphicValuesChanged
        {
            add { AddHandler(GraphicValuesChangedEvent, value); }
            remove { RemoveHandler(GraphicValuesChangedEvent, value); }
        }

        #endregion

        public Link()
        {
            InitializeComponent();

            MinValue = double.MaxValue;
            MaxValue = double.MinValue;
            Period = TimeSpan.Zero;

            LinkArrowLength = 75;
            LinkDirection = Direction.Right;
            StrokeHeight = 16;
            LinkLength = double.NaN;

            Loaded += new RoutedEventHandler(Link_Loaded);
        }

        public void PauseAnimation()
        {
            m_PauseMoveAnimation = true;
            try
            {
                linkMoveAnimationClock.Controller.Pause();
            }
            catch { }
        }

        public void ResumeAnimation()
        {
            m_PauseMoveAnimation = false;
            try
            {
                linkMoveAnimationClock.Controller.Resume();
            }
            catch { }
        }

        private void BuildImage()
        {
            try
            {
                double length = IsVerticalOrientation ? ActualHeight : ActualWidth;

                DrawingVisual drawingVisual = new DrawingVisual();

                int lengthAdd = (int)(LinkArrowLength - length % LinkArrowLength);

                length += lengthAdd + LinkArrowLength;

                double imageWidth = IsVerticalOrientation ? ActualWidth : length;
                double imageHeight = IsHorizontalOrientation ? ActualHeight : length;

                if (imageWidth < 1)
                    imageWidth = 1;

                if (imageHeight < 1)
                    imageHeight = 1;

                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    LinearGradientBrush gradient = new LinearGradientBrush(Color1, Color2, IsHorizontalOrientation ? 0 : 90);
                    Pen pen = new Pen(gradient, 1);

                    double lineOffset = 0;
                    double lineOffsetStep = 3 / (StrokeHeight / 2F);

                    for (double j = 0; j <= StrokeHeight / 2; j += 0.5)
                    {
                        for (int i = 0; i < length; i += LinkArrowLength)
                        {
                            if (IsHorizontalOrientation)
                                drawingContext.DrawLine(pen, new Point(i + lineOffset, j), new Point(i + 75 + lineOffset, j));
                            else
                                drawingContext.DrawLine(pen, new Point(j, i + lineOffset), new Point(j, i + 75 + lineOffset));
                        }

                        lineOffset += lineOffsetStep;
                    }

                    lineOffset -= lineOffsetStep * 2;

                    for (double j = StrokeHeight / 2 + 0.5; j < StrokeHeight; j += 0.5)
                    {
                        for (int i = 0; i < length; i += LinkArrowLength)
                        {
                            if (IsHorizontalOrientation)
                                drawingContext.DrawLine(pen, new Point(i + lineOffset, j), new Point(i + 75 + lineOffset, j));
                            else
                                drawingContext.DrawLine(pen, new Point(j, i + lineOffset), new Point(j, i + 75 + lineOffset));
                        }

                        lineOffset -= lineOffsetStep;
                    }
                }

                RenderTargetBitmap bmp = new RenderTargetBitmap((int)imageWidth, (int)imageHeight, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(drawingVisual);

                image.Source = bmp;
            }
            catch { }
        }

        private void OnSpeedChanged()
        {
            double val = SpeedRatio;

            if (val > 0)
            {
                linkMoveAnimationClock.Controller.SpeedRatio = val;

                if (!m_PauseMoveAnimation)
                    ResumeAnimation();
            }
            else
            {
                PauseAnimation();
            }
        }

        private void OnBlinkChanged()
        {
            if (Blinking)
            {
                linkBlinkAnimationClock.Controller.Resume();
                linkBlinkAnimationClock.Controller.SpeedRatio = BlinkingSpeedRatio;
            }
            else
            {
                linkBlinkAnimationClock.Controller.Seek(TimeSpan.FromSeconds(0), TimeSeekOrigin.BeginTime);
                linkBlinkAnimationClock.Controller.Pause();
            }
        }

        private Point LogicalToPhysical(DateTime TimeStamp, double Value)
        {
            double timeStampSecond = (TimeStamp - m_MinTimeStamp).TotalSeconds;
            if (IsHorizontalOrientation)
                return new Point(timeStampSecond * m_kx, (m_MaxValue - Value) * m_ky);
            else
                return new Point((m_MaxValue - Value) * m_ky, timeStampSecond * m_kx);
        }

        private void CalculateCoefs()
        {
            NumberFormatInfo nfi = (CultureInfo.CurrentCulture.Clone() as CultureInfo).NumberFormat;
            nfi.NumberDecimalSeparator = ".";

            m_MinValue = MinValue;
            m_MaxValue = MaxValue;

            m_MinTimeStamp = DateTime.MaxValue;
            m_MaxTimeStamp = DateTime.MinValue;

            if (GraphicValues == null)
                return;

            m_Graphic.Clear();

            foreach (XmlElement value in GraphicValues)
            {
                DateTime timeStamp;
                double val;

                if (DateTime.TryParse(value.GetAttribute("timeStamp"), out timeStamp) && double.TryParse(value.InnerText, NumberStyles.Float, nfi, out val))
                {
                    if (timeStamp < m_MinTimeStamp)
                        m_MinTimeStamp = timeStamp;

                    if (timeStamp > m_MaxTimeStamp)
                        m_MaxTimeStamp = timeStamp;

                    m_MinValue = Math.Min(val, m_MinValue);
                    m_MaxValue = Math.Max(val, m_MaxValue);
                }
            }

            if (Period != TimeSpan.Zero)
                m_MinTimeStamp = m_MaxTimeStamp - Period;

            foreach (XmlElement value in GraphicValues)
            {
                DateTime timeStamp;
                double val;

                if (DateTime.TryParse(value.GetAttribute("timeStamp"), out timeStamp) && double.TryParse(value.InnerText, NumberStyles.Number, nfi, out val))
                {
                    if (timeStamp >= m_MinTimeStamp)
                        m_Graphic.Add(timeStamp, val);
                }
            }
        }

        private void RedrawGraphic()
        {
            try
            {
                CalculateCoefs();

                if (IsHorizontalOrientation)
                {
                    m_kx = (ActualWidth) / (m_MaxTimeStamp - m_MinTimeStamp).TotalSeconds;
                    m_ky = (ActualHeight) / (m_MaxValue - m_MinValue);
                }
                else
                {
                    m_kx = (ActualHeight) / (m_MaxTimeStamp - m_MinTimeStamp).TotalSeconds;
                    m_ky = (ActualWidth) / (m_MaxValue - m_MinValue);
                }

                DrawingVisual drawingVisual = new DrawingVisual();
                using (DrawingContext drawingContext = drawingVisual.RenderOpen())
                {
                    Point prevPoint = new Point();
                    bool firstPoint = true;

                    foreach (KeyValuePair<DateTime, double> value in m_Graphic)
                    {
                        Point p = LogicalToPhysical(value.Key, value.Value);

                        if (!firstPoint)
                            drawingContext.DrawLine(new Pen(Brushes.White, 1), prevPoint, p);

                        prevPoint = p;
                        firstPoint = false;
                    }
                }

                RenderTargetBitmap bmp = new RenderTargetBitmap((int)ActualWidth, (int)ActualHeight, 96, 96, PixelFormats.Pbgra32);
                bmp.Render(drawingVisual);

                graphicImage.Source = bmp;
            }
            catch { }
        }

        private void Link_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation linkMoveAnimation;

            if (LinkDirection == Direction.Left || LinkDirection == Direction.Up)
            {
                linkMoveAnimation = new DoubleAnimation(0, -LinkArrowLength, TimeSpan.FromSeconds(1));
                linkMoveAnimation.RepeatBehavior = RepeatBehavior.Forever;
                linkMoveAnimationClock = linkMoveAnimation.CreateClock();
            }
            else
            {
                linkMoveAnimation = new DoubleAnimation(-LinkArrowLength, 0, TimeSpan.FromSeconds(1));
                linkMoveAnimation.RepeatBehavior = RepeatBehavior.Forever;
                linkMoveAnimationClock = linkMoveAnimation.CreateClock();
            }

            PauseAnimation();

            DoubleAnimation linkBlinkAnimation = new DoubleAnimation(0.1, TimeSpan.FromSeconds(1));
            linkBlinkAnimation.RepeatBehavior = RepeatBehavior.Forever;
            linkBlinkAnimation.AutoReverse = true;
            linkBlinkAnimationClock = linkBlinkAnimation.CreateClock();

            if (IsHorizontalOrientation)
            {
                if (!double.IsNaN(LinkLength))
                    Width = LinkLength;

                Height = StrokeHeight;
            }
            else
            {
                if (!double.IsNaN(LinkLength))
                    Height = LinkLength;

                Width = StrokeHeight;
            }

            switch (LinkDirection)
            {
                case Direction.Left:
                    image.ApplyAnimationClock(Canvas.LeftProperty, linkMoveAnimationClock);
                    flipTransform.ScaleX = -1;
                    break;

                case Direction.Right:
                    image.ApplyAnimationClock(Canvas.LeftProperty, linkMoveAnimationClock);
                    break;

                case Direction.Down:
                    image.ApplyAnimationClock(Canvas.TopProperty, linkMoveAnimationClock);
                    break;

                case Direction.Up:
                    flipTransform.ScaleY = -1;
                    image.ApplyAnimationClock(Canvas.TopProperty, linkMoveAnimationClock);
                    break;
            }

            if (IsHorizontalOrientation)
                blickRectangle.EndPoint = new Point(0, 1);
            else
                blickRectangle.EndPoint = new Point(1, 0);

            image.ApplyAnimationClock(Image.OpacityProperty, linkBlinkAnimationClock);


            SizeChanged += new SizeChangedEventHandler(Link2_SizeChanged);
            SpeedRatioChanged += new RoutedEventHandler(Link_SpeedChanged);

            Color1Changed += new RoutedEventHandler(Link_Color1Changed);
            Color2Changed += new RoutedEventHandler(Link_Color2Changed);

            BlinkingChanged += new RoutedEventHandler(Link_BlinkingChanged);
            BlinkingRatioSpeedChanged += new RoutedEventHandler(Link_BlinkingSpeedChanged);

            GraphicValuesChanged += new RoutedEventHandler(Link_GraphicValuesChanged);

            OnBlinkChanged();
            OnSpeedChanged();
            BuildImage();
            RedrawGraphic();
        }

        private void Link_GraphicValuesChanged(object sender, RoutedEventArgs e)
        {
            RedrawGraphic();
        }

        private void Link_BlinkingSpeedChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkChanged();
        }

        private void Link_BlinkingChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkChanged();
        }

        private void Link_Color1Changed(object sender, RoutedEventArgs e)
        {
            BuildImage();
        }

        private void Link_Color2Changed(object sender, RoutedEventArgs e)
        {
            BuildImage();
        }

        private void Link2_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BuildImage();
        }

        private void Link_SpeedChanged(object sender, RoutedEventArgs e)
        {
            OnSpeedChanged();
        }
    }
}