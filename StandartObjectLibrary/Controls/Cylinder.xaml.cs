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
using DashboardEngine;

namespace StandartObjectLibrary
{
    /// <summary>
    /// Interaction logic for Cylinder.xaml
    /// </summary>
    public partial class Cylinder
    {
        #region Properties

        private AnimationClock fillGeometryAnimationClock;
        private AnimationClock highlightEllipseAnimationClock;

        [Category("Cylinder Properties")]
        public ColorFill FillSource { get; set; }

        [Category("Cylinder Properties")]
        public double LowerLimit { get; set; }

        [Category("Cylinder Properties")]
        public double UpperLimit { get; set; }

        #endregion

        #region Dependency Properties

        #region Value

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(Cylinder),
            new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnValueChangedCallback), new CoerceValueCallback(OnCoerceValueCallback)));

        [Bindable(true), Category("Cylinder Properties")]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(ValueChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static object OnCoerceValueCallback(DependencyObject d, object value)
        {
            Cylinder e = d as Cylinder;

            if (double.IsNaN((double)value))
                return e.LowerLimit;

            if ((double)value < e.LowerLimit)
                return e.LowerLimit;

            if ((double)value > e.UpperLimit)
                return e.UpperLimit;

            return value;
        }

        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Cylinder));

        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        #endregion

        #region FillBrush

        public static readonly DependencyProperty FillBrushProperty = DependencyProperty.Register("FillBrush", typeof(Brush), typeof(Cylinder),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(FillBrushChangedCallback)));

        [Bindable(true), Category("Cylinder Properties")]
        public Brush FillBrush
        {
            get { return (Brush)GetValue(FillBrushProperty); }
            set { SetValue(FillBrushProperty, value); }
        }

        private static void FillBrushChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(FillBrushChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        public static readonly RoutedEvent FillBrushChangedEvent =
            EventManager.RegisterRoutedEvent("FillBrushChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Cylinder));

        public event RoutedEventHandler FillBrushChanged
        {
            add { AddHandler(FillBrushChangedEvent, value); }
            remove { RemoveHandler(FillBrushChangedEvent, value); }
        }

        #endregion

        #region Blinking 

        public static readonly DependencyProperty BlinkingProperty = 
            DependencyProperty.Register("Blinking", typeof(bool), typeof(Cylinder),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnBlinkingChangedCallback)));

        [Bindable(true), Category("Cylinder Properties")]
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
            EventManager.RegisterRoutedEvent("BlinkingChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Cylinder));

        public event RoutedEventHandler BlinkingChanged
        {
            add { AddHandler(BlinkingChangedEvent, value); }
            remove { RemoveHandler(BlinkingChangedEvent, value); }
        }

        #endregion

        #region BlinkingSpeedRatio

        public static readonly DependencyProperty BlinkingSpeedRatioProperty = 
            DependencyProperty.Register("BlinkingSpeedRatio", typeof(double), typeof(Cylinder),
            new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnBlinkingSpeedRatioChangedCallback), new CoerceValueCallback(CoerceBlinkingSpeedRatioCallback)));


        [Bindable(true), Category("Cylinder Properties")]
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
            EventManager.RegisterRoutedEvent("BlinkingSpeedRatioChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(Cylinder));

        public event RoutedEventHandler BlinkingSpeedRatioChanged
        {
            add { AddHandler(BlinkingSpeedRatioChangedEvent, value); }
            remove { RemoveHandler(BlinkingSpeedRatioChangedEvent, value); }
        }

        #endregion

        #endregion

        public Cylinder()
        {
            this.InitializeComponent();

            BorderBrush = Brushes.White;

            DoubleAnimation fillGeometryAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
            fillGeometryAnimation.AutoReverse = true;
            fillGeometryAnimation.RepeatBehavior = RepeatBehavior.Forever;
            fillGeometryAnimationClock = fillGeometryAnimation.CreateClock();

            DoubleAnimation highiltEllipseAnimation = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1));
            highiltEllipseAnimation.AutoReverse = true;
            highiltEllipseAnimation.RepeatBehavior = RepeatBehavior.Forever;
            highlightEllipseAnimationClock = highiltEllipseAnimation.CreateClock();

            LowerLimit = 0;
            UpperLimit = 100;

            Loaded += new RoutedEventHandler(Cylinder_Loaded);
        }

        private void OnValueChanged()
        {
            if (Value > UpperLimit)
                Value = UpperLimit;

            if (Value < LowerLimit)
                Value = LowerLimit;

            if (FillSource != null)
            {
                Brush fillBrush = FillSource.GetBrushAt(Value).Clone();

                fillGeometry.Brush = fillBrush;
                highlightEllipse.Brush = fillBrush;
            }
            else
            {
                Color fillBrushColor = (FillBrush as SolidColorBrush).Color;
                fillGeometry.Brush = new SolidColorBrush(ColorHelper.ChangeColor(fillBrushColor, 0.5F, 1));

                highlightEllipse.Brush = FillBrush.Clone();

                SolidColorBrush solidColorBrush = (SolidColorBrush)FillBrush.Clone();
                SolidColorBrush darkenBrush = new SolidColorBrush(ColorHelper.ChangeColor(solidColorBrush.Color, 0.4F, -1));
                highlightEllipse.Pen = new Pen(darkenBrush, 1);
            }

            if (highlightEllipse.Brush != null)
                highlightEllipse.Brush.ApplyAnimationClock(Brush.OpacityProperty, highlightEllipseAnimationClock);

            if (fillGeometry.Brush != null)
                fillGeometry.Brush.ApplyAnimationClock(Brush.OpacityProperty, fillGeometryAnimationClock);

            double k = 82D / (UpperLimit - LowerLimit);

            double fillLength = k * (Value - LowerLimit);

            clipGeometry.Rect = new Rect(0, 91 - fillLength, 100, fillLength + 9);

            if (FillSource != null)
            {
                for (int i = 0; i < FillSource.Items.Count; i++)
                {
                    ColorInterval interval1 = FillSource.Items[i];
                    ColorInterval interval2 = FillSource.Items[i];

                    if (i < FillSource.Items.Count - 1)
                        interval2 = FillSource.Items[i + 1];

                    if (interval1.Value <= Value && (Value < interval2.Value || i == FillSource.Items.Count - 1))
                    {
                        double interval = interval2.Value - interval1.Value;

                        if (interval != 0)
                        {
                            BlinkingChanged -= Cylinder_BlinkingChanged;
                            Blinking = interval1.Blinking;
                            BlinkingChanged += Cylinder_BlinkingChanged;

                            BlinkingSpeedRatio = interval1.BlinkingSpeedRatio;
                        }
                        else
                        {
                            BlinkingChanged -= Cylinder_BlinkingChanged;
                            Blinking = interval1.Blinking;
                            BlinkingChanged += Cylinder_BlinkingChanged;

                            BlinkingSpeedRatio = interval1.BlinkingSpeedRatio;
                        }
                    }
                }
            }

            highlightEllipseGeometry.Center = new Point(50.1F, 91 - fillLength);
        }

        private void OnBlinkingChanged()
        {
            switch (Blinking)
            {
                case true:
                    fillGeometryAnimationClock.Controller.SpeedRatio = BlinkingSpeedRatio;
                    fillGeometryAnimationClock.Controller.Resume();

                    highlightEllipseAnimationClock.Controller.SpeedRatio = BlinkingSpeedRatio;
                    highlightEllipseAnimationClock.Controller.Resume();
                    break;

                case false:
                    fillGeometryAnimationClock.Controller.Seek(TimeSpan.FromSeconds(0), TimeSeekOrigin.BeginTime);
                    fillGeometryAnimationClock.Controller.Pause();

                    highlightEllipseAnimationClock.Controller.Seek(TimeSpan.FromSeconds(0), TimeSeekOrigin.BeginTime);
                    highlightEllipseAnimationClock.Controller.Pause();
                    break;
            }
        }

        private void Cylinder_Loaded(object sender, RoutedEventArgs e)
        {
            OnValueChanged();

            ValueChanged += new RoutedEventHandler(Cylinder_ValueChanged);
            FillBrushChanged += new RoutedEventHandler(Cylinder_FillBrushChanged);
            BlinkingChanged += new RoutedEventHandler(Cylinder_BlinkingChanged);
            BlinkingSpeedRatioChanged += new RoutedEventHandler(Cylinder_BlinkingSpeedRatioChanged);

            OnBlinkingChanged();
        }

        private void Cylinder_FillBrushChanged(object sender, RoutedEventArgs e)
        {
            OnValueChanged();
        }

        private void Cylinder_BlinkingSpeedRatioChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkingChanged();
        }

        private void Cylinder_BlinkingChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkingChanged();
        }

        private void Cylinder_ValueChanged(object sender, RoutedEventArgs e)
        {
            OnValueChanged();
        }
    }
}