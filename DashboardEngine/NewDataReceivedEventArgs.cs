using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardEngine
{
    public class NewDataReceivedEventArgs: EventArgs
    {
        public string DashboardName { get; set; }

        public NewDataReceivedEventArgs(string dashboardName)
        {
            DashboardName = dashboardName;
        }
    }
}
