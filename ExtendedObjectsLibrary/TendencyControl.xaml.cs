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
using System.Windows.Media.Animation;

namespace ExtendedObjectsLibrary
{
	public partial class TendencyControl
	{
        private AnimationClock controlBlinkAnimationClock;

        private GeometryDrawing upArrowGeometry = new GeometryDrawing();
        private GeometryDrawing downArrowGeometry = new GeometryDrawing();
        private GeometryDrawing equalGeometry = new GeometryDrawing();
        private GeometryDrawing errorGeometry = new GeometryDrawing();

        #region Dependency Properties

        public static readonly DependencyProperty TendencyStateProperty = DependencyProperty.Register("TendencyState", typeof(TendencyState), typeof(TendencyControl),
            new FrameworkPropertyMetadata(TendencyState.Up, new PropertyChangedCallback(OnTendencyStateChangedCallback)));

        public TendencyState TendencyState
        {
            get { return (TendencyState)GetValue(TendencyStateProperty); }
            set { SetValue(TendencyStateProperty, value); }
        }


        public static readonly DependencyProperty UpStateBrushProperty =
            DependencyProperty.Register("UpStateBrush", typeof(Brush), typeof(TendencyControl), new FrameworkPropertyMetadata(Brushes.Green));

        public Brush UpStateBrush
        {
            get { return (Brush)GetValue(UpStateBrushProperty); }
            set { SetValue(UpStateBrushProperty, value); }
        }


        public static readonly DependencyProperty EqualStatePenProperty =
            DependencyProperty.Register("EqualStatePen", typeof(Pen), typeof(TendencyControl), new FrameworkPropertyMetadata(new Pen(Brushes.Gray, 5) { LineJoin = PenLineJoin.Round }));

        public Pen EqualStatePen
        {
            get { return (Pen)GetValue(EqualStatePenProperty); }
            set { SetValue(EqualStatePenProperty, value); }
        }


        public static readonly DependencyProperty DownStateBrushProperty =
            DependencyProperty.Register("DownStateBrush", typeof(Brush), typeof(TendencyControl), new FrameworkPropertyMetadata(Brushes.Red));

        public Brush DownStateBrush
        {
            get { return (Brush)GetValue(DownStateBrushProperty); }
            set { SetValue(DownStateBrushProperty, value); }
        }


        public static readonly DependencyProperty ErrorStatePenProperty =
            DependencyProperty.Register("ErrorStatePen", typeof(Pen), typeof(TendencyControl), new FrameworkPropertyMetadata(new Pen(Brushes.Red, 2) { LineJoin = PenLineJoin.Round }));

        public Pen ErrorStatePen
        {
            get { return (Pen)GetValue(ErrorStatePenProperty); }
            set { SetValue(ErrorStatePenProperty, value); }
        }


        public static readonly DependencyProperty BlinkingProperty =
            DependencyProperty.Register("Blinking", typeof(bool), typeof(TendencyControl), new FrameworkPropertyMetadata(false, new PropertyChangedCallback(OnBlinkingChangedCallback)));

        public bool Blinking
        {
            get { return (bool)GetValue(BlinkingProperty); }
            set { SetValue(BlinkingProperty, value); }
        }



        public static readonly DependencyProperty BlinkingSpeedRatioProperty =
            DependencyProperty.Register("BlinkingSpeedRatio", typeof(double), typeof(TendencyControl), new FrameworkPropertyMetadata((double)0, new PropertyChangedCallback(OnBlinkingSpeedRationChangedCallback), new CoerceValueCallback(BlinkingSpeedRatioCoerceValueCallback)));

        public double BlinkingSpeedRatio
        {
            get { return (double)GetValue(BlinkingSpeedRatioProperty); }
            set { SetValue(BlinkingSpeedRatioProperty, value); }
        }

        #endregion

        #region Callbacks
        
        private static void OnTendencyStateChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(TendencyStateChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static void OnBlinkingChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(BlinkingChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static void OnBlinkingSpeedRationChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(BlinkingSpeedRatioChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        private static object BlinkingSpeedRatioCoerceValueCallback(DependencyObject d, object value)
        {
            double speedRatio = (double)value;

            if (speedRatio < 0)
                return 0;

            if (speedRatio > 10)
                return 10;

            return value;
        }

        #endregion

        #region Routed Events

        public static readonly RoutedEvent TendencyStateChangedEvent =
            EventManager.RegisterRoutedEvent("TendencyStateChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(TendencyControl));

        public event RoutedEventHandler TendencyStateChanged
        {
            add { AddHandler(TendencyStateChangedEvent, value); }
            remove { RemoveHandler(TendencyStateChangedEvent, value); }
        }

        public static readonly RoutedEvent BlinkingChangedEvent =
            EventManager.RegisterRoutedEvent("BlinkingChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(TendencyControl));

        public event RoutedEventHandler BlinkingChanged
        {
            add { AddHandler(BlinkingChangedEvent, value); }
            remove { RemoveHandler(BlinkingChangedEvent, value); }
        }

        public static readonly RoutedEvent BlinkingSpeedRatioChangedEvent =
            EventManager.RegisterRoutedEvent("BlinkingSpeedRatioChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(TendencyControl));

        public event RoutedEventHandler BlinkingSpeedRatioChanged
        {
            add { AddHandler(BlinkingSpeedRatioChangedEvent, value); }
            remove { RemoveHandler(BlinkingSpeedRatioChangedEvent, value); }
        }

        #endregion

        public TendencyControl()
		{
			this.InitializeComponent();

            upArrowGeometry = (Resources["upArrow"] as GeometryDrawing);
            upArrowGeometry.Brush = UpStateBrush;

            downArrowGeometry = (Resources["downArrow"] as GeometryDrawing);
            downArrowGeometry.Brush = DownStateBrush;

            GeometryGroup equalGeometryGroup = new GeometryGroup();
            equalGeometryGroup.Children.Add(Geometry.Parse("F1 M 3.72917,2.91667L 60.5625,2.91667L 60.5625,59.9167L 3.72917,59.9167L 3.72917,2.91667 Z"));
            equalGeometryGroup.Children.Add(Geometry.Parse("F1 M 31.6458,13.25C 38.8716,13.25 44.7292,21.7939 44.7292,32.3333C 44.7292,42.8728 38.8716,51.4167 31.6458,51.4167C 24.4201,51.4167 18.5625,42.8728 18.5625,32.3333C 18.5625,21.7939 24.4201,13.25 31.6458,13.25 Z"));
            equalGeometryGroup.Children.Add(Geometry.Parse("F1 M 39.8958,18.25L 23.7292,45.4167"));

            equalGeometry.Pen = EqualStatePen;
            equalGeometry.Geometry = equalGeometryGroup;

            GeometryGroup errorGeometryGroup = new GeometryGroup();
            errorGeometryGroup.Children.Add(Geometry.Parse("F1 M 3.72917,2.91667L 60.5625,2.91667L 60.5625,59.9167L 3.72917,59.9167L 3.72917,2.91667 Z"));
            errorGeometryGroup.Children.Add(Geometry.Parse("F1 M 3.72917,2.91667L 60.5625,59.9167"));
            errorGeometryGroup.Children.Add(Geometry.Parse("F1 M 60.5625,2.91667L 3.72917,59.9167"));

            errorGeometry.Geometry = errorGeometryGroup;
            errorGeometry.Pen = ErrorStatePen;

            drawingGroup.Children.Add(upArrowGeometry);

            DoubleAnimation animation = new DoubleAnimation(1, 0.1, TimeSpan.FromSeconds(1));
            animation.AutoReverse = true;
            animation.RepeatBehavior = RepeatBehavior.Forever;

            controlBlinkAnimationClock = animation.CreateClock();
            this.ApplyAnimationClock(UIElement.OpacityProperty, controlBlinkAnimationClock);

            Loaded += new RoutedEventHandler(TendencyControl_Loaded);
		}

        private void TendencyControl_Loaded(object sender, RoutedEventArgs e)
        {
            TendencyStateChanged += new RoutedEventHandler(TendencyControl_TendencyStateChanged);
            BlinkingChanged += new RoutedEventHandler(TendencyControl_BlinkingChanged);
            BlinkingSpeedRatioChanged += new RoutedEventHandler(TendencyControl_BlinkingSpeedRatioChanged);

            OnTendencyStateChanged();
            OnBlinkingChanged();
        }

        private void TendencyControl_BlinkingSpeedRatioChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkingChanged();
        }

        private void TendencyControl_BlinkingChanged(object sender, RoutedEventArgs e)
        {
            OnBlinkingChanged();
        }

        private void TendencyControl_TendencyStateChanged(object sender, RoutedEventArgs e)
        {
            OnTendencyStateChanged();
        }

        private void OnTendencyStateChanged()
        {
            drawingGroup.Children.Clear();

            switch (TendencyState)
            {
                case TendencyState.Down:
                    drawingGroup.Children.Add(downArrowGeometry);
                    break;
                
                case TendencyState.Equal:
                    drawingGroup.Children.Add(equalGeometry);
                    break;
                
                case TendencyState.Up:
                    drawingGroup.Children.Add(upArrowGeometry);
                    break;

                case TendencyState.Error:
                    drawingGroup.Children.Add(errorGeometry);
                    break;
            }
        }

        private void OnBlinkingChanged()
        {
            switch (Blinking)
            {
                case true:
                    controlBlinkAnimationClock.Controller.Resume();
                    controlBlinkAnimationClock.Controller.SpeedRatio = BlinkingSpeedRatio;
                    break;

                case false:
                    controlBlinkAnimationClock.Controller.Seek(TimeSpan.FromSeconds(0), TimeSeekOrigin.BeginTime);
                    controlBlinkAnimationClock.Controller.Pause();
                    break;
            }
        }
    }
}