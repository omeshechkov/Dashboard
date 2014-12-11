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
using StandartObjectLibrary;
using System.ComponentModel;
using System.Collections;
using DashboardEngine;

namespace StandartObjectLibrary
{
    /// <summary>
    /// Interaction logic for Link.xaml
    /// </summary>
    public partial class SimpleLink : DashboardObject
    {
        #region Properties

        [Category("Link Properties")]
        public double MinValue { get; set; }

        [Category("Link Properties")]
        public double MaxValue { get; set; }

        [Category("Link Properties")]
        public TimeSpan Period { get; set; }

        [Category("Link Properties")]
        public Direction LinkDirection { get; set; }

        [Category("Link Properties")]
        public string MeasureUnit { get; set; }

        [Category("Link Properties")]
        public double LinkLength { get; set; }

        [Category("Link Properties")]
        public double StrokeHeight { get; set; }

        [Category("Link Properties")]
        public string Label { get; set; }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register("SpeedRatio", typeof(double), typeof(SimpleLink), new FrameworkPropertyMetadata((double)0));

        [Bindable(true), Category("Link Properties")]
        public double SpeedRatio
        {
            get { return (double)GetValue(SpeedRatioProperty); }
            set { SetValue(SpeedRatioProperty, value); }
        }

        public static readonly DependencyProperty GraphicValuesProperty =
            DependencyProperty.Register("GraphicValues", typeof(IEnumerable), typeof(SimpleLink), new FrameworkPropertyMetadata(null));

        [Bindable(true), Category("Link Properties")]
        public IEnumerable GraphicValues
        {
            get { return (IEnumerable)GetValue(GraphicValuesProperty); }
            set { SetValue(GraphicValuesProperty, value); }
        }

        #endregion

        public SimpleLink()
        {
            InitializeComponent();

            StrokeHeight = 16;
            LinkDirection = Direction.Right;
            LinkLength = double.NaN;

            MinValue = double.MaxValue;
            MaxValue = double.MinValue;

            Loaded += new RoutedEventHandler(Link_Loaded);
        }

        private void Link_Loaded(object sender, RoutedEventArgs e)
        {
            link.Period = Period;
            link.MinValue = MinValue;
            link.MaxValue = MaxValue;
            link.LinkDirection = LinkDirection;

            link.LinkLength = LinkLength;
            link.StrokeHeight = StrokeHeight;

            if (string.IsNullOrEmpty(Label))
                TitleTextBlock.Height = 0;
            else
                TitleTextBlock.Text = Label;
            MeasureUnitTextBlock.Text = MeasureUnit;

            StateChanged += new RoutedEventHandler(Link_StateChanged);
            ValueChanged += new RoutedEventHandler(SimpleLink_ValueChanged);

            switch (LinkDirection)
            {
                case Direction.Left:
                    MakeHorizontal();
                    break;

                case Direction.Right:
                    MakeHorizontal(); 
                    break;

                case Direction.Down:
                    MakeVertical();
                    break;

                case Direction.Up:
                    MakeVertical();
                    break;
            }

            OnStateChanged();
            OnValueChanged();
        }

        private void SimpleLink_ValueChanged(object sender, RoutedEventArgs e)
        {
            OnValueChanged();
        }

        private void Link_StateChanged(object sender, RoutedEventArgs e)
        {
            OnStateChanged();
        }

        private void MakeHorizontal()
        {
            TitleTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
            ValueStackPanel.HorizontalAlignment = HorizontalAlignment.Center;
            ValueStackPanel.Margin = new Thickness(5, 0, 0, 0);

            DockPanel.SetDock(TitleTextBlock, Dock.Top);
            DockPanel.SetDock(ValueStackPanel, Dock.Bottom);

            TitleRotateTransform.Angle = 0;
            ValueRotateTransform.Angle = 0;
        }

        private void MakeVertical()
        {
            TitleTextBlock.VerticalAlignment = VerticalAlignment.Center;
            ValueStackPanel.VerticalAlignment = VerticalAlignment.Center;

            DockPanel.SetDock(TitleTextBlock, Dock.Right);
            DockPanel.SetDock(ValueStackPanel, Dock.Left);

            TitleRotateTransform.Angle = 90;
            ValueRotateTransform.Angle = 90;
        }

        private void OnValueChanged()
        {
            if (Value != null)
                ValueTextBlock.Text = Value.ToString();
        }

        private void OnStateChanged()
        {
            link.Color2 = DashboardStyleResources.FillColors[State];

            if (State == Severity.Disabled)
                link.PauseAnimation();
            else
                link.ResumeAnimation();
        }
    }
}