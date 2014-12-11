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

namespace StandartObjectLibrary
{
    /// <summary>
    /// Interaction logic for Cooler.xaml
    /// </summary>
    public partial class Cooler : UserControl
    {
        #region Properties

        private AnimationClock coolerRotateAnimationClock;
        private AnimationClock coolerBlinkAnimationClock;

        private LinearGradientBrush rightTopArcBrush = new LinearGradientBrush(Colors.Black, Colors.Gray, 0);
        private LinearGradientBrush rightBottomArcBrush = new LinearGradientBrush(Colors.Black, Colors.Gray, 90);
        private LinearGradientBrush leftBottomArcBrush = new LinearGradientBrush(Colors.Gray, Colors.Black, 0);
        private LinearGradientBrush leftTopArcBrush = new LinearGradientBrush(Colors.Gray, Colors.Black, 90);
        private bool m_PauseRotateAnimation = false;

        #endregion

        #region Dependency Properties

        #region Color1

        public static readonly DependencyProperty Color1Property = DependencyProperty.Register("Color1", typeof(Color), typeof(Cooler),
           new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColor1ChangedCallback)));

        [Bindable(true), Category("Cooler Properties")]
        public Color Color1
        {
            get { return (Color)GetValue(Color1Property); }
            set { SetValue(Color1Property, value); }
        }

        private static void OnColor1ChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(Color1ChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        public static readonly RoutedEvent Color1ChangedEvent =
            EventManager.RegisterRoutedEvent("Color1Changed", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Cooler));

        public event RoutedEventHandler Color1Changed
        {
            add { AddHandler(Color1ChangedEvent, value); }
            remove { RemoveHandler(Color1ChangedEvent, value); }
        }

        #endregion

        #region Color2

        public static readonly DependencyProperty Color2Property = DependencyProperty.Register("Color2", typeof(Color), typeof(Cooler),
            new FrameworkPropertyMetadata(Colors.Gray, new PropertyChangedCallback(OnColor2ChangedCallback)));

        [Bindable(true), Category("Cooler Properties")]
        public Color Color2
        {
            get { return (Color)GetValue(Color2Property); }
            set { SetValue(Color2Property, value); }
        }

        private static void OnColor2ChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(Color2ChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        public static readonly RoutedEvent Color2ChangedEvent =
            EventManager.RegisterRoutedEvent("Color2Changed", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Cooler));

        public event RoutedEventHandler Color2Changed
        {
            add { AddHandler(Color2ChangedEvent, value); }
            remove { RemoveHandler(Color2ChangedEvent, value); }
        }

        #endregion

        #region StrokeHeight

        public static readonly DependencyProperty StrokeHeightProperty = DependencyProperty.Register("StrokeHeight", typeof(int), typeof(Cooler),
            new FrameworkPropertyMetadata(12, new PropertyChangedCallback(OnStrokeHeightChangedCallback), new CoerceValueCallback(CoerceStrokeHeightCallback)));

        [Bindable(true), Category("Cooler Properties")]
        public int StrokeHeight
        {
            get { return (int)GetValue(StrokeHeightProperty); }
            set { SetValue(StrokeHeightProperty, value); }
        }

        private static void OnStrokeHeightChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs args = new RoutedEventArgs(StrokeHeightChangedEvent);
            (d as FrameworkElement).RaiseEvent(args);
        }

        private static object CoerceStrokeHeightCallback(DependencyObject d, object o)
        {
            return Math.Max((int)0, (int)o);
        }

        public static readonly RoutedEvent StrokeHeightChangedEvent =
            EventManager.RegisterRoutedEvent("StrokeHeightChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Cooler));

        public event RoutedEventHandler StrokeHeightChanged
        {
            add { AddHandler(StrokeHeightChangedEvent, value); }
            remove { RemoveHandler(StrokeHeightChangedEvent, value); }
        }

        #endregion

        #region SpeedRatio

        public static readonly DependencyProperty SpeedRatioProperty = DependencyProperty.Register("SpeedRatio", typeof(double), typeof(Cooler),
            new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnSpeedRatioChangedCallback),
                new CoerceValueCallback(CoerceSpeedRatioCallback)));

        [Bindable(true), Category("Cooler Properties")]
        public double SpeedRatio
        {
            get { return (double)GetValue(SpeedRatioProperty); }
            set { SetValue(SpeedRatioProperty, value); }
        }

        private static void OnSpeedRatioChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(SpeedRatioChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static object CoerceSpeedRatioCallback(DependencyObject d, object o)
        {
            return Math.Max((double)0, (double)o);
        }

        public static readonly RoutedEvent SpeedRatioChangedEvent =
            EventManager.RegisterRoutedEvent("SpeedRatioChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Cooler));

        public event RoutedEventHandler SpeedRatioChanged
        {
            add { AddHandler(SpeedRatioChangedEvent, value); }
            remove { RemoveHandler(SpeedRatioChangedEvent, value); }
        }

        #endregion

        #region Value

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(Cooler),
            new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnValueChangedCallback)));

        [Bindable(true), Category("Cooler Properties")]
        public string Value
        {
            get { return (string)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(ValueChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Cooler));

        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        #endregion

        #region Blinking

        public static readonly DependencyProperty BlinkingProperty = DependencyProperty.Register("Blinking", typeof(bool), typeof(Cooler),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnBlinkingChangedCallback)));

        [Bindable(true), Category("Cooler Properties")]
        public bool Blinking
        {
            get { return (bool)GetValue(BlinkingProperty); }
            set { SetValue(BlinkingProperty, value); }
        }

        private static void OnBlinkingChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(BlinkingChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        public static readonly RoutedEvent BlinkingChangedEvent =
            EventManager.RegisterRoutedEvent("BlinkingChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Cooler));

        public event RoutedEventHandler BlinkingChanged
        {
            add { AddHandler(BlinkingChangedEvent, value); }
            remove { RemoveHandler(BlinkingChangedEvent, value); }
        }

        #endregion

        #region BlinkingSpeedRatio

        public static readonly DependencyProperty BlinkingSpeedRatioProperty = DependencyProperty.Register("BlinkingSpeedRatio", typeof(double), typeof(Cooler),
            new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnBlinkingSpeedRatioChangedCallback),
                new CoerceValueCallback(CoerceBlinkingSpeedRatioCallback)));


        [Bindable(true), Category("Cooler Properties")]
        public double BlinkingSpeedRatio
        {
            get { return (double)GetValue(BlinkingSpeedRatioProperty); }
            set { SetValue(BlinkingSpeedRatioProperty, value); }
        }

        private static void OnBlinkingSpeedRatioChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(BlinkingSpeedRatioChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static object CoerceBlinkingSpeedRatioCallback(DependencyObject d, object o)
        {
            return Math.Max((double)0, (double)o);
        }

        public static readonly RoutedEvent BlinkingSpeedRatioChangedEvent =
            EventManager.RegisterRoutedEvent("BlinkingSpeedRatioChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Cooler));

        public event RoutedEventHandler BlinkingSpeedRatioChanged
        {
            add { AddHandler(BlinkingSpeedRatioChangedEvent, value); }
            remove { RemoveHandler(BlinkingSpeedRatioChangedEvent, value); }
        }

        #endregion

        #endregion

        public Cooler()
        {
            InitializeComponent();

            DoubleAnimation coolerRotateAnimation = new DoubleAnimation(0, 360, TimeSpan.FromSeconds(3));
            coolerRotateAnimation.RepeatBehavior = RepeatBehavior.Forever;
            coolerRotateAnimationClock = coolerRotateAnimation.CreateClock();

            DoubleAnimation linkBlinkAnimation = new DoubleAnimation(0.1, TimeSpan.FromSeconds(1));
            linkBlinkAnimation.RepeatBehavior = RepeatBehavior.Forever;
            linkBlinkAnimation.AutoReverse = true;
            coolerBlinkAnimationClock = linkBlinkAnimation.CreateClock();

            image.ApplyAnimationClock(Image.OpacityProperty, coolerBlinkAnimationClock);

            Loaded += new RoutedEventHandler(Cooler_Loaded);
        }

        public void PauseAnimation()
        {
            m_PauseRotateAnimation = true;

            try
            {
                coolerRotateAnimationClock.Controller.Pause();
            }
            catch { }
        }

        public void ResumeAnimation()
        {
            m_PauseRotateAnimation = false;

            try
            {
                coolerRotateAnimationClock.Controller.Resume();
            }
            catch { }
        }

        private PathGeometry BuildArcGeometry(Point p1, Point p2, double radius)
        {
            if (radius < 0)
                radius = 0;

            PathGeometry arcGeometry = new PathGeometry();

            PathFigure figure = new PathFigure();
            figure.StartPoint = p1;
            figure.Segments.Add(new ArcSegment(p2, new Size(radius, radius), 0, false, SweepDirection.Clockwise, true));

            arcGeometry.Figures.Add(figure);

            return arcGeometry;
        }

        private void DrawPoint(DrawingContext drawingContext, Point p)
        {
            drawingContext.DrawEllipse(Brushes.Red, null, p, 1, 1);
        }

        private void DrawCircle(DrawingContext drawingContext, double offset, double angle)
        {
            double diameter = Math.Min(ActualWidth, ActualHeight);

            drawingContext.PushTransform(new RotateTransform(angle, diameter / 2, diameter / 2));

            Point topPoint = new Point(diameter / 2, offset + 1);
            Point rightPoint = new Point(diameter - offset - 1, diameter / 2);
            Point bottomPoint = new Point(diameter / 2, diameter - offset - 1);
            Point leftPoint = new Point(offset + 1, diameter / 2);

            //DrawPoint(drawingContext, topPoint);
            //DrawPoint(drawingContext, rightPoint);
            //DrawPoint(drawingContext, bottomPoint);
            //DrawPoint(drawingContext, leftPoint);

            Geometry rightTopArcGeometry = BuildArcGeometry(topPoint, rightPoint, diameter / 2 - offset - 1);
            Geometry rightBottomArcGeometry = BuildArcGeometry(rightPoint, bottomPoint, diameter / 2 - offset - 1);
            Geometry leftBottomArcGeometry = BuildArcGeometry(bottomPoint, leftPoint, diameter / 2 - offset - 1);
            Geometry leftTopArcGeometry = BuildArcGeometry(leftPoint, topPoint, diameter / 2 - offset - 1);

            drawingContext.DrawGeometry(null, new Pen(rightTopArcBrush, 2), rightTopArcGeometry);
            drawingContext.DrawGeometry(null, new Pen(rightBottomArcBrush, 2), rightBottomArcGeometry);
            drawingContext.DrawGeometry(null, new Pen(leftBottomArcBrush, 2), leftBottomArcGeometry);
            drawingContext.DrawGeometry(null, new Pen(leftTopArcBrush, 2), leftTopArcGeometry);

            drawingContext.Pop();
        }

        private void CalcClipPath()
        {
            double radius = Math.Min(ActualWidth, ActualHeight) / 2 - StrokeHeight - 3;

            if (radius < 0)
                radius = 0;

            innerClipPathEllipse.Center = new Point(ActualWidth / 2, ActualHeight / 2);
            innerClipPathEllipse.RadiusX = radius;
            innerClipPathEllipse.RadiusY = radius;
        }

        private void BuildImage()
        {
            rightTopArcBrush.GradientStops[0].Color = Color1;
            rightTopArcBrush.GradientStops[1].Color = Color2;

            rightBottomArcBrush.GradientStops[0].Color = Color1;
            rightBottomArcBrush.GradientStops[1].Color = Color2;

            leftBottomArcBrush.GradientStops[1].Color = Color1;
            leftBottomArcBrush.GradientStops[0].Color = Color2;

            leftTopArcBrush.GradientStops[1].Color = Color1;
            leftTopArcBrush.GradientStops[0].Color = Color2;

            CalcClipPath();

            double diameter = Math.Min(ActualWidth, ActualHeight);

            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                double angle = 0;
                double angleStep = 5F / ((double)StrokeHeight / 2F);

                for (double i = 0; i < StrokeHeight / 2; i += 0.5)
                {
                    DrawCircle(drawingContext, i, angle);

                    angle += angleStep;
                }

                angle -= angleStep;

                for (double i = StrokeHeight / 2 + 0.5; i < StrokeHeight; i += 0.5)
                {
                    DrawCircle(drawingContext, i, angle);

                    angle -= angleStep;
                }
            }

            

            if (diameter < 1)
                diameter = 1;

            RenderTargetBitmap bmp = new RenderTargetBitmap((int)diameter, (int)diameter, 96, 96, PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            image.Source = bmp;
        }

        private void OnSpeedChanged()
        {
            double val = SpeedRatio;

            if (val > 0)
            {
                coolerRotateAnimationClock.Controller.SpeedRatio = val;

                if (!m_PauseRotateAnimation)
                    coolerRotateAnimationClock.Controller.Resume();
            }
            else
            {
                coolerRotateAnimationClock.Controller.Pause();
            }
        }

        private void OnBlinkChanged()
        {
            if (Blinking)
            {
                coolerBlinkAnimationClock.Controller.Resume();
                coolerBlinkAnimationClock.Controller.SpeedRatio = BlinkingSpeedRatio;
            }
            else
            {
                coolerBlinkAnimationClock.Controller.Seek(TimeSpan.FromSeconds(0), TimeSeekOrigin.BeginTime);
                coolerBlinkAnimationClock.Controller.Pause();
            }
        }

        private void OnValueChnaged()
        {
            valueTextBlock.Text = Value;
            valueTextBlock.InvalidateMeasure();

            CalcClipPath();
        }

        private void CalculateTransform()
        {
            rotateTransform.CenterX = ActualWidth / 2;
            rotateTransform.CenterY = ActualHeight / 2;
        }


        private void Cooler_Loaded(object sender, RoutedEventArgs e)
        {
            SpeedRatioChanged += new RoutedEventHandler(Cooler_SpeedRatioChanged);
            StrokeHeightChanged += new RoutedEventHandler(Cooler_StrokeHeightChanged);

            Color1Changed += new RoutedEventHandler(Cooler_Color1Changed);
            Color2Changed += new RoutedEventHandler(Cooler_Color2Changed);

            BlinkingChanged += new RoutedEventHandler(Cooler_BlinkingChanged);
            BlinkingSpeedRatioChanged += new RoutedEventHandler(Cooler_BlinkingSpeedRatioChanged);
            ValueChanged += new RoutedEventHandler(Cooler_ValueChanged);

            CalculateTransform();
            BuildImage();

            rotateTransform.CenterX = 0;
            rotateTransform.CenterY = 0;

            rotateTransform.ApplyAnimationClock(RotateTransform.AngleProperty, coolerRotateAnimationClock);

            OnBlinkChanged();
            OnSpeedChanged();
            OnValueChnaged();
        }

        private void Cooler_ValueChanged(object sender, RoutedEventArgs e)
        {
            OnValueChnaged();
        }

        private void Cooler_StrokeHeightChanged(object sender, RoutedEventArgs e)
        {
            BuildImage();
        }

        private void Cooler_BlinkingSpeedRatioChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkChanged();
        }

        private void Cooler_BlinkingChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkChanged();
        }

        private void Cooler_Color1Changed(object sender, RoutedEventArgs e)
        {
            BuildImage();
        }

        private void Cooler_Color2Changed(object sender, RoutedEventArgs e)
        {
            BuildImage();
        }

        private void Cooler_SpeedRatioChanged(object sender, RoutedEventArgs e)
        {
            OnSpeedChanged();
        }
    }
}