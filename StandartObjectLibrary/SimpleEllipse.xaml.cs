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
    /// Interaction logic for Ellipse.xaml
    /// </summary>
    public partial class SimpleEllipse : DashboardObject
    {
        #region Properties

        [Category("Ellipse Properties")]
        public int RoundPrecision { get; set; }

        [Category("Ellipse Properties")]
        public string PrefixLabel { get; set; }

        #endregion

        #region Dependency Properties

        public static readonly DependencyProperty ShowValueProperty =
            DependencyProperty.Register("ShowValue", typeof(bool), typeof(SimpleEllipse), new FrameworkPropertyMetadata(true));

        [Bindable(true), Category("Ellipse Properties")]
        public bool ShowValue
        {
            get { return (bool)GetValue(ShowValueProperty); }
            set { SetValue(ShowValueProperty, value); }
        }

        #endregion

        public SimpleEllipse()
        {
            InitializeComponent();

            PrefixLabel = string.Empty;

            Loaded += new RoutedEventHandler(Ellipse_Loaded);
        }

        private void Ellipse_Loaded(object sender, RoutedEventArgs e)
        {
            StateChanged += new RoutedEventHandler(Ellipse_StateChanged);

            ellipse.RoundPrecision = RoundPrecision;
            ellipse.PrefixLabel = PrefixLabel;
            OnStateChanged();
        }

        private void Ellipse_StateChanged(object sender, RoutedEventArgs e)
        {
            OnStateChanged();
        }

        private void OnStateChanged()
        {
            ellipse.Fill = DashboardStyleResources.RadialFillBrushes[State];
        }
    }
}