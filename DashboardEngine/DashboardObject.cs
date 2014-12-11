using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using DashboardEngine;
using System.ComponentModel;
using System.Windows;
using System.Collections;
using System.Xml.Linq;
using System.Xml;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace DashboardEngine
{
    public class DashboardObject : UserControl
    {
        #region Properties

        private XmlElement m_DataSource = null;
        private ToolTip m_ToolTip = new ToolTip();

        [Category("Dashboard Object Propreties")]
        public string CounterXPath { get; set; }

        [Category("Dashboard Object Propreties")]
        public Severity AutoShowToolTipOnSeverity { get; set; }

        #endregion

        #region Dependecies Properties

        #region AutoShowToolTip

        public static readonly DependencyProperty AutoShowToolTipProperty =
            DependencyProperty.Register("AutoShowToolTip", typeof(bool), typeof(DashboardObject), new FrameworkPropertyMetadata(true));

        [Bindable(true), Category("Dashboard Object Propreties")]
        public bool AutoShowToolTip
        {
            get { return (bool)GetValue(AutoShowToolTipProperty); }
            set { SetValue(AutoShowToolTipProperty, value); }
        }

        #endregion

        #region Title

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(DashboardObject),
            new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnValueChangedCallback)));

        [Bindable(true), Category("Dashboard Object Propreties")]
        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        #endregion

        #region Description

        public static readonly DependencyProperty DescriptionProperty =
            DependencyProperty.Register("Description", typeof(string), typeof(DashboardObject),
            new FrameworkPropertyMetadata(string.Empty, new PropertyChangedCallback(OnValueChangedCallback)));

        [Bindable(true), Category("Dashboard Object Propreties")]
        public string Description
        {
            get { return (string)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        #endregion

        #region Aggregate Scope

        public static readonly DependencyProperty AggregateScopeProperty =
            DependencyProperty.Register("AggregateScope", typeof(string), typeof(DashboardObject), new FrameworkPropertyMetadata(string.Empty));

        [Bindable(true), Category("Dashboard Object Propreties")]
        public string AggregateScope
        {
            get { return (string)GetValue(AggregateScopeProperty); }
            set { SetValue(AggregateScopeProperty, value); }
        }

        #endregion

        #region State

        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(Severity), typeof(DashboardObject),
            new FrameworkPropertyMetadata(Severity.Disabled, new PropertyChangedCallback(OnStateChangedCallback)));

        [Bindable(true), Category("Dashboard Object Propreties")]
        public Severity State
        {
            get { return (Severity)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }

        private static void OnStateChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(StateChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        public static readonly RoutedEvent StateChangedEvent =
            EventManager.RegisterRoutedEvent("StateChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(DashboardObject));

        public event RoutedEventHandler StateChanged
        {
            add { AddHandler(StateChangedEvent, value); }
            remove { RemoveHandler(StateChangedEvent, value); }
        }

        #endregion

        #region Value

        public static readonly DependencyProperty ValueProperty =
            DependencyProperty.Register("Value", typeof(object), typeof(DashboardObject),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnValueChangedCallback)));

        [Bindable(true), Category("Dashboard Object Propreties")]
        public object Value
        {
            get { return GetValue(ValueProperty); }
            set { SetValue(ValueProperty, value); }
        }

        private static void OnValueChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(ValueChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        public static readonly RoutedEvent ValueChangedEvent =
            EventManager.RegisterRoutedEvent("ValueChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(DashboardObject));

        public event RoutedEventHandler ValueChanged
        {
            add { AddHandler(ValueChangedEvent, value); }
            remove { RemoveHandler(ValueChangedEvent, value); }
        }

        #endregion

        #region DataSource

        public static readonly DependencyProperty DataSourceProperty =
            DependencyProperty.Register("DataSource", typeof(IEnumerable), typeof(DashboardObject),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(OnDataSourceChangedCallback)));

        [Bindable(true), Category("Dashboard Object Propreties")]
        public IEnumerable DataSource
        {
            get { return (IEnumerable)GetValue(DataSourceProperty); }
            set { SetValue(DataSourceProperty, value); }
        }

        private static void OnDataSourceChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            RoutedEventArgs newargs = new RoutedEventArgs(DataSourceChangedEvent);
            (d as FrameworkElement).RaiseEvent(newargs);
        }

        public static readonly RoutedEvent DataSourceChangedEvent =
            EventManager.RegisterRoutedEvent("DataSourceChanged", RoutingStrategy.Bubble, typeof(DependencyPropertyChangedEventHandler), typeof(DashboardObject));

        public event RoutedEventHandler DataSourceChanged
        {
            add { AddHandler(DataSourceChangedEvent, value); }
            remove { RemoveHandler(DataSourceChangedEvent, value); }
        }

        #endregion

        #region Blinking

        public static readonly DependencyProperty BlinkingProperty =
            DependencyProperty.Register("Blinking", typeof(bool), typeof(DashboardObject), new FrameworkPropertyMetadata(false));

        [Bindable(true), Category("Dashboard Object Propreties")]
        public bool Blinking
        {
            get { return (bool)GetValue(BlinkingProperty); }
            set { SetValue(BlinkingProperty, value); }
        }

        #endregion

        #region BlinkingSpeedRatio

        public static readonly DependencyProperty BlinkingSpeedRatioProperty =
            DependencyProperty.Register("BlinkingSpeedRatio", typeof(double), typeof(DashboardObject), new FrameworkPropertyMetadata((double)3));

        [Bindable(true), Category("Dashboard Object Propreties")]
        public double BlinkingSpeedRatio
        {
            get { return (double)GetValue(BlinkingSpeedRatioProperty); }
            set { SetValue(BlinkingSpeedRatioProperty, value); }
        }

        #endregion

        #endregion

        public DashboardObject()
        {
            Loaded += new RoutedEventHandler(DashboardObject_Loaded);
            Unloaded += new RoutedEventHandler(DashboardObject_Unloaded);

            AutoShowToolTipOnSeverity = Severity.Information;
            CounterXPath = "Counters/Counter";
            Description = string.Empty;

            m_ToolTip.PlacementTarget = this;
            ToolTip = m_ToolTip;
        }

        private Severity GetCounterSeverity(XmlNode counter)
        {
            Severity resultSeverity = Severity.Disabled;


            SortedDictionary<double, Severity> intervals = new SortedDictionary<double, Severity>();

            double normalSeverity;
            double informationSeverity;
            double lowSeverity;
            double mediumSeverity;
            double highSeverity;
            double value;
            double disableSeverity = double.NaN;

            if (!XmlHelper.GetAttribute(counter, "DisableSeverity", out disableSeverity))
                disableSeverity = double.NaN;

            if (XmlHelper.GetAttribute(counter, "NormalSeverity", out normalSeverity))
                intervals.Add(normalSeverity, Severity.Normal);

            if (XmlHelper.GetAttribute(counter, "InformationSeverity", out informationSeverity) && !intervals.ContainsKey(informationSeverity))
                intervals.Add(informationSeverity, Severity.Information);

            if (XmlHelper.GetAttribute(counter, "LowSeverity", out lowSeverity) && !intervals.ContainsKey(lowSeverity))
                intervals.Add(lowSeverity, Severity.Low);

            if (XmlHelper.GetAttribute(counter, "MediumSeverity", out mediumSeverity) && !intervals.ContainsKey(mediumSeverity))
                intervals.Add(mediumSeverity, Severity.Medium);

            if (XmlHelper.GetAttribute(counter, "HighSeverity", out highSeverity) && !intervals.ContainsKey(highSeverity))
                intervals.Add(highSeverity, Severity.High);

            if (XmlHelper.GetAttribute(counter, "Value", out value) && intervals.Count > 0)
            {
                if (value == disableSeverity)
                    return Severity.Disabled;

                double minValue = intervals.Min(o => o.Key);
                double maxValue = intervals.Max(o => o.Key);

                if (value <= minValue)
                    resultSeverity = intervals[minValue];
                else if (value >= maxValue)
                    resultSeverity = intervals[maxValue];
                else
                {
                    foreach (KeyValuePair<double, Severity> kvp in intervals.Reverse())
                    {
                        if (value >= kvp.Key)
                        {
                            resultSeverity = kvp.Value;
                            break;
                        }
                    }
                }
            }

            return resultSeverity;
        }

        private void ChangeToolTipAlarms(List<ToolTipAlarmEntry> alarms)
        {
            try
            {
                m_ToolTip.Content = ToolTipFactory.CreatePage(Title, Description, alarms);
            }
            catch (Exception ex)
            { 
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            m_ToolTip.IsOpen = false;
        }

        private void OnDataSourceChanged()
        {
            m_DataSource = DataSource == null ? null : DataSource.Cast<XmlElement>().FirstOrDefault();
            
            if (m_DataSource != null)
            {
                Severity resultSeverity = Severity.Disabled;
                string value = string.Empty;

                XmlNodeList counters = m_DataSource.SelectNodes(CounterXPath);

                List<ToolTipAlarmEntry> alarmMessages = new List<ToolTipAlarmEntry>();
                foreach (XmlNode counterNode in counters)
                {
                    Severity state = GetCounterSeverity(counterNode);

                    bool isPrimary;
                    if (XmlHelper.GetAttribute(counterNode, "IsPrimary", out isPrimary) && isPrimary)
                    {
                        if (XmlHelper.GetAttribute(counterNode, "Value", out value))
                            Value = value;

                        if (state == Severity.Disabled)
                        {
                            resultSeverity = Severity.Disabled;
                            break;
                        }
                    }
                    
                    if (state > resultSeverity)
                        resultSeverity = state;

                    if (state > Severity.Normal)
                    {
                        XmlNode alarmNode = counterNode.SelectSingleNode("Alarm");

                        string message;
                        if (XmlHelper.GetAttribute(alarmNode, "message", out message))
                        {
                            List<string> parameters = new List<string>();

                            XmlNodeList parametersNodes = alarmNode.SelectNodes("Parameter");
                            foreach (XmlNode parameterNode in parametersNodes)
                            {
                                string parameter = string.IsNullOrEmpty(parameterNode.InnerText) ? "<Empty>" : parameterNode.InnerText;
                                parameters.Add(parameter);
                            }

                            message = string.Format(message, parameters.ToArray());

                            alarmMessages.Add(new ToolTipAlarmEntry(state, message));
                        }
                    }
                }

                if (State != resultSeverity)
                    State = resultSeverity;

                Blinking = resultSeverity == Severity.High;

                if (alarmMessages.Count > 0)
                {
                    ChangeToolTipAlarms(alarmMessages);

                    if (AutoShowToolTip && resultSeverity >= AutoShowToolTipOnSeverity)
                    {
                        m_ToolTip.HorizontalOffset = ActualWidth / 2;
                        m_ToolTip.VerticalOffset = ActualHeight / 2;
                        m_ToolTip.Placement = PlacementMode.RelativePoint;
                        m_ToolTip.IsOpen = true;
                        m_ToolTip.StaysOpen = true;
                    }
                }
                else
                {
                    ChangeToolTipAlarms(null);
                    m_ToolTip.StaysOpen = true;
                    m_ToolTip.IsOpen = false;
                }
            }
            else
                State = Severity.Disabled;
        }

        private void DashboardObject_Unloaded(object sender, RoutedEventArgs e)
        {
            m_ToolTip.IsOpen = false;
            m_ToolTip.StaysOpen = true;

        }

        private void DashboardObject_Loaded(object sender, RoutedEventArgs e)
        {
            DataSourceChanged += new RoutedEventHandler(DashboardObject_DataSourceChanged);

            AggregateStates.RegisterObject(this);

            ChangeToolTipAlarms(null);

            OnDataSourceChanged();
        }

        private void DashboardObject_DataSourceChanged(object sender, RoutedEventArgs e)
        {
            OnDataSourceChanged();
        }
    }
}