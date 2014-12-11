using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DashboardEngine
{
    public class ToolTipAlarmEntry
    {
        public Severity AlarmSeverity { get; set; }
        public string Message { get; set; }

        public ToolTipAlarmEntry(Severity alarmSeverity, string message)
        {
            AlarmSeverity = alarmSeverity;
            Message = message;
        }
    }
}
