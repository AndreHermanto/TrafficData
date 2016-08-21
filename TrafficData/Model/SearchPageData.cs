using System;
using System.Collections.Generic;

namespace TrafficData.Model
{
    //Model Class for Search Page
    public class SearchPageData
    {
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

        public List<List<KeyValuePair<string, object>>> WeightChartData
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
        public List<KeyValuePair<string, object>> AverageWeigthChartData { get; internal set; }
        public List<KeyValuePair<string, object>> LowestWeigthChartData { get; internal set; }
        public List<KeyValuePair<string, object>> HighestWeigthChartData { get; internal set; }
        public List<KeyValuePair<string, object>> NthTileWeigthChartData { get; internal set; }
        public List<KeyValuePair<string, object>> AverageTrafficFlowChartData { get; internal set; }
        public List<KeyValuePair<string, object>> AverageTrafficDensityChartData { get; internal set; }
        public List<KeyValuePair<string, object>> WeightHistogramChartData { get; internal set; }
        public List<KeyValuePair<string, object>> WeightPercentileChartData { get; internal set; }
        public List<KeyValuePair<string, object>> LowestSpeedChartData { get; internal set; }
        public List<KeyValuePair<string, object>> HighestSpeedChartData { get; internal set; }
        public List<KeyValuePair<string, object>> NthTileSpeedChartData { get; internal set; }
        public List<KeyValuePair<string, object>> SpeedHistogramChartData { get; internal set; }
        public List<KeyValuePair<string, object>> SpeedPercentileChartData { get; internal set; }
        public List<KeyValuePair<string, object>> AverageLengthChartData { get; internal set; }
        public List<KeyValuePair<string, object>> LowestLengthChartData { get; internal set; }
        public List<KeyValuePair<string, object>> HighestLengthChartData { get; internal set; }
        public List<KeyValuePair<string, object>> NthTileLengthChartData { get; internal set; }
        public List<KeyValuePair<string, object>> LengthHistogramChartData { get; internal set; }
        public List<KeyValuePair<string, object>> LengthPercentileChartData { get; internal set; }
        public List<KeyValuePair<string, object>> LowestHeadwayChartData { get; internal set; }
        public List<KeyValuePair<string, object>> HighestHeadwayChartData { get; internal set; }
        public List<KeyValuePair<string, object>> AverageHeadwayChartData { get; internal set; }
        public List<KeyValuePair<string, object>> NthTileHeadwayChartData { get; internal set; }
        public List<KeyValuePair<string, object>> HeadwayPercentileChartData { get; internal set; }
        public List<KeyValuePair<string, object>> HeadwayHistogramChartData { get; internal set; }

        public SearchPageData()
        {
        }
    }
}