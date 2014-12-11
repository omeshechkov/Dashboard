using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Markup;

namespace StandartObjectLibrary
{
    public partial class Ellipse
    {
        #region Properties

        private AnimationClock ellipseBlinkAnimationClock;

        [Category("Ellipse Properties")]
        public BrushFill FillSource { get; set; }

        [Category("Ellipse Properties")]
        public int RoundPrecision { get; set; }

        [Category("Ellipse Properties")]
        public string PrefixLabel { get; set; }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(double), typeof(Ellipse),
            new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnValueChangedCallback)));

        [Bindable(true), Category("Ellipse Properties")]
        public double Value
        {
            get { return (double)GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        public static readonly DependencyProperty FillProperty = DependencyProperty.Register("Fill", typeof(Brush), typeof(StandartObjectLibrary.Ellipse),
            new FrameworkPropertyMetadata(Brushes.Green, new PropertyChangedCallback(OnFillChangedCallback)));

        [Bindable(true), Category("Ellipse Properties")]
        public Brush Fill
        {
            get { return (Brush)GetValue(FillProperty); }
            set { SetValue(FillProperty, value); }
        }

        public static readonly DependencyProperty ShowValueProperty = DependencyProperty.Register("ShowValue", typeof(bool), typeof(StandartObjectLibrary.Ellipse),
            new FrameworkPropertyMetadata(true, new PropertyChangedCallback(OnShowValueChangedCallback)));

        [Bindable(true), Category("Ellipse Properties")]
        public bool ShowValue
        {
            get { return (bool)GetValue(ShowValueProperty); }
            set { SetValue(ShowValueProperty, value); }
        }

        public static readonly DependencyProperty BlinkingProperty = DependencyProperty.Register("Blinking", typeof(bool), typeof(StandartObjectLibrary.Ellipse),
            new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnBlinkingChangedCallback)));

        [Bindable(true), Category("Ellipse Properties")]
        public bool Blinking
        {
            get { return (bool)GetValue(BlinkingProperty); }
            set { SetValue(BlinkingProperty, value); }
        }

        public static readonly DependencyProperty BlinkingSpeedRatioProperty = DependencyProperty.Register("BlinkingSpeedRatio", typeof(double), typeof(StandartObjectLibrary.Ellipse),
            new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnBlinkingSpeedRatioChangedCallback),
                new CoerceValueCallback(CoerceBlinkingSpeedRatioCallback)));


        [Bindable(true), Category("Ellipse Properties")]
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

        private static void OnFillChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(FillChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static void OnShowValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(FillChangedEvent);
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
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(StandartObjectLibrary.Ellipse));

        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        public static readonly RoutedEvent FillChangedEvent =
            EventManager.RegisterRoutedEvent("FillChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(StandartObjectLibrary.Ellipse));

        public event RoutedEventHandler FillChanged
        {
            add { AddHandler(FillChangedEvent, value); }
            remove { RemoveHandler(FillChangedEvent, value); }
        }

        public static readonly RoutedEvent ShowValueChangedEvent =
            EventManager.RegisterRoutedEvent("ShowValueChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(StandartObjectLibrary.Ellipse));

        public event RoutedEventHandler ShowValueChanged
        {
            add { AddHandler(ShowValueChangedEvent, value); }
            remove { RemoveHandler(ShowValueChangedEvent, value); }
        }

        public static readonly RoutedEvent BlinkingChangedEvent =
            EventManager.RegisterRoutedEvent("BlinkingChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(StandartObjectLibrary.Ellipse));

        public event RoutedEventHandler BlinkingChanged
        {
            add { AddHandler(BlinkingChangedEvent, value); }
            remove { RemoveHandler(BlinkingChangedEvent, value); }
        }

        public static readonly RoutedEvent BlinkingSpeedRatioChangedEvent =
            EventManager.RegisterRoutedEvent("BlinkingSpeedRatioChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(StandartObjectLibrary.Ellipse));

        public event RoutedEventHandler BlinkingSpeedRatioChanged
        {
            add { AddHandler(BlinkingSpeedRatioChangedEvent, value); }
            remove { RemoveHandler(BlinkingSpeedRatioChangedEvent, value); }
        }

        #endregion

        public Ellipse()
        {
            InitializeComponent();

            DoubleAnimation animation = new DoubleAnimation(1, 0.1, TimeSpan.FromSeconds(1));
            animation.AutoReverse = true;
            animation.RepeatBehavior = RepeatBehavior.Forever;

            ellipseBlinkAnimationClock = animation.CreateClock();
            this.ApplyAnimationClock(UIElement.OpacityProperty, ellipseBlinkAnimationClock);

            RoundPrecision = -1;
            PrefixLabel = string.Empty;

            Loaded += new RoutedEventHandler(Ellipse_Loaded);
        }

        private void OnValueChanged()
        {
            textBlock1.Text = PrefixLabel;
            textBlock2.Text = PrefixLabel;

            if (RoundPrecision > -1)
            {
                textBlock1.Text += Math.Round(Value, RoundPrecision).ToString();
                textBlock2.Text += Math.Round(Value, RoundPrecision).ToString();
            }
            else
            {
                textBlock1.Text += Value.ToString();
                textBlock2.Text += Value.ToString();
            }

            if (FillSource != null)
            {
                BrushInterval brushInterval = FillSource[Value];
                ellipse.Fill = brushInterval.Brush;

                BlinkingChanged -= Ellipse_BlinkingChanged;
                Blinking = brushInterval.Blinking;
                BlinkingChanged += Ellipse_BlinkingChanged;

                BlinkingSpeedRatio = brushInterval.BlinkingSpeedRatio;
            }
            else
            {
                ellipse.Fill = Fill;
            }
        }

        private void OnBlinkingChanged()
        {
            switch (Blinking)
            {
                case true:
                    ellipseBlinkAnimationClock.Controller.Resume();
                    ellipseBlinkAnimationClock.Controller.SpeedRatio = BlinkingSpeedRatio;
                    break;

                case false:
                    ellipseBlinkAnimationClock.Controller.Seek(TimeSpan.FromSeconds(0), TimeSeekOrigin.BeginTime);
                    ellipseBlinkAnimationClock.Controller.Pause();
                    break;
            }
        }

        private void Ellipse_Loaded(object sender, RoutedEventArgs e)
        {
            ValueChanged += new RoutedEventHandler(Ellipse_ValueChanged);
            FillChanged += new RoutedEventHandler(Ellipse_FillChanged);

            BlinkingChanged += new RoutedEventHandler(Ellipse_BlinkingChanged);
            BlinkingSpeedRatioChanged += new RoutedEventHandler(Ellipse_BlinkingSpeedRatioChanged);

            OnValueChanged();
            OnBlinkingChanged();
        }

        private void Ellipse_FillChanged(object sender, RoutedEventArgs e)
        {
            ellipse.Fill = Fill;
        }

        private void Ellipse_BlinkingSpeedRatioChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkingChanged();
        }

        private void Ellipse_BlinkingChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkingChanged();
        }

        private void Ellipse_ValueChanged(object sender, RoutedEventArgs e)
        {
            OnValueChanged();
        }
    }
}