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

namespace DashboardEngine
{
    /// <summary>
    /// Interaction logic for ToolTipPage.xaml
    /// </summary>
    public partial class ToolTipPage : Page
    {
        public ToolTipPage(string elementName, string description, List<ToolTipAlarmEntry> currentAlarms)
        {
            InitializeComponent();

            //Unattach grid object
            Content = null;

            elementNameTextBox.Text = elementName;
            descriptionTextBox.Text = description;

            if (currentAlarms != null && currentAlarms.Count != 0)
            {
                TextBlock currentAlarmsTextBlock = new TextBlock();
                currentAlarmsTextBlock.FontWeight = FontWeights.Bold;
                currentAlarmsTextBlock.Margin = new Thickness(0, 3, 0, 0);
                currentAlarmsTextBlock.Text = "Current Alarms:";

                stackPanel.Children.Add(currentAlarmsTextBlock);

                ListBox alarmsListBox = new ListBox();
                alarmsListBox.Style = (Style)LayoutRoot.FindResource("alarmListBoxStyle");
                alarmsListBox.ItemContainerStyle = (Style)LayoutRoot.FindResource("alarmListBoxItemStyle");

                alarmsListBox.ItemsSource = from alarm in currentAlarms
                                            select new
                                            {
                                                Message = alarm.Message,
                                                EllipseFill = DashboardStyleResources.RadialFillBrushes[alarm.AlarmSeverity]
                                            };

                stackPanel.Children.Add(alarmsListBox);
            }
        }
    }
}