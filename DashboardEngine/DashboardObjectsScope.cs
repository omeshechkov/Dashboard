using System.Windows.Controls;

namespace DashboardEngine
{
    public class DashboardObjectsScope
    {
        public DashboardContext DashboardContext { get; set; }

        public ListBoxItem ListBoxItem { get; set; }

        public Severity State { get; set; }

        public DashboardObjectsScope(DashboardContext dashboardContext, ListBoxItem listBoxItem)
        {
            DashboardContext = dashboardContext;
            ListBoxItem = listBoxItem;

            State = Severity.Disabled;
        }
    }
}
