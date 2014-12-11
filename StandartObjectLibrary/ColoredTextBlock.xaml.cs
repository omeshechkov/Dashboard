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
using DashboardEngine;

namespace StandartObjectLibrary
{
    /// <summary>
    /// Interaction logic for ColoredTextBlock.xaml
    /// </summary>
    public partial class ColoredTextBlock : DashboardObject
    {
        public ColoredTextBlock()
        {
            InitializeComponent();

            Loaded += new RoutedEventHandler(ColoredTextBlock_Loaded);
        }

        private void ColoredTextBlock_Loaded(object sender, RoutedEventArgs e)
        {
            StateChanged += new RoutedEventHandler(ColoredTextBlock_StateChanged);
            ValueChanged += new RoutedEventHandler(ColoredTextBlock_ValueChanged);

            OnStateChanged();
            OnValueChanged();
        }

        private void ColoredTextBlock_ValueChanged(object sender, RoutedEventArgs e)
        {
            OnValueChanged();
        }

        private void ColoredTextBlock_StateChanged(object sender, RoutedEventArgs e)
        {
            OnStateChanged();
        }

        private void OnStateChanged()
        {
            Color highLightColor = ColorHelper.ChangeColor(DashboardStyleResources.FillColors[State], 0.5F, 1);

            ValueTextBlock.Foreground = new SolidColorBrush(highLightColor);
        }

        private void OnValueChanged()
        {
            if (Value != null)
                ValueTextBlock.Text = Value.ToString();
        }
    }
}