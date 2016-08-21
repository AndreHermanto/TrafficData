using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TrafficData.DataAccess
{
    public class DataAccess
    {
        private SQLiteConnection sqlite;
        //Configure Database connection
        public DataAccess()
        {
            
            string databaseName = ConfigurationManager.AppSettings["databaseName"].ToString();
            sqlite = new SQLiteConnection(string.Format("Data Source={0}",databaseName));

        }
        //Building a string query
        private DataTable SelectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
                throw ex;
            }
            sqlite.Close();
            return dt;
        }
        //Total Vehicle, Heavy Vehicles Percentage, Lane usage, Average speed query 
        public Tuple<int, int, decimal, int, int> GetTotals(DateTime dateTime, string timeUnit)
        {
            string startDate = null;
            string endDate = null;

            if (timeUnit.Equals("Day")) // TODO change this to enum
            {
                startDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).ToString("yyyy-MM-dd HH:mm:ss");
                endDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (timeUnit.Equals("Month"))
            {
                startDate = new DateTime(dateTime.Year, dateTime.Month, 1).ToString("yyyy-MM-dd HH:mm:ss");
                var lastDay = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                endDate = new DateTime(dateTime.Year, dateTime.Month, lastDay, 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (timeUnit.Equals("Year"))
            {
                startDate = new DateTime(dateTime.Year, 1, 1).ToString("yyyy-MM-dd HH:mm:ss");
                var lastDay = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                endDate = new DateTime(dateTime.Year, 12, lastDay, 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");
            }


            int totalVehicles = 0;
            int totalGrossWeight = 0;
            decimal averageSpeed = 0;
            int mostUsedLane = 0;
            int leastUsedLane = 0;

            var totalsCommand = string.Format("SELECT count(*), avg(speed), sum(grossweight) FROM Vehicles where DateTimeLocal >= Datetime('{0}') and DateTimeLocal <= Datetime('{1}')", startDate, endDate);
            var totalsDataTable = SelectQuery(totalsCommand);

            if (totalsDataTable.Rows.Count > 0)
            {
                int.TryParse(totalsDataTable.Rows[0][0].ToString(), out totalVehicles);
                decimal.TryParse(totalsDataTable.Rows[0][1].ToString(), out averageSpeed);
                //int.TryParse(totalsDataTable.Rows[0][2].ToString(), out totalGrossWeight);
            }

            var usedLanesCommand = string.Format("Select lane, count(*) from Vehicles  where DateTimeLocal >= Datetime('{0}') and DateTimeLocal <= Datetime('{1}') group by lane order by count(*)", startDate, endDate);
            var mostUsedLaneDataTable = SelectQuery(usedLanesCommand);


            if (mostUsedLaneDataTable.Rows.Count > 0)
            {
                int.TryParse(mostUsedLaneDataTable.Rows[0][0].ToString(), out mostUsedLane);
                int.TryParse(mostUsedLaneDataTable.Rows[mostUsedLaneDataTable.Rows.Count - 1][0].ToString(), out leastUsedLane);
            }

            var heavyVehTot = string.Format("SELECT count(*) FROM Vehicles where AxleClass > 2 and DateTimeLocal >= Datetime('{0}') and DateTimeLocal <= Datetime('{1}')", startDate, endDate);
            var vehTot = string.Format("SELECT count(*) FROM Vehicles where DateTimeLocal >= Datetime('{0}') and DateTimeLocal <= Datetime('{1}')", startDate, endDate);
            var heavyVehTotTable = SelectQuery(heavyVehTot);
            var vehTotTable = SelectQuery(vehTot);

            if (double.Parse(heavyVehTotTable.Rows[0][0].ToString()) > 0)
            {
                double temp = double.Parse(heavyVehTotTable.Rows[0][0].ToString()) / double.Parse(vehTotTable.Rows[0][0].ToString()) * 100;
                totalGrossWeight = (int)temp;
            }


            return new Tuple<int, int, decimal, int, int>(totalVehicles, totalGrossWeight, averageSpeed, mostUsedLane, leastUsedLane);
        }
        //Displaying data as a chart for dashboard page
        private List<KeyValuePair<string, object>> GetChartData<T>(DateTime dateTime, string timeUnit, string operation)
        {
            var dictionaryData = new Dictionary<string, object>();
            var delta = -5;
            var timeUnitGroupByClause = "%d-%m-%Y";
            var dateTimeFormat = "dd-MM-yyyy";
            string startDate = null;
            string endDate = null;
            List<string> timeUnitsToConsider = new List<string>();



            if (timeUnit.Equals("Day")) // TODO change this to enum
            {
                timeUnitGroupByClause = "%d-%m-%Y";
                dateTimeFormat = "dd-MM-yyyy";
                var startDateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).AddDays(delta);
                startDate = startDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                endDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");

                for (int i = 1; i <= Math.Abs(delta); i++)
                {
                    dictionaryData.Add(startDateTime.AddDays(i).ToString(dateTimeFormat), Convert.ChangeType(0, typeof(T)));
                }
            }
            else if (timeUnit.Equals("Month"))
            {
                timeUnitGroupByClause = "%m-%Y";
                dateTimeFormat = "MM-yyyy";
                var startDateTime = new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(delta);
                startDate = startDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                var lastDay = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                endDate = new DateTime(dateTime.Year, dateTime.Month, lastDay, 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");

                for (int i = 1; i <= Math.Abs(delta); i++)
                {
                    dictionaryData.Add(startDateTime.AddMonths(i).ToString(dateTimeFormat), 0);
                }
            }
            else if (timeUnit.Equals("Year"))
            {
                timeUnitGroupByClause = "%Y";
                dateTimeFormat = "yyyy";
                var startDateTime = new DateTime(dateTime.Year, 1, 1).AddYears(delta);
                startDate = startDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                var lastDay = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                endDate = new DateTime(dateTime.Year, 12 , lastDay, 23, 59, 59).ToString("yyyy-MM-dd HH:mm:ss");

                for (int i = 1; i <= Math.Abs(delta); i++)
                {
                    dictionaryData.Add(startDateTime.AddYears(i).ToString(dateTimeFormat), 0);
                }
            }


            var command = "select strftime(\"{3}\", DateTimeLocal), {2} from vehicles where DateTimeLocal >= Datetime('{0}') and DateTimeLocal <= Datetime('{1}') group by strftime(\"{3}\", datetimelocal)";

            var data = SelectQuery(string.Format(command, startDate, endDate, operation, timeUnitGroupByClause));

            var chartDataPoints = new List<KeyValuePair<string, object>>();
            foreach (DataRow point in data.Rows)
            {
                var key = point[0].ToString();
                if (dictionaryData.ContainsKey(key))
                {
                    dictionaryData[key] = Convert.ChangeType(point[1], typeof(T));
                }
                //chartDataPoints.Add(new KeyValuePair<string, object>(point[0].ToString(), point[1]));
            }

            foreach (var entry in dictionaryData)
            {
                chartDataPoints.Add(new KeyValuePair<string, object>(entry.Key, entry.Value));
            }
            return chartDataPoints;
        }
        //Displaying data as a chart for search page
        private List<KeyValuePair<string, object>> GetChartData<T>(DateTime startTime, DateTime endTime, string timeUnit, string operation, string filter = null, int? index = null, string lastClause = null)
        {
            var dictionaryData = new Dictionary<string, object>();

            var timeUnitGroupByClause = "%d-%m-%Y";
            var dateTimeFormat = "dd-MM-yyyy";
            string startDate = null;
            string endDate = null;
            List<string> timeUnitsToConsider = new List<string>();
            string appendindex = index != null ? " (" + index + ")" : null;

            if (timeUnit.Equals("Day")) // TODO change this to enum
            {
                timeUnitGroupByClause = "%d-%m-%Y";
                dateTimeFormat = "dd-MM-yyyy";
                var startDateTime = new DateTime(startTime.Year, startTime.Month, startTime.Day);
                startDate = startDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                var endDateTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, 23, 59, 59);
                endDate = endDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                

                System.TimeSpan diff = endDateTime.Subtract(startDateTime);
                for (int i = 0; i <= diff.Days; i++)
                {
                    dictionaryData.Add(startDateTime.AddDays(i).ToString(dateTimeFormat) + appendindex, Convert.ChangeType(0, typeof(T)));
                }
            }
            else if (timeUnit.Equals("Month"))
            {
                timeUnitGroupByClause = "%m-%Y";
                dateTimeFormat = "MM-yyyy";
                var startDateTime = new DateTime(startTime.Year, startTime.Month, 1);
                startDate = startDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                var lastDay = DateTime.DaysInMonth(endTime.Year, endTime.Month);
                var endDateTime = new DateTime(endTime.Year, endTime.Month, lastDay, 23, 59, 59);
                endDate = endDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                int diff = MonthDifference(startDateTime, endDateTime);
                for (int i = 0; i <= diff; i++)
                {
                    dictionaryData.Add(startDateTime.AddMonths(i).ToString(dateTimeFormat) + appendindex, Convert.ChangeType(0, typeof(T)));
                }
            }
            else if (timeUnit.Equals("Year"))
            {
                timeUnitGroupByClause = "%Y";
                dateTimeFormat = "yyyy";
                var startDateTime = new DateTime(startTime.Year, 1, 1);
                startDate = startDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                var lastDay = DateTime.DaysInMonth(endTime.Year, endTime.Month);
                var endDateTime = new DateTime(endTime.Year, 12, lastDay, 23, 59, 59);
                endDate = endDateTime.ToString("yyyy-MM-dd HH:mm:ss");

                var diff = endDateTime.Year - startDateTime.Year;
                for (int i = 0; i <= diff; i++)
                {
                    dictionaryData.Add(startDateTime.AddYears(i).ToString(dateTimeFormat) + appendindex, Convert.ChangeType(0, typeof(T)));
                }
            }

            if(lastClause != null)
            {
                dictionaryData.Clear();
            }

            lastClause = lastClause ?? "group by strftime(\"{3}\", datetimelocal)";
            
            var command = "select strftime(\"{3}\", DateTimeLocal) as daydate, {2} , time(DateTimeLocal) as hour from vehicles where DateTimeLocal >= Datetime('{0}') and DateTimeLocal <= Datetime('{1}') {4} " + lastClause;

            var data = SelectQuery(string.Format(command, startDate, endDate, operation, timeUnitGroupByClause, filter));

            var chartDataPoints = new List<KeyValuePair<string, object>>();
            foreach (DataRow point in data.Rows)
            {
                dictionaryData[point[0].ToString() + appendindex] = Convert.ChangeType(point[1], typeof(T));
                //chartDataPoints.Add(new KeyValuePair<string, object>(point[0].ToString(), point[1]));
            }

            foreach (var entry in dictionaryData)
            {
                chartDataPoints.Add(new KeyValuePair<string, object>(entry.Key, entry.Value));
            }
            return chartDataPoints;
        }
        //Total vehicle chart
        public List<KeyValuePair<string, object>> GetVehicleChartData(DateTime dateTime, string timeUnit)
        {
            return GetChartData<long>(dateTime, timeUnit, "count(*)");
        }
        //Percentages vehicle per axle class pie chart
        public List<KeyValuePair<string, object>> GetWeightGrossChartData(DateTime dateTime, string timeUnit)
        {
            var chartData = new List<KeyValuePair<string, object>>();
            var date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
            var endDate = date.ToString("yyyy-MM-dd HH:mm:ss");
            var startDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).ToString("yyyy-MM-dd HH:mm:ss");

            if (timeUnit.Equals("Day")) // TODO change this to enum
            {
                date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
                endDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                startDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (timeUnit.Equals("Month"))
            {
                var lastDay = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                date = new DateTime(dateTime.Year, dateTime.Month, lastDay, 23, 59, 59);
                endDate = date.ToString("yyyy-MM-dd HH:mm:ss");

                startDate = new DateTime(dateTime.Year, dateTime.Month, 1).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (timeUnit.Equals("Year"))
            {
                var lastDay = DateTime.DaysInMonth(dateTime.Year, 12);
                date = new DateTime(dateTime.Year, 12, lastDay, 23, 59, 59);
                endDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                startDate = new DateTime(dateTime.Year, 1, 1).ToString("yyyy-MM-dd HH:mm:ss");
            }

            var usedLanesCommand = string.Format("Select AxleClass, count(*) from Vehicles  where DateTimeLocal >= Datetime('{0}') and DateTimeLocal <= Datetime('{1}') group by AxleClass", startDate, endDate);
            var mostUsedLaneDataTable = SelectQuery(usedLanesCommand);

            foreach (DataRow point in mostUsedLaneDataTable.Rows)
            {
                chartData.Add(new KeyValuePair<string, object>("Class " + point[0].ToString(), point[1]));
            }

            return chartData;
        }
        //Average speed chart
        public List<KeyValuePair<string, object>> GetAverageSpeedChartData(DateTime dateTime, string timeUnit)
        {
            return GetChartData<double>(dateTime, timeUnit, "avg(speed)");
        }
        //Comparison lane used pie chart
        public List<KeyValuePair<string, object>> GetUsedLaneChartData(DateTime dateTime, string timeUnit)
        {
            // TODO Time Units 

            var chartData = new List<KeyValuePair<string, object>>();
            var date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
            var endDate = date.ToString("yyyy-MM-dd HH:mm:ss");
            var startDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).ToString("yyyy-MM-dd HH:mm:ss");

            if (timeUnit.Equals("Day")) // TODO change this to enum
            {
                date = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 23, 59, 59);
                endDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                startDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (timeUnit.Equals("Month"))
            {
                var lastDay = DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
                date = new DateTime(dateTime.Year, dateTime.Month, lastDay, 23, 59, 59);
                endDate = date.ToString("yyyy-MM-dd HH:mm:ss");

                startDate = new DateTime(dateTime.Year, dateTime.Month, 1).ToString("yyyy-MM-dd HH:mm:ss");
            }
            else if (timeUnit.Equals("Year"))
            {
                var lastDay = DateTime.DaysInMonth(dateTime.Year, 12);
                date = new DateTime(dateTime.Year, 12, lastDay, 23, 59, 59);
                endDate = date.ToString("yyyy-MM-dd HH:mm:ss");
                startDate = new DateTime(dateTime.Year, 1, 1).ToString("yyyy-MM-dd HH:mm:ss");
            }

            var usedLanesCommand = string.Format("Select lane, count(*) from Vehicles  where DateTimeLocal >= Datetime('{0}') and DateTimeLocal <= Datetime('{1}') group by lane", startDate, endDate);
            var mostUsedLaneDataTable = SelectQuery(usedLanesCommand);

            foreach (DataRow point in mostUsedLaneDataTable.Rows)
            {
                chartData.Add(new KeyValuePair<string, object>(point[0].ToString(), point[1]));
            }

            return chartData;
        }
        //Get total vehicle chart with additional filter as parameters
        public List<KeyValuePair<string, object>> GetVehicleChartDataWithFilters(DateTime startDate, DateTime endDate, string timeUnit, Dictionary<string, List<KeyValuePair<bool, object>>> filters, int index)
        {
            var filter = new StringBuilder();

            if (filters.ContainsKey("Weight"))
            {
                filter.AppendFormat(" AND grossweight >= {0} AND grossweight <= {1}", filters["Weight"][0].Value, filters["Weight"][1].Value);
            }

            if (filters.ContainsKey("Axle"))
            {
                filter.AppendFormat(" AND axleclass = {0} ", filters["Axle"][0].Value);
            }

            if (filters.ContainsKey("Length"))
            {
                if (filters["Length"][0].Key == true) // class
                {
                    filter.AppendFormat(" AND LoopClass >= {0} AND LoopClass <= {1}", filters["Length"][0].Value, filters["Length"][1].Value);
                }
                else
                {
                    filter.AppendFormat(" AND Length >= {0} AND Length <= {1}", filters["Length"][0].Value, filters["Length"][1].Value);
                }

            }

            if (filters.ContainsKey("Speed"))
            {
                filter.AppendFormat(" AND speed >= {0} AND speed <= {1}", filters["Speed"][0].Value, filters["Speed"][1].Value);
            }

            if (filters.ContainsKey("Lane") && filters["Lane"].Exists(f => ((bool)f.Value) == true))
            {
                filter.Append(" AND ( ");
                var laneNumber = 1;
                foreach (var entry in filters["Lane"])
                {
                    if (((bool)entry.Value))
                    {
                        filter.AppendFormat(" lane = {0} OR ", laneNumber);
                    }

                    laneNumber++;
                }
                filter.Remove(filter.Length - 3, 3);
                filter.Append(" ) ");
            }

            if (filters.ContainsKey("Time"))
            {
                var starttime = ((DateTime)filters["Time"][0].Value).ToString("HH:mm:ss");
                var endtime = ((DateTime)filters["Time"][1].Value).ToString("HH:mm:ss");
                filter.AppendFormat(" AND hour >= time('{0}') AND hour <= time('{1}')", starttime, endtime);
            }

            return GetChartData<long>(startDate, endDate, timeUnit, "count(*)", filter.ToString(), index);
        }
        //Get weight chart with additional filter as parameters
        public Dictionary<string,List<KeyValuePair<string, object>>> GetWeightChartDataWithFilters(DateTime startDate, DateTime endDate, string timeUnit, Dictionary<string, List<KeyValuePair<bool, object>>> filters, Dictionary<string,bool> activeInfos, int index, bool isToUpgradeHistogram = true)
        {
            Dictionary<string, List<KeyValuePair<string, object>>> chartDataSets = new Dictionary<string, List<KeyValuePair<string, object>>>();

            var filter = new StringBuilder();

            if (filters.ContainsKey("Lane") && filters["Lane"].Exists(f => ((bool)f.Value) == true))
            {
                filter.Append(" AND ( ");
                var laneNumber = 1;
                foreach (var entry in filters["Lane"])
                {
                    if (((bool)entry.Value))
                    {
                        filter.AppendFormat(" lane = {0} OR ", laneNumber);
                    }

                    laneNumber++;
                }
                filter.Remove(filter.Length - 3, 3);
                filter.Append(" ) ");
            }

            if (filters.ContainsKey("Time"))
            {
                var starttime = ((DateTime)filters["Time"][0].Value).ToString("HH:mm:ss");
                var endtime = ((DateTime)filters["Time"][1].Value).ToString("HH:mm:ss");
                filter.AppendFormat(" AND hour >= time('{0}') AND hour <= time('{1}')", starttime, endtime);
            }

            //var totalWeight = GetChartData<long>(startDate, endDate, timeUnit, "SUM(grossweight)", filter.ToString(), index);

            if (activeInfos["Average"] == true)
            {
                chartDataSets["Average"] = (GetChartData<long>(startDate, endDate, timeUnit, "avg(grossweight)", filter.ToString(), index));
            }

            if(activeInfos["Highest"] == true)
            {
                chartDataSets["Highest"] = (GetChartData<long>(startDate, endDate, timeUnit, "max(grossweight)", filter.ToString(), index));
            }

            if (activeInfos["Lowest"] == true)
            {
                chartDataSets["Lowest"] = (GetChartData<long>(startDate, endDate, timeUnit, "min(grossweight)", filter.ToString(), index));
            }

            if (activeInfos["NthTile"] == true)
            {
                var percentile = (int)filters["Percentile"].First().Value;
                List<KeyValuePair<string, object>> data = GetPercentileData(startDate, endDate, timeUnit, index, filter, percentile, "grossweight");

                chartDataSets["NthTile"] = data;
            }

                if (activeInfos["Histogram"] == true)
                {
                    var newFilter = filter.ToString();
                    var data = new List<KeyValuePair<string, object>>();

                var totals1StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND AxleClass >= 1 AND AxleClass <= 2 ", index));
                data.Add(new KeyValuePair<string, object>("1 and 2 (" + index + ")", totals1StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals2StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND AxleClass >= 3 AND AxleClass <= 4 ", index));
                data.Add(new KeyValuePair<string, object>("3 and 4 (" + index + ")", totals2StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals3StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND AxleClass >= 5 AND AxleClass <= 6 ", index));
                data.Add(new KeyValuePair<string, object>("5 and 6 (" + index + ")", totals3StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals4StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND AxleClass >= 7 AND AxleClass <= 8 ", index));
                data.Add(new KeyValuePair<string, object>("7 and 8 (" + index + ")", totals4StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals5StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND AxleClass >= 9", index));
                data.Add(new KeyValuePair<string, object>("9 + (" + index + ")", totals5StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                chartDataSets["Histogram"] = data;
                }
                if (activeInfos["Percentile"] == true)
                {
                    var data = new List<KeyValuePair<string, object>>();
                    var totalsData = GetData<long>(startDate, endDate, "count(*)", filter.ToString());
                    var total = Convert.ToInt64(totalsData.First().Value.ToString());

                    if (total > 0)
                    {
                        var percentileData = GetData<long>(startDate, endDate, "grossweight", filter.ToString()+ " order by grossweight ", true);

                        for (int percentile = 1; percentile < 101; percentile++)
                        {
                                var entryToGet = (int) Math.Floor((percentile / 100.0) * (total + 1));
                        entryToGet = entryToGet > percentileData.Count - 1 ? entryToGet - 1 : entryToGet;
                        data.Add(new KeyValuePair<string, object>(percentile.ToString() + " (" + index + ")", Convert.ToInt64(percentileData[entryToGet > 0 ? entryToGet - 1 : 0].Value)));

                        }
                    }
                    
                    chartDataSets["Percentile"] = data;

                }

            return chartDataSets;
        }
        //Get speed chart with additional filter as parameters
        public Dictionary<string, List<KeyValuePair<string, object>>> GetSpeedChartDataWithFilters(DateTime startDate, DateTime endDate, string timeUnit, Dictionary<string, List<KeyValuePair<bool, object>>> filters, Dictionary<string, bool> activeInfos, int index, bool isToUpgradeHistogram = true)
        {
            Dictionary<string, List<KeyValuePair<string, object>>> chartDataSets = new Dictionary<string, List<KeyValuePair<string, object>>>();

            var filter = new StringBuilder();

            if (filters.ContainsKey("Lane") && filters["Lane"].Exists(f => ((bool)f.Value) == true))
            {
                filter.Append(" AND ( ");
                var laneNumber = 1;
                foreach (var entry in filters["Lane"])
                {
                    if (((bool)entry.Value))
                    {
                        filter.AppendFormat(" lane = {0} OR ", laneNumber);
                    }

                    laneNumber++;
                }
                filter.Remove(filter.Length - 3, 3);
                filter.Append(" ) ");
            }

            if (filters.ContainsKey("Time"))
            {
                var starttime = ((DateTime)filters["Time"][0].Value).ToString("HH:mm:ss");
                var endtime = ((DateTime)filters["Time"][1].Value).ToString("HH:mm:ss");
                filter.AppendFormat(" AND hour >= time('{0}') AND hour <= time('{1}')", starttime, endtime);
            }

            if (activeInfos["Average"] == true)
            {
                chartDataSets["Average"] = (GetChartData<long>(startDate, endDate, timeUnit, "avg(speed)", filter.ToString(), index));
            }

            if (activeInfos["Highest"] == true)
            {
                chartDataSets["Highest"] = (GetChartData<long>(startDate, endDate, timeUnit, "max(speed)", filter.ToString(), index));
            }

            if (activeInfos["Lowest"] == true)
            {
                chartDataSets["Lowest"] = (GetChartData<long>(startDate, endDate, timeUnit, "min(speed)", filter.ToString(), index));
            }

            if (activeInfos["NthTile"] == true)
            {
                var percentile = (int)filters["Percentile"].First().Value;
                List<KeyValuePair<string, object>> data = GetPercentileData(startDate, endDate, timeUnit, index, filter, percentile, "speed");

                chartDataSets["NthTile"] = data;
            }

            if (activeInfos["Histogram"] == true)
            {
                var newFilter = filter.ToString();
                var data = new List<KeyValuePair<string, object>>();

                var totals1StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND speed >= 0 AND speed <= 20 ", index));
                data.Add(new KeyValuePair<string, object>("0 to 20 (" + index + ")", totals1StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals2StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND speed >= 21 AND speed <= 40 ", index));
                data.Add(new KeyValuePair<string, object>("21 to 40 (" + index + ")", totals2StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals3StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND speed >= 41 AND speed <= 60 ", index));
                data.Add(new KeyValuePair<string, object>("41 to 60 (" + index + ")", totals3StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals4StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND speed >= 61 AND speed <= 80 ", index));
                data.Add(new KeyValuePair<string, object>("61 to 80 (" + index + ")", totals4StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals5StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND speed >= 81", index));
                data.Add(new KeyValuePair<string, object>("81 + (" + index + ")", totals5StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                chartDataSets["Histogram"] = data;
            }
            if (activeInfos["Percentile"] == true)
            {
                var data = new List<KeyValuePair<string, object>>();
                var totalsData = GetData<long>(startDate, endDate, "count(*)", filter.ToString());
                var total = Convert.ToInt64(totalsData.First().Value.ToString());

                if (total > 0)
                {
                    var percentileData = GetData<long>(startDate, endDate, "speed", filter.ToString() + " order by speed ", true);

                    for (int percentile = 1; percentile < 101; percentile++)
                    {
                        var entryToGet = (int)Math.Floor((percentile / 100.0) * (total + 1));

                        entryToGet = entryToGet > percentileData.Count - 1 ? entryToGet - 1 : entryToGet;
                        data.Add(new KeyValuePair<string, object>(percentile.ToString() + " (" + index + ")", Convert.ToInt64(percentileData[entryToGet > 0 ? entryToGet - 1 : 0].Value)));
                    }
                }

                chartDataSets["Percentile"] = data;

            }

            return chartDataSets;
        }
        //Get length chart with additional filter as parameters
        public Dictionary<string, List<KeyValuePair<string, object>>> GetLengthChartDataWithFilters(DateTime startDate, DateTime endDate, string timeUnit, Dictionary<string, List<KeyValuePair<bool, object>>> filters, Dictionary<string, bool> activeInfos, int index, bool isToUpgradeHistogram = true)
        {
            Dictionary<string, List<KeyValuePair<string, object>>> chartDataSets = new Dictionary<string, List<KeyValuePair<string, object>>>();

            var filter = new StringBuilder();

            if (filters.ContainsKey("Lane") && filters["Lane"].Exists(f => ((bool)f.Value) == true))
            {
                filter.Append(" AND ( ");
                var laneNumber = 1;
                foreach (var entry in filters["Lane"])
                {
                    if (((bool)entry.Value))
                    {
                        filter.AppendFormat(" lane = {0} OR ", laneNumber);
                    }

                    laneNumber++;
                }
                filter.Remove(filter.Length - 3, 3);
                filter.Append(" ) ");
            }

            if (filters.ContainsKey("Time"))
            {
                var starttime = ((DateTime)filters["Time"][0].Value).ToString("HH:mm:ss");
                var endtime = ((DateTime)filters["Time"][1].Value).ToString("HH:mm:ss");
                filter.AppendFormat(" AND hour >= time('{0}') AND hour <= time('{1}')", starttime, endtime);
            }

            if (activeInfos["Average"] == true)
            {
                chartDataSets["Average"] = (GetChartData<long>(startDate, endDate, timeUnit, "avg(length)", filter.ToString(), index));
            }

            if (activeInfos["Highest"] == true)
            {
                chartDataSets["Highest"] = (GetChartData<long>(startDate, endDate, timeUnit, "max(length)", filter.ToString(), index));
            }

            if (activeInfos["Lowest"] == true)
            {
                chartDataSets["Lowest"] = (GetChartData<long>(startDate, endDate, timeUnit, "min(length)", filter.ToString(), index));
            }

            if (activeInfos["NthTile"] == true)
            {
                var percentile = (int)filters["Percentile"].First().Value;
                List<KeyValuePair<string, object>> data = GetPercentileData(startDate, endDate, timeUnit, index, filter, percentile, "length");

                chartDataSets["NthTile"] = data;
            }

            if (activeInfos["Histogram"] == true)
            {
                var newFilter = filter.ToString();
                var data = new List<KeyValuePair<string, object>>();

                var totals1StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND length >= 0 AND length <= 5 ", index));
                data.Add(new KeyValuePair<string, object>("0 to 5 (" + index + ")", totals1StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals2StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND length > 5 AND length <= 10 ", index));
                data.Add(new KeyValuePair<string, object>("5 to 10 (" + index + ")", totals2StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals3StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND length > 10 AND length <= 15 ", index));
                data.Add(new KeyValuePair<string, object>("10 to 15 (" + index + ")", totals3StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals4StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND length > 15 AND length <= 20 ", index));
                data.Add(new KeyValuePair<string, object>("15 to 20 (" + index + ")", totals4StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals5StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND length >= 20", index));
                data.Add(new KeyValuePair<string, object>("20 + (" + index + ")", totals5StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                chartDataSets["Histogram"] = data;
            }
            if (activeInfos["Percentile"] == true)
            {
                var data = new List<KeyValuePair<string, object>>();
                var totalsData = GetData<long>(startDate, endDate, "count(*)", filter.ToString());
                var total = Convert.ToInt64(totalsData.First().Value.ToString());

                if (total > 0)
                {
                    var percentileData = GetData<long>(startDate, endDate, "length", filter.ToString() + " order by length ", true);

                    for (int percentile = 1; percentile < 101; percentile++)
                    {
                        var entryToGet = (int)Math.Floor((percentile / 100.0) * (total + 1));
                        entryToGet = entryToGet > percentileData.Count - 1 ? entryToGet - 1 : entryToGet;
                        data.Add(new KeyValuePair<string, object>(percentile.ToString() + " (" + index + ")", Convert.ToInt64(percentileData[entryToGet > 0 ? entryToGet - 1 : 0].Value)));
                    }
                }

                chartDataSets["Percentile"] = data;

            }

            return chartDataSets;
        }
        //Get headway chart with additional filter as parameters
        public Dictionary<string, List<KeyValuePair<string, object>>> GetHeadwayChartDataWithFilters(DateTime startDate, DateTime endDate, string timeUnit, Dictionary<string, List<KeyValuePair<bool, object>>> filters, Dictionary<string, bool> activeInfos, int index, bool isToUpgradeHistogram = true)
        {
            Dictionary<string, List<KeyValuePair<string, object>>> chartDataSets = new Dictionary<string, List<KeyValuePair<string, object>>>();

            var filter = new StringBuilder();

            if (filters.ContainsKey("Lane") && filters["Lane"].Exists(f => ((bool)f.Value) == true))
            {
                filter.Append(" AND ( ");
                var laneNumber = 1;
                foreach (var entry in filters["Lane"])
                {
                    if (((bool)entry.Value))
                    {
                        filter.AppendFormat(" lane = {0} OR ", laneNumber);
                    }

                    laneNumber++;
                }
                filter.Remove(filter.Length - 3, 3);
                filter.Append(" ) ");
            }

            if (filters.ContainsKey("Time"))
            {
                var starttime = ((DateTime)filters["Time"][0].Value).ToString("HH:mm:ss");
                var endtime = ((DateTime)filters["Time"][1].Value).ToString("HH:mm:ss");
                filter.AppendFormat(" AND hour >= time('{0}') AND hour <= time('{1}')", starttime, endtime);
            }

            if (activeInfos["Average"] == true)
            {
                chartDataSets["Average"] = (GetChartData<long>(startDate, endDate, timeUnit, "avg(headway)", filter.ToString(), index));
            }

            if (activeInfos["Highest"] == true)
            {
                chartDataSets["Highest"] = (GetChartData<long>(startDate, endDate, timeUnit, "max(headway)", filter.ToString(), index));
            }

            if (activeInfos["Lowest"] == true)
            {
                chartDataSets["Lowest"] = (GetChartData<long>(startDate, endDate, timeUnit, "min(headway)", filter.ToString(), index));
            }

            if (activeInfos["NthTile"] == true)
            {
                var percentile = (int)filters["Percentile"].First().Value;
                List<KeyValuePair<string, object>> data = GetPercentileData(startDate, endDate, timeUnit, index, filter, percentile, "headway");

                chartDataSets["NthTile"] = data;
            }

            if (activeInfos["Histogram"] == true)
            {
                var newFilter = filter.ToString();
                var data = new List<KeyValuePair<string, object>>();

                var totals1StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND headway >= 0 AND headway <= 5 ", index));
                data.Add(new KeyValuePair<string, object>("0 to 5 (" + index + ")", totals1StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals2StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND headway > 5 AND headway <= 10 ", index));
                data.Add(new KeyValuePair<string, object>("5 to 10 (" + index + ")", totals2StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals3StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND headway > 10 AND headway <= 15 ", index));
                data.Add(new KeyValuePair<string, object>("10 to 15 (" + index + ")", totals3StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals4StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND headway > 15 AND headway <= 20 ", index));
                data.Add(new KeyValuePair<string, object>("15 to 20 (" + index + ")", totals4StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                var totals5StCategory = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", newFilter + " AND headway > 20", index));
                data.Add(new KeyValuePair<string, object>("20 + (" + index + ")", totals5StCategory.Sum(kv => Convert.ToInt64(kv.Value.ToString()))));

                chartDataSets["Histogram"] = data;
            }
            if (activeInfos["Percentile"] == true)
            {
                var data = new List<KeyValuePair<string, object>>();
                var totalsData = GetData<long>(startDate, endDate, "count(*)", filter.ToString());
                var total = Convert.ToInt64(totalsData.First().Value.ToString());

                if (total > 0)
                {
                    var percentileData = GetData<long>(startDate, endDate, "headway", filter.ToString() + " order by headway ", true);

                    for (int percentile = 1; percentile < 101; percentile++)
                    {
                        var entryToGet = (int)Math.Floor((percentile / 100.0) * (total + 1));
                        entryToGet = entryToGet > percentileData.Count - 1 ? entryToGet - 1 : entryToGet;
                        data.Add(new KeyValuePair<string, object>(percentile.ToString() + " (" + index + ")", Convert.ToInt64(percentileData[entryToGet > 0 ? entryToGet - 1 : 0].Value)));
                    }
                }

                chartDataSets["Percentile"] = data;

            }

            return chartDataSets;
        }
        //Get percentile chart data for Weight, Speed, Length, and Headway pages
        private List<KeyValuePair<string, object>> GetPercentileData(DateTime startDate, DateTime endDate, string timeUnit, int index, StringBuilder filter, int percentile, string field)
        {
            var totals = (GetChartData<long>(startDate, endDate, timeUnit, "count(*)", filter.ToString(), index));
            var data = new List<KeyValuePair<string, object>>();
            foreach (var entry in totals)
            {
                int total = int.Parse(entry.Value.ToString());
                if (total > 0)
                {
                    var entryToGet = (int)Math.Floor((percentile / 100.0) * (total + 1));

                    if(percentile == 100)
                    {
                        entryToGet = total - 1;
                    }

                    var newFilter = filter.ToString();

                    newFilter += "and dayDate = '" + entry.Key.Split(' ')[0] + "'";
                    var data1 = (GetChartData<long>(startDate, endDate, timeUnit, field , newFilter, index, string.Format("order by {0} LIMIT 1 offset ",field) + entryToGet));

                    foreach (var entry1 in data1)
                    {
                        data.Add(new KeyValuePair<string, object>(entry1.Key, entry1.Value));
                    }
                }
                else
                {
                    data.Add(new KeyValuePair<string, object>(entry.Key, entry.Value));
                }
            }

            return data;
        }
        //Fill in the percentile chart
        private long GetPercentileDataWihoutDate(DateTime startDate, DateTime endDate, int minPercentile, int maxPercentile)
        {
            var totalsData = GetData<long>(startDate, endDate, "count(*)");
            var total = Convert.ToInt64(totalsData.First().Value.ToString());
            
            long data = 0;

            if(total > 0)
            {
                var percentileData = GetData<long>(startDate, endDate, "grossweight", "order by grossweight", true);

                for(int percentile = minPercentile; percentile < maxPercentile; percentile++)
                {
                    var entryToGet = (int)Math.Floor((percentile / 100.0) * (total + 1));

                    data = Convert.ToInt64(percentileData[entryToGet - 2].Value);

                }        
            }
            return data;
        }

        private List<KeyValuePair<string, object>> GetData<T>(DateTime startDate, DateTime endDate, string operation, string whereClause = null, bool isCollection = false)
        {
            var command = "select {2} , time(DateTimeLocal) as hour from vehicles where DateTimeLocal >= Datetime('{0}') and DateTimeLocal <= Datetime('{1}') " + whereClause;

            var startDateStr = startDate.ToString("yyyy-MM-dd HH:mm:ss");
            var endDateStr = endDate.ToString("yyyy-MM-dd HH:mm:ss");

            var data = SelectQuery(string.Format(command, startDateStr, endDateStr, operation));
            var chartDataPoints = new List<KeyValuePair<string, object>>();

            if (isCollection)
            {
                
                foreach (DataRow point in data.Rows)
                {
                    chartDataPoints.Add(new KeyValuePair<string, object>("object", Convert.ChangeType(point[0], typeof(T))));
                }
            }
            else
            {
                var point = data.Rows.Count > 0 ? data.Rows[0] : null;
                chartDataPoints.Add(new KeyValuePair<string, object>("object", point != null? Convert.ChangeType(point[0], typeof(T)) : 0));
            }

            return chartDataPoints;
        }
        //Calculate month difference between 2 dates
        public static int MonthDifference(DateTime lValue, DateTime rValue)
        {
            return Math.Abs((lValue.Month - rValue.Month) + 12 * (lValue.Year - rValue.Year));
        }
        //Chart data for Calculated Data Page
        public Tuple<List<KeyValuePair<string, object>>, List<KeyValuePair<string, object>>> GetCalculatedChartData(DateTime startDate, DateTime endDate, string timeUnit, Dictionary<string, List<KeyValuePair<bool, object>>> filters)
        {
            var filter = new StringBuilder();

            if (filters.ContainsKey("Lane") && filters["Lane"].Exists(f => ((bool)f.Value) == true))
            {
                filter.Append(" AND ( ");
                var laneNumber = 1;
                foreach (var entry in filters["Lane"])
                {
                    if (((bool)entry.Value))
                    {
                        filter.AppendFormat(" lane = {0} OR ", laneNumber);
                    }

                    laneNumber++;
                }
                filter.Remove(filter.Length - 3, 3);
                filter.Append(" ) ");
            }
            var totalsEachTimeUnit = GetChartData<long>(startDate, endDate, timeUnit, "count(*)", filter.ToString());
            var avgSpeedEachTimeUnit = GetChartData<decimal>(startDate, endDate, timeUnit, "avg(speed)", filter.ToString());

            var avgFlowChartData = new List<KeyValuePair<string, object>>();

            foreach (var entry in totalsEachTimeUnit)
            {
                var timeValue = entry.Key;
                var countValue = Convert.ToInt64(entry.Value.ToString());
                int divisorUnit = 1;
                if(timeUnit.Equals("Day"))
                {
                    divisorUnit = 24;
                }
                else if(timeUnit.Equals("Month"))
                {
                    divisorUnit = DateTime.DaysInMonth(startDate.Year, startDate.Month) * 24;
                }
                else
                {
                    divisorUnit = 365 * 24;
                }


                avgFlowChartData.Add(new KeyValuePair<string, object>(timeValue, (double) countValue / (double)divisorUnit));
            }

            var avgDensityChartData = new List<KeyValuePair<string, object>>();

            foreach(var entry in avgFlowChartData)
            {
                var timeValue = entry.Key;
                var avgFlow = Convert.ToDouble(entry.Value.ToString());
                double density = 0;
                if (avgFlow > 0)
                {
                    var avgSpeed = Convert.ToDouble(avgSpeedEachTimeUnit.SingleOrDefault(item => item.Key.Equals(timeValue)).Value.ToString());
                    density = (double) avgFlow / (double) avgSpeed;
                }

                avgDensityChartData.Add(new KeyValuePair<string, object>(timeValue, density));

            }

            return new Tuple<List<KeyValuePair<string, object>>, List<KeyValuePair<string, object>>>(avgFlowChartData,avgDensityChartData);
        }
    }
}
