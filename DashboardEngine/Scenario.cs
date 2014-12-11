using System;
using System.Linq;
using System.Xml.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DashboardEngine
{
    public class DashboardContext
    {
        private DateTime _lastUpdate = DateTime.MinValue;

        public int RefreshInterval { get; private set; }

        public string XamlFile { get; set; }

        public string DashboardName { get; set; }

        public System.Windows.Controls.Page Page { get; private set; }

        public static DashboardContext Load(XElement xDashboard)
        {
            var dashboardContext = new DashboardContext
            {
                DashboardName = XmlUtils.GetAsString(xDashboard, "@name"),
                XamlFile = XmlUtils.GetAsString(xDashboard, "@xamlFile"),
                RefreshInterval = XmlUtils.GetAsInt(xDashboard, "@refreshInterval")
            };

            var pageStream = Application.GetRemoteStream(new Uri(dashboardContext.XamlFile, UriKind.Absolute)).Stream;

            dashboardContext.Page = (System.Windows.Controls.Page)XamlReader.Load(pageStream);

            return dashboardContext;
        }

        public bool RefreshDataProviders()
        {
            if (DateTime.Now - _lastUpdate < TimeSpan.FromSeconds(RefreshInterval))
                return false;

            _lastUpdate = DateTime.Now;

            foreach (var resource in Page.Resources.Values.OfType<XmlDataProvider>())
            {
                resource.Refresh();
            }

            return true;
        }
    }
}