using System.Collections.Generic;

namespace DashboardEngine
{
    public class AggregateStates
    {
        private static object m_SyncObject = new object();
        private static Dictionary<string, List<DashboardObject>> m_RegisteredObjects = new Dictionary<string, List<DashboardObject>>();

        public static void RegisterObject(DashboardObject Object)
        {
            if (!string.IsNullOrEmpty(Object.AggregateScope))
            {
                lock (m_SyncObject)
                {
                    var list = new List<DashboardObject>();

                    if (!m_RegisteredObjects.ContainsKey(Object.AggregateScope))
                        m_RegisteredObjects.Add(Object.AggregateScope, list);

                    list = m_RegisteredObjects[Object.AggregateScope];

                    if (!list.Contains(Object))
                        list.Add(Object);
                }
            }
        }

        public static void UnregisterDashboard(string AggregateScope)
        {
            if (!string.IsNullOrEmpty(AggregateScope))
            {
                lock (m_SyncObject)
                {
                    if (m_RegisteredObjects.ContainsKey(AggregateScope))
                        m_RegisteredObjects.Remove(AggregateScope);
                }
            }
        }

        public static Severity GetDashboardTotalSeverity(string DashboardName)
        {
            lock (m_SyncObject)
            {
                var resultSeverity = Severity.Disabled;

                if (m_RegisteredObjects.ContainsKey(DashboardName))
                {
                    var dashboardObjects = m_RegisteredObjects[DashboardName];

                    foreach (var dashboardObject in dashboardObjects)
                    {
                        if (resultSeverity == Severity.Disabled)
                            resultSeverity = dashboardObject.State;
                        else if (dashboardObject.State != Severity.Disabled && dashboardObject.State > resultSeverity)
                            resultSeverity = dashboardObject.State;
                    }
                }

                return resultSeverity;
            }
        }
    }
}