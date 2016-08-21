using System;
using System.Collections.Generic;
using System.Linq;
using TrafficData.DataAccess;
//The class is used as a service which will be used by ViewModel to return the data to the UI
namespace TrafficData.Model
{
    public class DataService : IDataService
    {
        //Get dashboard data content
        public MainPageData GetMainPageData(KeyValuePair<DateTime, string> datetimeInfo)
        {
            var mainPageData = new MainPageData();
            var dal = new DataAccess.DataAccess();
            var dateTime = datetimeInfo.Key;
            var timeUnit = datetimeInfo.Value;

            var totals = dal.GetTotals(dateTime, timeUnit);
            mainPageData.TotalVehicles = totals.Item1;
            mainPageData.TotalGrossWeight = totals.Item2;
            mainPageData.AverageSpeed = totals.Item3;
            mainPageData.MostUsedLane = totals.Item4;
            mainPageData.LeastUsedLane = totals.Item5;

            var vehicleChartData = dal.GetVehicleChartData(dateTime, timeUnit);
            mainPageData.VehicleChartData = vehicleChartData;

            var weightGrossData = dal.GetWeightGrossChartData(dateTime, timeUnit);
            mainPageData.GrossWeightChartData = weightGrossData;

            var averageSpeedData = dal.GetAverageSpeedChartData(dateTime, timeUnit);
            mainPageData.AverageSpeedChartData = averageSpeedData;

            var laneUsedData = dal.GetUsedLaneChartData(dateTime, timeUnit);
            mainPageData.UsedLaneChartData = laneUsedData;

            return mainPageData;
        }
        //Get search data content
        public SearchPageData GetSearchPageData(List<QueryInfo> queryInfos)
        {
            var searchPageData = new SearchPageData();
            var dal = new DataAccess.DataAccess();

            var data = new List<KeyValuePair<string, object>>();

            var index = 1;
            foreach (var queryInfo in queryInfos)
            {
                data.AddRange(dal.GetVehicleChartDataWithFilters(queryInfo.StartTime, queryInfo.EndTime, queryInfo.TimeUnit, queryInfo.Filters, queryInfo.Index));
                index++;
            }

            List<KeyValuePair<string,object>> orderedList = null;
            if (queryInfos.Count > 0 && queryInfos.First().TimeUnit != "Year")
            {
                orderedList = data.OrderBy(x => DateTime.Parse(x.Key.Split(' ')[0])).ToList();
            }
            else
            {
                orderedList = data.OrderBy(x => Int32.Parse(x.Key.Split(' ')[0])).ToList();
            }

            searchPageData.VehicleChartData = orderedList;

            return searchPageData;
        }
        //Get caculated data content
        public SearchPageData GetSearchPageDataForCalculatedData(QueryInfo queryInfo)
        {
            var searchPageData = new SearchPageData();
            var dal = new DataAccess.DataAccess();

            var data = dal.GetCalculatedChartData(queryInfo.StartTime, queryInfo.EndTime, queryInfo.TimeUnit, queryInfo.Filters);

            // Order data
            List<KeyValuePair<string, object>> averageFlowOrderedList = null;
            if (queryInfo.TimeUnit != "Year")
            {
                averageFlowOrderedList = data.Item1.OrderBy(x => DateTime.Parse(x.Key.Split(' ')[0])).ToList();
            }
            else
            {
                averageFlowOrderedList = data.Item1.OrderBy(x => Int32.Parse(x.Key.Split(' ')[0])).ToList();
            }

            List<KeyValuePair<string, object>> averageDensistyOrderedList = null;
            if (queryInfo.TimeUnit != "Year")
            {
                averageDensistyOrderedList = data.Item2.OrderBy(x => DateTime.Parse(x.Key.Split(' ')[0])).ToList();
            }
            else
            {
                averageDensistyOrderedList = data.Item2.OrderBy(x => Int32.Parse(x.Key.Split(' ')[0])).ToList();
            }


            searchPageData.AverageTrafficFlowChartData = averageFlowOrderedList;
            searchPageData.AverageTrafficDensityChartData = averageDensistyOrderedList;


            return searchPageData;

        }
        //Get weight data content
        public SearchPageData GetSearchPageDataForWeight(List<QueryInfo> queryInfos)
        {
            var searchPageData = new SearchPageData();
            var dal = new DataAccess.DataAccess();

            var data = new Dictionary<string, List<KeyValuePair<string, object>>>();

            foreach (var queryInfo in queryInfos)
            {
                var queryInfoData = dal.GetWeightChartDataWithFilters(queryInfo.StartTime, queryInfo.EndTime, queryInfo.TimeUnit, queryInfo.Filters, queryInfo.ActiveInfos, queryInfo.Index);

                foreach(var entry in queryInfoData)
                {
                    if(data.ContainsKey(entry.Key))
                    {
                        data[entry.Key].AddRange(entry.Value);
                    }
                    else
                    {
                        data.Add(entry.Key, entry.Value);
                    }
                }
            }

            var newData = new Dictionary<string, List<KeyValuePair<string, object>>>();
            foreach(var entry in data)
            {
                List<KeyValuePair<string, object>> orderedList = null;
                if (entry.Key.Equals("Histogram") || entry.Key.Equals("Percentile"))
                {
                    orderedList = entry.Value.OrderBy(x => Int32.Parse(x.Key.Split(' ')[0])).ToList();
                }
                else if (queryInfos.Count > 0 && queryInfos.First().TimeUnit != "Year")
                {
                    orderedList = entry.Value.OrderBy(x => DateTime.Parse(x.Key.Split(' ')[0])).ToList();
                }
                else
                {
                    orderedList = entry.Value.OrderBy(x => Int32.Parse(x.Key.Split(' ')[0])).ToList();
                }

                newData.Add(entry.Key, orderedList);
            }

            searchPageData.AverageWeigthChartData = newData.ContainsKey("Average") ? newData["Average"] : null;
            searchPageData.HighestWeigthChartData = newData.ContainsKey("Highest") ? newData["Highest"] : null;
            searchPageData.LowestWeigthChartData = newData.ContainsKey("Lowest") ? newData["Lowest"] : null;
            searchPageData.NthTileWeigthChartData = newData.ContainsKey("NthTile") ? newData["NthTile"] : null;
            searchPageData.WeightHistogramChartData = newData.ContainsKey("Histogram") ? newData["Histogram"] : null;
            searchPageData.WeightPercentileChartData = newData.ContainsKey("Percentile") ? newData["Percentile"] : null;

            return searchPageData;
        }
        //Get speed data content
        public SearchPageData GetSearchPageDataForSpeed(List<QueryInfo> queryInfos)
        {
            var searchPageData = new SearchPageData();
            var dal = new DataAccess.DataAccess();

            var data = new Dictionary<string, List<KeyValuePair<string, object>>>();

            foreach (var queryInfo in queryInfos)
            {
                var queryInfoData = dal.GetSpeedChartDataWithFilters(queryInfo.StartTime, queryInfo.EndTime, queryInfo.TimeUnit, queryInfo.Filters, queryInfo.ActiveInfos, queryInfo.Index);

                foreach (var entry in queryInfoData)
                {
                    if (data.ContainsKey(entry.Key))
                    {
                        data[entry.Key].AddRange(entry.Value);
                    }
                    else
                    {
                        data.Add(entry.Key, entry.Value);
                    }
                }
            }

            var newData = new Dictionary<string, List<KeyValuePair<string, object>>>();
            foreach (var entry in data)
            {
                List<KeyValuePair<string, object>> orderedList = null;
                if (entry.Key.Equals("Histogram") || entry.Key.Equals("Percentile"))
                {
                    orderedList = entry.Value.OrderBy(x => Int32.Parse(x.Key.Split(' ')[0])).ToList();
                }
                else if (queryInfos.Count > 0 && queryInfos.First().TimeUnit != "Year")
                {
                    orderedList = entry.Value.OrderBy(x => DateTime.Parse(x.Key.Split(' ')[0])).ToList();
                }
                else
                {
                    orderedList = entry.Value.OrderBy(x => Int32.Parse(x.Key.Split(' ')[0])).ToList();
                }

                newData.Add(entry.Key, orderedList);
            }

            searchPageData.AverageSpeedChartData = newData.ContainsKey("Average") ? newData["Average"] : null;
            searchPageData.HighestSpeedChartData = newData.ContainsKey("Highest") ? newData["Highest"] : null;
            searchPageData.LowestSpeedChartData = newData.ContainsKey("Lowest") ? newData["Lowest"] : null;
            searchPageData.NthTileSpeedChartData = newData.ContainsKey("NthTile") ? newData["NthTile"] : null;
            searchPageData.SpeedHistogramChartData = newData.ContainsKey("Histogram") ? newData["Histogram"] : null;
            searchPageData.SpeedPercentileChartData = newData.ContainsKey("Percentile") ? newData["Percentile"] : null;

            return searchPageData;
        }
        //Get length data content
        public SearchPageData GetSearchPageDataForLenght(List<QueryInfo> queryInfos)
        {
            var searchPageData = new SearchPageData();
            var dal = new DataAccess.DataAccess();

            var data = new Dictionary<string, List<KeyValuePair<string, object>>>();

            foreach (var queryInfo in queryInfos)
            {
                var queryInfoData = dal.GetLengthChartDataWithFilters(queryInfo.StartTime, queryInfo.EndTime, queryInfo.TimeUnit, queryInfo.Filters, queryInfo.ActiveInfos, queryInfo.Index);

                foreach (var entry in queryInfoData)
                {
                    if (data.ContainsKey(entry.Key))
                    {
                        data[entry.Key].AddRange(entry.Value);
                    }
                    else
                    {
                        data.Add(entry.Key, entry.Value);
                    }
                }
            }

            var newData = new Dictionary<string, List<KeyValuePair<string, object>>>();
            foreach (var entry in data)
            {
                List<KeyValuePair<string, object>> orderedList = null;
                if (entry.Key.Equals("Histogram") || entry.Key.Equals("Percentile"))
                {
                    orderedList = entry.Value.OrderBy(x => Int32.Parse(x.Key.Split(' ')[0])).ToList();
                }
                else if (queryInfos.Count > 0 && queryInfos.First().TimeUnit != "Year")
                {
                    orderedList = entry.Value.OrderBy(x => DateTime.Parse(x.Key.Split(' ')[0])).ToList();
                }
                else
                {
                    orderedList = entry.Value.OrderBy(x => Int32.Parse(x.Key.Split(' ')[0])).ToList();
                }

                newData.Add(entry.Key, orderedList);
            }

            searchPageData.AverageLengthChartData = newData.ContainsKey("Average") ? newData["Average"] : null;
            searchPageData.HighestLengthChartData = newData.ContainsKey("Highest") ? newData["Highest"] : null;
            searchPageData.LowestLengthChartData = newData.ContainsKey("Lowest") ? newData["Lowest"] : null;
            searchPageData.NthTileLengthChartData = newData.ContainsKey("NthTile") ? newData["NthTile"] : null;
            searchPageData.LengthHistogramChartData = newData.ContainsKey("Histogram") ? newData["Histogram"] : null;
            searchPageData.LengthPercentileChartData = newData.ContainsKey("Percentile") ? newData["Percentile"] : null;

            return searchPageData;
        }
        //Get headway data content
        public SearchPageData GetSearchPageDataForHeadway(List<QueryInfo> queryInfos)
        {
            var searchPageData = new SearchPageData();
            var dal = new DataAccess.DataAccess();

            var data = new Dictionary<string, List<KeyValuePair<string, object>>>();

            foreach (var queryInfo in queryInfos)
            {
                var queryInfoData = dal.GetHeadwayChartDataWithFilters(queryInfo.StartTime, queryInfo.EndTime, queryInfo.TimeUnit, queryInfo.Filters, queryInfo.ActiveInfos, queryInfo.Index);

                foreach (var entry in queryInfoData)
                {
                    if (data.ContainsKey(entry.Key))
                    {
                        data[entry.Key].AddRange(entry.Value);
                    }
                    else
                    {
                        data.Add(entry.Key, entry.Value);
                    }
                }
            }

            var newData = new Dictionary<string, List<KeyValuePair<string, object>>>();
            foreach (var entry in data)
            {

                    List<KeyValuePair<string, object>> orderedList = null;
                if(entry.Key.Equals("Histogram") || entry.Key.Equals("Percentile"))
                {
                    orderedList = entry.Value.OrderBy(x => Int32.Parse(x.Key.Split(' ')[0])).ToList();
                }
                    else if (queryInfos.Count > 0 && queryInfos.First().TimeUnit != "Year")
                    {
                        orderedList = entry.Value.OrderBy(x => DateTime.Parse(x.Key.Split(' ')[0])).ToList();
                    }
                    else
                    {
                        orderedList = entry.Value.OrderBy(x => Int32.Parse(x.Key.Split(' ')[0])).ToList();
                    }
                

                newData.Add(entry.Key, orderedList);
            }

            searchPageData.AverageHeadwayChartData = newData.ContainsKey("Average") ? newData["Average"] : null;
            searchPageData.HighestHeadwayChartData = newData.ContainsKey("Highest") ? newData["Highest"] : null;
            searchPageData.LowestHeadwayChartData = newData.ContainsKey("Lowest") ? newData["Lowest"] : null;
            searchPageData.NthTileHeadwayChartData = newData.ContainsKey("NthTile") ? newData["NthTile"] : null;
            searchPageData.HeadwayHistogramChartData = newData.ContainsKey("Histogram") ? newData["Histogram"] : null;
            searchPageData.HeadwayPercentileChartData = newData.ContainsKey("Percentile") ? newData["Percentile"] : null;

            return searchPageData;
        }
    }
    //This class is used as a model for the query
    public class QueryInfo
    {
        public string TimeUnit { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public DateTime StartHour
        {
            get; set;
        }

        public DateTime EndHour
        {
            get; set;
        }

        public Dictionary<string, List<KeyValuePair<bool, object>>> Filters { get; set; }
        public int Index { get; internal set; }
        public Dictionary<string, bool> ActiveInfos { get; internal set; }
    }
}