using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Media;

namespace DashboardEngine
{
    public class ToolTipFactory
    {
        private static Style m_TextBlockStyle;

        private static Style TextBlockStyle 
        { 
            get { return m_TextBlockStyle; } 
        }

        static ToolTipFactory()
        {
            m_TextBlockStyle = new Style() { TargetType = typeof(TextBlock) };
            m_TextBlockStyle.Setters.Add(new Setter(TextBlock.FontSizeProperty, (double)11));
            m_TextBlockStyle.Setters.Add(new Setter(TextBlock.FontFamilyProperty, new FontFamily("Verdana")));
        }

        private static Grid CreateAlarmElement(Severity alarmSeverity, string message)
        {
            Grid grid = new Grid()
            {
                Margin = new Thickness(5, 1, 0, 1)
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(10) });
            grid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Star) });

            Ellipse ellipse = new Ellipse()
            {
                Width = 10,
                Height = 10,
                Fill = DashboardStyleResources.RadialFillBrushes[alarmSeverity]
            };

            TextBlock textBlock = new TextBlock()
            {
                Style = TextBlockStyle,
                Margin = new Thickness(3, 0, 0, 0),
                VerticalAlignment = VerticalAlignment.Center,
                Text = message
            };

            grid.Children.Add(ellipse);
            grid.Children.Add(textBlock);

            Grid.SetColumn(ellipse, 0);
            Grid.SetColumn(textBlock, 1);

            return grid;
        }

        public static Grid CreatePage(string elementName, string description, List<ToolTipAlarmEntry> currentAlarms)
        {
            Grid layoutRoot = new Grid()
            {
                Margin = new Thickness(2, 3, 2, 3)
            };

            StackPanel stackPanel = new StackPanel() 
            { 
                Orientation = Orientation.Vertical 
            };

            TextBlock elementNameTextBlock = new TextBlock()
            {
                Style = TextBlockStyle,
                FontWeight = FontWeights.Bold,
                Text = elementName
            };

            TextBlock descriptionTextBlock = new TextBlock()
            {
                Style = TextBlockStyle,
                Margin = new Thickness(3, 0, 0, 0),
                MaxWidth = 500,
                TextWrapping = TextWrapping.Wrap,
                Text = description
            };

            stackPanel.Children.Add(elementNameTextBlock);
            stackPanel.Children.Add(descriptionTextBlock);

            if (currentAlarms != null && currentAlarms.Count != 0)
            {
                TextBlock currentAlarmsTextBlock = new TextBlock()
                {
                    Style = TextBlockStyle,
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 3, 0, 0),
                    Text = "Current Alarms:"
                };

                stackPanel.Children.Add(currentAlarmsTextBlock);

                foreach (ToolTipAlarmEntry alarm in currentAlarms)
                {
                    Grid alarmGrid = CreateAlarmElement(alarm.AlarmSeverity, alarm.Message);
                    stackPanel.Children.Add(alarmGrid);
                }
            }

            layoutRoot.Children.Add(stackPanel);

            return layoutRoot;
        }
    }
}