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
using StandartObjectLibrary;
using DashboardEngine;
using System.Threading;

namespace StandartObjectLibrary
{
    /// <summary>
    /// Interaction logic for Cooler.xaml
    /// </summary>
    public partial class SimpleCooler : DashboardObject
    {
        #region Properties

        [Category("Cooler Properties")]
        public string MeasureUnit { get; set; }

        [Category("Cooler Properties")]
        public double InnerCoolerWidth 
        { 
            get { return cooler.ActualWidth; } 
        }

        [Category("Cooler Properties")]
        public double InnerCoolerHeight
        {
            get { return cooler.ActualHeight; }
        }

        [Category("Cooler Properties")]
        public string Label { get; set; }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty StrokeHeightProperty =
            DependencyProperty.Register("StrokeHeight", typeof(int), typeof(SimpleCooler), new FrameworkPropertyMetadata(12));

        [Bindable(true), Category("Cooler Properties")]
        public int StrokeHeight
        {
            get { return (int)GetValue(StrokeHeightProperty); }
            set { SetValue(StrokeHeightProperty, value); }
        }

        public static readonly DependencyProperty SpeedRatioProperty =
            DependencyProperty.Register("SpeedRatio", typeof(double), typeof(SimpleCooler), new FrameworkPropertyMetadata((double)0));

        [Bindable(true), Category("Cooler Properties")]
        public double SpeedRatio
        {
            get { return (double)GetValue(SpeedRatioProperty); }
            set { SetValue(SpeedRatioProperty, value); }
        }

        #endregion

        public SimpleCooler()
            : base()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(Cooler_Loaded);
        }

        private void Cooler_Loaded(object sender, RoutedEventArgs e)
        {
            StateChanged += new RoutedEventHandler(Cooler_StateChanged);

            if (string.IsNullOrEmpty(Label))
                titleTextBlock.Height = 0;
            else
                titleTextBlock.Text = Label;

            if (string.IsNullOrEmpty(MeasureUnit))
                measureUnitTextBlock.Height = 0;
            else
                measureUnitTextBlock.Text = MeasureUnit;
            

            cooler.Color2 = DashboardStyleResources.FillColors[State];
            

            if (State == Severity.Disabled)
                cooler.PauseAnimation();
            else
                cooler.ResumeAnimation();
        }

        private void Cooler_StateChanged(object sender, RoutedEventArgs e)
        {
            cooler.Color2 = DashboardStyleResources.FillColors[State];

            if (State == Severity.Disabled)
                cooler.PauseAnimation();
            else
                cooler.ResumeAnimation();
        }
    }
}