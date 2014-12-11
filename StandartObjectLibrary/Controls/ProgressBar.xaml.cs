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
using System.Windows.Media.Animation;
using System.ComponentModel;
using DashboardEngine;

namespace StandartObjectLibrary
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class ProgressBar : UserControl
    {
        #region Properties

        private PathFigure clipPathFigure;
        private AnimationClock progressBarBlinkAnimationClock;

        [Category("Progress Bar Properties")]
        public ColorFill FillSource { get; set; }

        [Category("Progress Bar Properties")]
        public string MeasureUnit { get; set; }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(ProgressBar),
            new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnValueChangedCallback), new CoerceValueCallback(OnCoerceValueCallback)));

        [Bindable(true), Category("Progress Bar Properties")]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty FillBrushProperty = DependencyProperty.Register("FillBrush", typeof(Brush), typeof(ProgressBar),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(FillBrushChangedCallback)));

        [Bindable(true), Category("Progress Bar Properties")]
        public Brush FillBrush
        {
            get { return (Brush)GetValue(FillBrushProperty); }
            set { SetValue(FillBrushProperty, value); }
        }

        public static readonly DependencyProperty UpperLimitProperty =
            DependencyProperty.Register("UpperLimit", typeof(double), typeof(ProgressBar),
            new FrameworkPropertyMetadata((double)100, new PropertyChangedCallback(OnUpperLimitChangedCallback)));

        [Bindable(true), Category("Progress Bar Properties")]
        public double UpperLimit
        {
            get { return (double)GetValue(UpperLimitProperty); }
            set { SetValue(UpperLimitProperty, value); }
        }

        public static readonly DependencyProperty LowerLimitProperty =
            DependencyProperty.Register("LowerLimit", typeof(double), typeof(ProgressBar),
            new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnLowerLimitChangedCallback)));

        [Bindable(true), Category("Progress Bar Properties")]
        public double LowerLimit
        {
            get { return (double)GetValue(LowerLimitProperty); }
            set { SetValue(LowerLimitProperty, value); }
        }

        public static readonly DependencyProperty BlinkingProperty =
            DependencyProperty.Register("Blinking", typeof(bool), typeof(ProgressBar),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnBlinkingChangedCallback)));

        [Bindable(true), Category("Progress Bar Properties")]
        public bool Blinking
        {
            get { return (bool)GetValue(BlinkingProperty); }
            set { SetValue(BlinkingProperty, value); }
        }

        public static readonly DependencyProperty BlinkingSpeedRatioProperty =
            DependencyProperty.Register("BlinkingSpeedRatio", typeof(double), typeof(ProgressBar),
            new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnBlinkingSpeedRatioChangedCallback),
                new CoerceValueCallback(CoerceBlinkingSpeedRatioCallback)));


        [Bindable(true), Category("Progress Bar Properties")]
        public double BlinkingSpeedRatio
        {
            get { return (double)GetValue(BlinkingSpeedRatioProperty); }
            set { SetValue(BlinkingSpeedRatioProperty, value); }
        }

        #endregion

        #region Callbacks

        private static void OnValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(ValueChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static object OnCoerceValueCallback(DependencyObject d, object val)
        {
            ProgressBar progressBar = d as ProgressBar;
            double value = (double)val;

            if (value < progressBar.LowerLimit)
                return progressBar.LowerLimit;

            if (value > progressBar.UpperLimit)
                return progressBar.UpperLimit;

            return val;
        }

        private static void FillBrushChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(FillBrushChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static void OnUpperLimitChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(UpperLimitChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static void OnLowerLimitChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(LowerLimitChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
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

        private static object CoerceBlinkingSpeedRatioCallback(DependencyObject d, object o)
        {
            return Math.Max((double)0, (double)o);
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(ProgressBar));

        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        public static readonly RoutedEvent FillBrushChangedEvent =
            EventManager.RegisterRoutedEvent("FillBrushChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(ProgressBar));

        public event RoutedEventHandler FillBrushChanged
        {
            add { AddHandler(FillBrushChangedEvent, value); }
            remove { RemoveHandler(FillBrushChangedEvent, value); }
        }

        public static readonly RoutedEvent UpperLimitChangedEvent =
            EventManager.RegisterRoutedEvent("UpperLimitChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(ProgressBar));

        public event RoutedEventHandler UpperLimitChanged
        {
            add { AddHandler(UpperLimitChangedEvent, value); }
            remove { RemoveHandler(UpperLimitChangedEvent, value); }
        }

        public static readonly RoutedEvent LowerLimitChangedEvent =
            EventManager.RegisterRoutedEvent("LowerLimitChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(ProgressBar));

        public event RoutedEventHandler LowerLimitChanged
        {
            add { AddHandler(LowerLimitChangedEvent, value); }
            remove { RemoveHandler(LowerLimitChangedEvent, value); }
        }

        public static readonly RoutedEvent BlinkingChangedEvent =
            EventManager.RegisterRoutedEvent("BlinkingChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(ProgressBar));

        public event RoutedEventHandler BlinkingChanged
        {
            add { AddHandler(BlinkingChangedEvent, value); }
            remove { RemoveHandler(BlinkingChangedEvent, value); }
        }

        public static readonly RoutedEvent BlinkingSpeedRatioChangedEvent =
            EventManager.RegisterRoutedEvent("BlinkingSpeedRatioChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(ProgressBar));

        public event RoutedEventHandler BlinkingRatioSpeedChanged
        {
            add { AddHandler(BlinkingSpeedRatioChangedEvent, value); }
            remove { RemoveHandler(BlinkingSpeedRatioChangedEvent, value); }
        }

        #endregion

        public ProgressBar()
        {
            InitializeComponent();

            CreateGradient();

            DoubleAnimation progressBarBlinkAnimation = new DoubleAnimation(0.1, TimeSpan.FromSeconds(1));
            progressBarBlinkAnimation.RepeatBehavior = RepeatBehavior.Forever;
            progressBarBlinkAnimation.AutoReverse = true;

            progressBarBlinkAnimationClock = progressBarBlinkAnimation.CreateClock();

            rect.ApplyAnimationClock(Rectangle.OpacityProperty, progressBarBlinkAnimationClock);

            MeasureUnit = string.Empty;

            Loaded += new RoutedEventHandler(ProgressBar_Loaded);
        }

        private void ProgressBar_Loaded(object sender, RoutedEventArgs e)
        {
            CreateGradient();

            ValueChanged += new RoutedEventHandler(ProgressBar_ValueChanged);
            FillBrushChanged += new RoutedEventHandler(ProgressBar_FillBrushChanged);
            BlinkingChanged += new RoutedEventHandler(ProgressBar_BlinkingChanged);
            BlinkingRatioSpeedChanged += new RoutedEventHandler(ProgressBar_BlinkingRatioSpeedChanged);

            SizeChanged += new SizeChangedEventHandler(ProgressBar_SizeChanged);

            OnValueChanged();
            OnBlinkingChanged();
        }

        private void ProgressBar_FillBrushChanged(object sender, RoutedEventArgs e)
        {
            OnValueChanged();
        }

        private void ProgressBar_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            CreateGradient();
            OnValueChanged();
        }

        private void ProgressBar_BlinkingRatioSpeedChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkingChanged();
        }

        private void ProgressBar_BlinkingChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkingChanged();
        }

        private void ProgressBar_ValueChanged(object sender, RoutedEventArgs e)
        {
            OnValueChanged();
        }

        private void CreateGradient()
        {
            double difference = UpperLimit - LowerLimit;

            clipPathFigure = new PathFigure();
            clipPathFigure.StartPoint = new Point(0, 0);
            clipPathFigure.Segments.Add(new LineSegment(new Point(rect.ActualWidth, 0), false));
            clipPathFigure.Segments.Add(new LineSegment(new Point(rect.ActualWidth, rect.ActualHeight), false));
            clipPathFigure.Segments.Add(new LineSegment(new Point(0, rect.ActualHeight), false));
            clipPathFigure.Segments.Add(new LineSegment(new Point(0, 0), false));

            PathGeometry clipPathGeometry = new PathGeometry();
            clipPathGeometry.Figures.Add(clipPathFigure);

            rect.Clip = clipPathGeometry;
        }

        private void OnValueChanged()
        {
            if (FillSource != null)
            {
                Brush fillBrush = FillSource.GetBrushAt(Value);
                rect.Background = fillBrush;
                border.BorderBrush = fillBrush;
            }
            else
            {
                Color fillBrushColor = (FillBrush as SolidColorBrush).Color;
                rect.Background = new SolidColorBrush(ColorHelper.ChangeColor(fillBrushColor, 0.5F, 1));

                border.BorderBrush = FillBrush;
            }

            double k = ActualWidth / (UpperLimit - LowerLimit);

            double fillLength = k * (Value - LowerLimit);

            if (clipPathFigure != null && clipPathFigure.Segments.Count > 0)
            {
                LineSegment clipSeg1 = (LineSegment)clipPathFigure.Segments[0];
                LineSegment clipSeg2 = (LineSegment)clipPathFigure.Segments[1];

                clipSeg1.Point = new Point(fillLength, 0);
                clipSeg2.Point = new Point(fillLength, ActualHeight);

                if (FillSource != null)
                {
                    int count = FillSource.Items.Count;
                    for (int i = 0; i < count; i++)
                    {
                        ColorInterval interval1 = FillSource.Items[i];
                        ColorInterval interval2 = FillSource.Items[i];

                        if (i < FillSource.Items.Count - 1)
                            interval2 = FillSource.Items[i + 1];

                        double currentValue = interval1.Value;
                        double nextValue = interval2.Value;

                        if (currentValue <= Value && (Value < nextValue || i == count - 1))
                        {
                            BlinkingChanged -= ProgressBar_BlinkingChanged;
                            Blinking = interval1.Blinking;
                            BlinkingChanged += ProgressBar_BlinkingChanged;

                            BlinkingSpeedRatio = interval1.BlinkingSpeedRatio;
                        }
                    }
                }
            }
        }

        private void OnBlinkingChanged()
        {
            switch (Blinking)
            {
                case true:
                    progressBarBlinkAnimationClock.Controller.SpeedRatio = BlinkingSpeedRatio;
                    progressBarBlinkAnimationClock.Controller.Resume();
                    break;

                case false:
                    progressBarBlinkAnimationClock.Controller.Seek(TimeSpan.FromSeconds(0), TimeSeekOrigin.BeginTime);
                    progressBarBlinkAnimationClock.Controller.Pause();
                    break;
            }
        }
    }
}