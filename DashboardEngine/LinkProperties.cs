using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace DashboardEngine
{
    public class LinkProperties: DependencyObject
    {
        public static readonly DependencyProperty TargetFrameProperty =
            DependencyProperty.Register("TargetFrame", typeof(string), typeof(LinkProperties), new UIPropertyMetadata(null));

        public string TargetFrame
        {
            get { return (string)GetValue(TargetFrameProperty); }
            set { SetValue(TargetFrameProperty, value); }
        }

        public static readonly DependencyProperty TargetUriProperty =
            DependencyProperty.Register("TargetUri", typeof(string), typeof(LinkProperties), new UIPropertyMetadata(null));

        public string TargetUri
        {
            get { return (string)GetValue(TargetUriProperty); }
            set { SetValue(TargetUriProperty, value); }
        }
    }
}