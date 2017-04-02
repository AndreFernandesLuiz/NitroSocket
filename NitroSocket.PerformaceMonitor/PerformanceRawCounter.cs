using System;
using System.Diagnostics;

namespace Library.PerformaceMonitor
{
    public class PerformanceRawCounter
    {
        private PerformanceCounter rawCounter;
        private string scoreName;

        public PerformanceRawCounter(string scoreNameParam)
        {
            scoreName = scoreNameParam;
            CheckCategory(false);
            CreateCounter(null);
        }

        public PerformanceRawCounter(string scoreNameParam, string instanceName)
        {
            scoreName = scoreNameParam;
            CheckCategory(true);
            CreateCounter(instanceName);
        }

        private void CheckCategory(bool multiInstance)
        {
            if (!PerformanceCounterCategory.Exists(scoreName))
            {
                CounterCreationDataCollection meter = new CounterCreationDataCollection();
                CounterCreationData rawCounterData = new CounterCreationData();
                rawCounterData.CounterType = PerformanceCounterType.NumberOfItems32;
                rawCounterData.CounterName = "Quantity";
                rawCounterData.CounterHelp = "Quantity of a determinate item";
                meter.Add(rawCounterData);

                PerformanceCounterCategoryType category = multiInstance ? PerformanceCounterCategoryType.MultiInstance : PerformanceCounterCategoryType.SingleInstance;
                PerformanceCounterCategory.Create(scoreName, scoreName, category, meter);
            }
        }

        private void CreateCounter(string instanceName)
        {
            rawCounter = new PerformanceCounter();
            rawCounter.CategoryName = scoreName;
            rawCounter.CounterName = "Quantity";
            rawCounter.MachineName = ".";
            rawCounter.ReadOnly = false;

            if (instanceName != null)
            {
                rawCounter.InstanceLifetime = PerformanceCounterInstanceLifetime.Global;
                rawCounter.InstanceName = instanceName;
            }
        }

        public void UpdateCounter(long rawValue)
        {
            rawCounter.RawValue = rawValue;
        }

        public long Counter
        {
            get
            {
                return rawCounter.RawValue;
            }
        }

    }
}
