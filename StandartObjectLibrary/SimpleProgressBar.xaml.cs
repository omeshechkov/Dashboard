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

namespace StandartObjectLibrary
{
    /// <summary>
    /// Interaction logic for ProgressBar.xaml
    /// </summary>
    public partial class SimpleProgressBar : DashboardObject
    {
        #region Dependency Properties

        public static readonly DependencyProperty UpperLimitProperty =
            DependencyProperty.Register("UpperLimit", typeof(double), typeof(SimpleProgressBar), new FrameworkPropertyMetadata((double)100));

        [Bindable(true), Category("Progress Bar Properties")]
        public double UpperLimit
        {
            get { return (double)GetValue(UpperLimitProperty); }
            set { SetValue(UpperLimitProperty, value); }
        }

        public static readonly DependencyProperty LowerLimitProperty =
            DependencyProperty.Register("LowerLimit", typeof(double), typeof(SimpleProgressBar), new FrameworkPropertyMetadata((double)0));

        [Bindable(true), Category("Progress Bar Properties")]
        public double LowerLimit
        {
            get { return (double)GetValue(LowerLimitProperty); }
            set { SetValue(LowerLimitProperty, value); }
        }

        #endregion        
        
        public SimpleProgressBar()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(ProgressBar_Loaded);
        }

        private void ProgressBar_Loaded(object sender, RoutedEventArgs e)
        {
            StateChanged += new RoutedEventHandler(ProgressBar_StateChanged);

            OnStateChanged();
        }

        private void ProgressBar_StateChanged(object sender, RoutedEventArgs e)
        {
            OnStateChanged();
        }

        private void OnStateChanged()
        {
            progressBar.FillBrush = DashboardStyleResources.SolidFillBrushes[State];
        }
    }
}