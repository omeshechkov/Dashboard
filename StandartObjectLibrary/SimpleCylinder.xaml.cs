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
    /// Interaction logic for Cylinder.xaml
    /// </summary>
    public partial class SimpleCylinder : DashboardObject
    {
        #region Properties

        [Category("Cylinder Properties")]
        public double LowerLimit { get; set; }

        [Category("Cylinder Properties")]
        public double UpperLimit { get; set; }

        #endregion

        public SimpleCylinder()
        {
            InitializeComponent();

            LowerLimit = 0;
            UpperLimit = 100;

            Loaded += new RoutedEventHandler(Cylinder_Loaded);
        }

        private void Cylinder_Loaded(object sender, RoutedEventArgs e)
        {
            cylinder.LowerLimit = LowerLimit;
            cylinder.UpperLimit = UpperLimit;

            StateChanged += new RoutedEventHandler(Cylinder_StateChanged);

            cylinder.FillBrush = DashboardStyleResources.SolidFillBrushes[State];
        }

        private void Cylinder_StateChanged(object sender, RoutedEventArgs e)
        {
            cylinder.FillBrush = DashboardStyleResources.SolidFillBrushes[State];
        }
    }
}