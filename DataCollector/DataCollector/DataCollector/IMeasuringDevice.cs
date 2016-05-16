using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector
{
    interface IMeasuringDevice
    {
        /// <summary>
        /// Public method declaration that represents a metric value
        /// </summary>
        /// <returns>Decimal of most recent capture</returns>
        decimal MetricValue(int mostRecentMeasure);
        /// <summary>
        /// Public method declaration that represents an imperial value
        /// </summary>
        /// <returns>Decimal of the most recent capture</returns>
        decimal ImperialValue(int mostRecentMeasure);
        /// <summary>
        /// Public method declaration to start device and returns nothing
        /// </summary>
        void StartCollecting();
        /// <summary>
        /// Public method declaration that stops device and returns nothing
        /// </summary>
        void StopCollecting();
        /// <summary>
        /// Public method declaration that will retrieve a copy of all the recent data the measuring device has captured
        /// </summary>
        /// <returns>An array of ints</returns>
        decimal[] GetRawData();
    }
}
