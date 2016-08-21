using System.Collections.Generic;

namespace TrafficData.Model
{
    //Model Class for Dashboard Page
    public class MainPageData
    {
        public int TotalVehicles
        {
            get;
            set;
        }

        public int TotalGrossWeight
        {
            get;
            set;
        }

        public decimal AverageSpeed
        {
            get;
            set;
        }

        public int MostUsedLane
        {
            get;
            set;
        }

        public int LeastUsedLane
        {
            get;
            set;
        }

        public List<KeyValuePair<string, object>> VehicleChartData
        {
            get;
            set;
        }

        public List<KeyValuePair<string, object>> GrossWeightChartData
        {
            get;
            set;
        }

        public List<KeyValuePair<string, object>> AverageSpeedChartData
        {
            get;
            set;
        }

        public List<KeyValuePair<string, object>> UsedLaneChartData
        {
            get;
            set;
        }

        public MainPageData()
        {
        }
    }
}