using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Library.PerformaceMonitor
{

    public class PerformaceScore
    {
        #region Variables
        private PerformanceCounter NumberOfOperations;
        private PerformanceCounter AverageOperations;
        private PerformanceCounter AverageDuration;
        private PerformanceCounter BaseAverageDuration;
        private string scoreName = string.Empty;
        private long timeStart = 0;
        private long timeEnd = 0;
        #endregion

        public PerformaceScore(string scoreNameParam)
        {
            scoreName = scoreNameParam;
            CheckCategory(false);
            CreateCounters(null);
        }

        public PerformaceScore(string scoreNameParam, string instanceName)
        {
            scoreName = scoreNameParam;
            CheckCategory(true);
            CreateCounters(instanceName);
        }
        
        #region private methods

        private void CheckCategory(bool multiInstance)
        {
            bool counter = true;
            try
            {
                if (!PerformanceCounterCategory.Exists(scoreName))
                {
                    counter = false;
                }
            }
            catch (Exception ex)
            {
                counter = false;
            }

            if (!counter)
            { 
                CounterCreationDataCollection meter = new CounterCreationDataCollection();

                CounterCreationData averageDuration = new CounterCreationData();
                averageDuration.CounterType = PerformanceCounterType.AverageTimer32;
                averageDuration.CounterName = "average time per operation";
                averageDuration.CounterHelp = "Average duration per operation execution";
                meter.Add(averageDuration);


                CounterCreationData baseAverageDuration = new CounterCreationData();
                baseAverageDuration.CounterType = PerformanceCounterType.AverageBase;
                baseAverageDuration.CounterName = "average time per operation base";
                baseAverageDuration.CounterHelp = "Average duration per operation execution base";
                meter.Add(baseAverageDuration);

                CounterCreationData numberOfOperations = new CounterCreationData();
                numberOfOperations.CounterType = PerformanceCounterType.NumberOfItems32;
                numberOfOperations.CounterName = "# operations executed";
                numberOfOperations.CounterHelp = "Total number of operations executed";
                meter.Add(numberOfOperations);

                CounterCreationData averageOperations = new CounterCreationData();
                averageOperations.CounterType = PerformanceCounterType.RateOfCountsPerSecond32;
                averageOperations.CounterName = "# operations / sec";
                averageOperations.CounterHelp = "Number of operations executed per second";
                meter.Add(averageOperations);

                PerformanceCounterCategoryType category = multiInstance ? PerformanceCounterCategoryType.MultiInstance : PerformanceCounterCategoryType.SingleInstance;

                PerformanceCounterCategory.Create(scoreName, scoreName, category, meter);
            }
        }

        private void CreateCounters(string instanceName)
        {
            NumberOfOperations = new PerformanceCounter();
            NumberOfOperations.CategoryName = scoreName;
            NumberOfOperations.CounterName = "# operations executed";
            NumberOfOperations.MachineName = ".";
            NumberOfOperations.ReadOnly = false;

            AverageOperations = new PerformanceCounter();
            AverageOperations.CategoryName = scoreName;
            AverageOperations.CounterName = "# operations / sec";
            AverageOperations.MachineName = ".";
            AverageOperations.ReadOnly = false;

            AverageDuration = new PerformanceCounter();
            AverageDuration.CategoryName = scoreName;
            AverageDuration.CounterName = "average time per operation";
            AverageDuration.MachineName = ".";
            AverageDuration.ReadOnly = false;

            BaseAverageDuration = new PerformanceCounter();
            BaseAverageDuration.CategoryName = scoreName;
            BaseAverageDuration.CounterName = "average time per operation base";
            BaseAverageDuration.MachineName = ".";
            BaseAverageDuration.ReadOnly = false;

            if (instanceName != null)
            {
                NumberOfOperations.InstanceLifetime = PerformanceCounterInstanceLifetime.Global;
                AverageOperations.InstanceLifetime = PerformanceCounterInstanceLifetime.Global;
                AverageDuration.InstanceLifetime = PerformanceCounterInstanceLifetime.Global;
                BaseAverageDuration.InstanceLifetime = PerformanceCounterInstanceLifetime.Global;

                NumberOfOperations.InstanceName = instanceName;
                AverageOperations.InstanceName = instanceName;
                AverageDuration.InstanceName = instanceName;
                BaseAverageDuration.InstanceName = instanceName;
            }

        }

        #endregion private methods

        public long TotalOperations
        {
            get 
            {
                return NumberOfOperations.RawValue;
            }
        }

        public float OperationsPerSecond()
        {
            return AverageOperations.NextValue();
        }

        public float OperationAverageTime()
        {
            return AverageDuration.NextValue();
        }

        public void MeasureBasicScore()
        {
            long measureTime = timeEnd - timeStart;
            //Log.Trace(string.Format("Performace Counter {0} incremented by seconds {1}", ScoreName, measureTime));
            NumberOfOperations.Increment();
            AverageOperations.Increment();
            AverageDuration.IncrementBy(measureTime);
            BaseAverageDuration.Increment();
        }

        public void MeasureBasicScore(long measureTime)
        {
            //Log.Trace(string.Format("Performace Counter {0} incremented by seconds {1}", ScoreName, measureTime));
            NumberOfOperations.Increment();
            AverageOperations.Increment();
            AverageDuration.IncrementBy(measureTime);
            BaseAverageDuration.Increment();
        }

        public void MeasureBasicScore(long measureTime, long numberOfOperations)
        {
            //Log.Trace(string.Format("Performace Counter {0} incremented by seconds {1}, totalOperations = {2}", ScoreName, measureTime, numberOfOperations));
            NumberOfOperations.IncrementBy(numberOfOperations);
            AverageOperations.IncrementBy(numberOfOperations);
            AverageDuration.IncrementBy(measureTime);
            BaseAverageDuration.Increment();
        }


        #region ProcControl
        public void startProc()
        {
            QueryPerformanceCounter(ref timeStart);
        }


        public void endProc()
        {
            QueryPerformanceCounter(ref timeEnd);
        }


        [DllImport("Kernel32.dll")]
        public static extern void QueryPerformanceCounter(ref long ticks);

        #endregion

    }
 }
