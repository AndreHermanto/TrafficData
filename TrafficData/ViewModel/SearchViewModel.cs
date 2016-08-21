using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using TrafficData.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using GalaSoft.MvvmLight.Messaging;
using System.Linq;

namespace TrafficData.ViewModel
{
    //View model for search page
    public class SearchViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        #region Properties 

        /// <summary>
        /// The <see cref="SelectedFromDateTime" /> property's name.
        /// </summary>
        public const string SelectedFromDateTimePropertyName = "SelectedFromDateTime";

        private DateTime _selectedFromDateTime = DateTime.Now;

        /// <summary>
        /// Sets and gets the SelectedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime SelectedFromDateTime
        {
            get
            {
                return _selectedFromDateTime;
            }

            set
            {
                if (_selectedFromDateTime == value)
                {
                    return;
                }

                _selectedFromDateTime = value;
                RaisePropertyChanged(SelectedFromDateTimePropertyName);
                UpdateDateCommand.Execute(SelectedFromDateTimePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedFromDateTime" /> property's name.
        /// </summary>
        public const string SelectedToDateTimePropertyName = "SelectedToDateTime";

        private DateTime _selectedToDateTime = DateTime.Now;

        /// <summary>
        /// Sets and gets the SelectedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime SelectedToDateTime
        {
            get
            {
                return _selectedToDateTime;
            }

            set
            {
                if (_selectedToDateTime == value)
                {
                    return;
                }

                _selectedToDateTime = value;
                RaisePropertyChanged(SelectedToDateTimePropertyName);
                UpdateDateCommand.Execute(SelectedToDateTimePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedStartDate" /> property's name.
        /// </summary>
        public const string SelectedStartDatePropertyName = "SelectedStartDate";

        private DateTime _selectedStartDate = DateTime.Now;

        /// <summary>
        /// Sets and gets the SelectedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime SelectedStartDate
        {
            get
            {
                return _selectedStartDate;
            }

            set
            {
                if (_selectedStartDate == value)
                {
                    return;
                }

                _selectedStartDate = value;
                RaisePropertyChanged(SelectedStartDatePropertyName);
                UpdateDateCommand.Execute(SelectedStartDatePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SelectedEndDate" /> property's name.
        /// </summary>
        public const string SelectedEndDatePropertyName = "SelectedEndDate";

        private DateTime _selectedEndDate = DateTime.Now;

        /// <summary>
        /// Sets and gets the SelectedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime SelectedEndDate
        {
            get
            {
                return _selectedEndDate;
            }

            set
            {
                if (_selectedEndDate == value)
                {
                    return;
                }

                _selectedEndDate = value;
                RaisePropertyChanged(SelectedEndDatePropertyName);
                UpdateDateCommand.Execute(SelectedEndDatePropertyName);

            }
        }

        /// <summary>
        /// The <see cref="SelectedTimeUnit" /> property's name.
        /// </summary>
        public const string SelectedTimeUnitPropertyName = "SelectedTimeUnit";

        private string _selectedTimeUnit = "Day";

        /// <summary>
        /// Sets and gets the SelectedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string SelectedTimeUnit
        {
            get
            {
                return _selectedTimeUnit;
            }

            set
            {
                if (_selectedTimeUnit == value)
                {
                    return;
                }

                _selectedTimeUnit = value;
                RaisePropertyChanged(SelectedTimeUnitPropertyName);
                UpdateDateCommand.Execute(SelectedTimeUnitPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TimeUnits" /> property's name.
        /// </summary>
        public const string TimeUnitsPropertyName = "TimeUnits";

        private List<string> _timeUnits = new List<string>() { "Day", "Month", "Year" };

        /// <summary>
        /// Sets and gets the SelectedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<string> TimeUnits
        {
            get
            {
                return _timeUnits;
            }

            set
            {
                if (_timeUnits == value)
                {
                    return;
                }

                _timeUnits = value;
                RaisePropertyChanged(TimeUnitsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TimeUnits" /> property's name.
        /// </summary>
        public const string IsTimeUnitsEnablePropertyName = "IsTimeUnitsEnable";

        private bool _isTimeUnitsEnable = true;

        /// <summary>
        /// Sets and gets the SelectedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsTimeUnitsEnable
        {
            get
            {
                return _isTimeUnitsEnable;
            }

            set
            {
                if (_isTimeUnitsEnable == value)
                {
                    return;
                }

                _isTimeUnitsEnable = value;
                RaisePropertyChanged(IsTimeUnitsEnablePropertyName);
            }
        }


        /// <summary>
        /// The <see cref="TotalVehicles" /> property's name.
        /// </summary>
        public const string TotalVehiclesPropertyName = "TotalVehicles";

        private int _totalVehicles = 0;

        /// <summary>
        /// Sets and gets the TotalVehicles property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TotalVehicles
        {
            get
            {
                return _totalVehicles;
            }

            set
            {
                if (_totalVehicles == value)
                {
                    return;
                }

                _totalVehicles = value;
                RaisePropertyChanged(TotalVehiclesPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="VehicleChartData" /> property's name.
        /// </summary>
        public const string VehicleChartDataPropertyName = "VehicleChartData";

        private List<KeyValuePair<string, object>> _vehicleChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> VehicleChartData
        {
            get
            {
                return _vehicleChartData;
            }

            set
            {
                if (_vehicleChartData == value)
                {
                    return;
                }

                _vehicleChartData = value;
                RaisePropertyChanged(VehicleChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="TotalGrossWeight" /> property's name.
        /// </summary>
        public const string TotalGrossWeightPropertyName = "TotalGrossWeight";

        private int _totalGrossWeight = 0;

        /// <summary>
        /// Sets and gets the TotalVehicles property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int TotalGrossWeight
        {
            get
            {
                return _totalGrossWeight;
            }

            set
            {
                if (_totalGrossWeight == value)
                {
                    return;
                }

                _totalGrossWeight = value;
                RaisePropertyChanged(TotalGrossWeightPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="GrossWeightChartData" /> property's name.
        /// </summary>
        public const string GrossWeightChartDataPropertyName = "GrossWeightChartData";

        private List<KeyValuePair<string, object>> _grossWeightChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> GrossWeightChartData
        {
            get
            {
                return _grossWeightChartData;
            }

            set
            {
                if (_grossWeightChartData == value)
                {
                    return;
                }

                _grossWeightChartData = value;
                RaisePropertyChanged(GrossWeightChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AverageWeigthChartData" /> property's name.
        /// </summary>
        public const string AverageWeigthChartDataPropertyName = "AverageWeigthChartData";

        private List<KeyValuePair<string, object>> _averageWeigthChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> AverageWeigthChartData
        {
            get
            {
                return _averageWeigthChartData;
            }

            set
            {
                if (_averageWeigthChartData == value)
                {
                    return;
                }

                    _averageWeigthChartData = value;
                RaisePropertyChanged(AverageWeigthChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="HighestWeigthChartData" /> property's name.
        /// </summary>
        public const string HighestWeigthChartDataPropertyName = "HighestWeigthChartData";

        private List<KeyValuePair<string, object>> _highestWeigthChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> HighestWeigthChartData
        {
            get
            {
                return _highestWeigthChartData;
            }

            set
            {
                if (_highestWeigthChartData == value)
                {
                    return;
                }

                _highestWeigthChartData = value;
                RaisePropertyChanged(HighestWeigthChartDataPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="LowestWeigthChartData" /> property's name.
        /// </summary>
        public const string LowestWeigthChartDataPropertyName = "LowestWeigthChartData";

        private List<KeyValuePair<string, object>> _lowestWeigthChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> LowestWeigthChartData
        {
            get
            {
                return _lowestWeigthChartData;
            }

            set
            {
                if (_lowestWeigthChartData == value)
                {
                    return;
                }

                _lowestWeigthChartData = value;
                RaisePropertyChanged(LowestWeigthChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LowestWeigthChartData" /> property's name.
        /// </summary>
        public const string NthTileWeigthChartDataPropertyName = "NthTileWeigthChartData";

        private List<KeyValuePair<string, object>> _nthTileWeigthChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> NthTileWeigthChartData
        {
            get
            {
                return _nthTileWeigthChartData;
            }

            set
            {
                if (_nthTileWeigthChartData == value)
                {
                    return;
                }

                _nthTileWeigthChartData = value;
                RaisePropertyChanged(NthTileWeigthChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="WeightHistogramChartData" /> property's name.
        /// </summary>
        public const string WeightHistogramChartDataPropertyName = "WeightHistogramChartData";

        private List<KeyValuePair<string, object>> _weightHistogramChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> WeightHistogramChartData
        {
            get
            {
                return _weightHistogramChartData;
            }

            set
            {
                if (_weightHistogramChartData == value)
                {
                    return;
                }

                _weightHistogramChartData = value;
                RaisePropertyChanged(WeightHistogramChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="WeightPercentileChartData" /> property's name.
        /// </summary>
        public const string WeightPercentileChartDataPropertyName = "WeightPercentileChartData";

        private List<KeyValuePair<string, object>> _weightPercentileChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> WeightPercentileChartData
        {
            get
            {
                return _weightPercentileChartData;
            }

            set
            {
                if (_weightPercentileChartData == value)
                {
                    return;
                }

                _weightPercentileChartData = value;
                RaisePropertyChanged(WeightPercentileChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AverageSpeed" /> property's name.
        /// </summary>
        public const string AverageSpeedPropertyName = "AverageSpeed";

        private decimal _averageSpeed = 0;

        /// <summary>
        /// Sets and gets the TotalVehicles property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public decimal AverageSpeed
        {
            get
            {
                return _averageSpeed;
            }

            set
            {
                if (_averageSpeed == value)
                {
                    return;
                }

                _averageSpeed = value;
                RaisePropertyChanged(AverageSpeedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AverageSpeedChartData" /> property's name.
        /// </summary>
        public const string AverageSpeedChartDataPropertyName = "AverageSpeedChartData";

        private List<KeyValuePair<string, object>> _averageSpeedChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> AverageSpeedChartData
        {
            get
            {
                return _averageSpeedChartData;
            }

            set
            {
                if (_averageSpeedChartData == value)
                {
                    return;
                }

                _averageSpeedChartData = value;
                RaisePropertyChanged(AverageSpeedChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="HighestSpeedChartData" /> property's name.
        /// </summary>
        public const string HighestSpeedChartDataPropertyName = "HighestSpeedChartData";

        private List<KeyValuePair<string, object>> _highestSpeedChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> HighestSpeedChartData
        {
            get
            {
                return _highestSpeedChartData;
            }

            set
            {
                if (_highestSpeedChartData == value)
                {
                    return;
                }

                _highestSpeedChartData = value;
                RaisePropertyChanged(HighestSpeedChartDataPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="LowestSpeedChartData" /> property's name.
        /// </summary>
        public const string LowestSpeedChartDataPropertyName = "LowestSpeedChartData";

        private List<KeyValuePair<string, object>> _lowestSpeedChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> LowestSpeedChartData
        {
            get
            {
                return _lowestSpeedChartData;
            }

            set
            {
                if (_lowestSpeedChartData == value)
                {
                    return;
                }

                _lowestSpeedChartData = value;
                RaisePropertyChanged(LowestSpeedChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LowestSpeedChartData" /> property's name.
        /// </summary>
        public const string NthTileSpeedChartDataPropertyName = "NthTileSpeedChartData";

        private List<KeyValuePair<string, object>> _nthTileSpeedChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> NthTileSpeedChartData
        {
            get
            {
                return _nthTileSpeedChartData;
            }

            set
            {
                if (_nthTileSpeedChartData == value)
                {
                    return;
                }

                _nthTileSpeedChartData = value;
                RaisePropertyChanged(NthTileSpeedChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SpeedHistogramChartData" /> property's name.
        /// </summary>
        public const string SpeedHistogramChartDataPropertyName = "SpeedHistogramChartData";

        private List<KeyValuePair<string, object>> _SpeedHistogramChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> SpeedHistogramChartData
        {
            get
            {
                return _SpeedHistogramChartData;
            }

            set
            {
                if (_SpeedHistogramChartData == value)
                {
                    return;
                }

                _SpeedHistogramChartData = value;
                RaisePropertyChanged(SpeedHistogramChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="SpeedPercentileChartData" /> property's name.
        /// </summary>
        public const string SpeedPercentileChartDataPropertyName = "SpeedPercentileChartData";

        private List<KeyValuePair<string, object>> _SpeedPercentileChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> SpeedPercentileChartData
        {
            get
            {
                return _SpeedPercentileChartData;
            }

            set
            {
                if (_SpeedPercentileChartData == value)
                {
                    return;
                }

                _SpeedPercentileChartData = value;
                RaisePropertyChanged(SpeedPercentileChartDataPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="MostUsedLane" /> property's name.
        /// </summary>
        public const string MostUsedLanePropertyName = "MostUsedLane";

        private int _mostUsedLane = 0;

        /// <summary>
        /// Sets and gets the TotalVehicles property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MostUsedLane
        {
            get
            {
                return _mostUsedLane;
            }

            set
            {
                if (_mostUsedLane == value)
                {
                    return;
                }

                _mostUsedLane = value;
                RaisePropertyChanged(MostUsedLanePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LeastUsedLane" /> property's name.
        /// </summary>
        public const string LeastUsedLanePropertyName = "LeastUsedLane";

        private int _leastUsedLane = 0;

        /// <summary>
        /// Sets and gets the TotalVehicles property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int LeastUsedLane
        {
            get
            {
                return _leastUsedLane;
            }

            set
            {
                if (_leastUsedLane == value)
                {
                    return;
                }

                _leastUsedLane = value;
                RaisePropertyChanged(LeastUsedLanePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LaneUsedChartData" /> property's name.
        /// </summary>
        public const string LaneUsedChartDataPropertyName = "LaneUsedChartData";

        private List<KeyValuePair<string, object>> _laneUsedChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> LaneUsedChartData
        {
            get
            {
                return _laneUsedChartData;
            }

            set
            {
                if (_laneUsedChartData == value)
                {
                    return;
                }

                _laneUsedChartData = value;
                RaisePropertyChanged(LaneUsedChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="CurrentDate" /> property's name.
        /// </summary>
        public const string CurrentDatePropertyName = "CurrentDate";

        private string _currentDate = DateTime.Now.ToString("dd MMMM yyyy, dddd");

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string CurrentDate
        {
            get
            {
                return _currentDate;
            }

            set
            {
                if (_currentDate == value)
                {
                    return;
                }

                _currentDate = value;
                RaisePropertyChanged(CurrentDatePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MinWeightFilter" /> property's name.
        /// </summary>
        public const string MinWeightFilterPropertyName = "MinWeightFilter";

        private int _minWeightFilter = 0;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MinWeightFilter
        {
            get
            {
                return _minWeightFilter;
            }

            set
            {
                if (_minWeightFilter == value)
                {
                    return;
                }

                _minWeightFilter = value;
                RaisePropertyChanged(MinWeightFilterPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MaxWeightFilter" /> property's name.
        /// </summary>
        public const string MaxWeightFilterPropertyName = "MaxWeightFilter";

        private int _maxWeightFilter = 0;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MaxWeightFilter
        {
            get
            {
                return _maxWeightFilter;
            }

            set
            {
                if (_maxWeightFilter == value)
                {
                    return;
                }

                _maxWeightFilter = value;
                RaisePropertyChanged(MaxWeightFilterPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AxleFilter" /> property's name.
        /// </summary>
        public const string AxleFilterPropertyName = "AxleFilter";

        private int _axleFilter = 0;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int AxleFilter
        {
            get
            {
                return _axleFilter;
            }

            set
            {
                if (_axleFilter == value)
                {
                    return;
                }

                _axleFilter = value;
                RaisePropertyChanged(AxleFilterPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MinLengthFilter" /> property's name.
        /// </summary>
        public const string MinLengthFilterPropertyName = "MinLengthFilter";

        private int _minLengthFilter = 0;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MinLengthFilter
        {
            get
            {
                return _minLengthFilter;
            }

            set
            {
                if (_minLengthFilter == value)
                {
                    return;
                }

                _minLengthFilter = value;
                RaisePropertyChanged(MinLengthFilterPropertyName);
            }
        }
        /// <summary>
        /// The <see cref="MaxLengthFilter" /> property's name.
        /// </summary>
        public const string MaxLengthFilterPropertyName = "MaxLengthFilter";

        private int _maxLengthFilter = 0;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MaxLengthFilter
        {
            get
            {
                return _maxLengthFilter;
            }

            set
            {
                if (_maxLengthFilter == value)
                {
                    return;
                }

                _maxLengthFilter = value;
                RaisePropertyChanged(MaxLengthFilterPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="MinSpeedFilter" /> property's name.
        /// </summary>
        public const string MinSpeedFilterPropertyName = "MinSpeedFilter";

        private int _minSpeedFilter = 0;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MinSpeedFilter
        {
            get
            {
                return _minSpeedFilter;
            }

            set
            {
                if (_minSpeedFilter == value)
                {
                    return;
                }

                _minSpeedFilter = value;
                RaisePropertyChanged(MinSpeedFilterPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="MaxSpeedFilter" /> property's name.
        /// </summary>
        public const string MaxSpeedFilterPropertyName = "MaxSpeedFilter";

        private int _maxSpeedFilter = 0;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int MaxSpeedFilter
        {
            get
            {
                return _maxSpeedFilter;
            }

            set
            {
                if (_maxSpeedFilter == value)
                {
                    return;
                }

                _maxSpeedFilter = value;
                RaisePropertyChanged(MaxSpeedFilterPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Lane1IsChecked" /> property's name.
        /// </summary>
        public const string Lane1IsCheckedPropertyName = "Lane1IsChecked";

        private bool _lane1IsChecked = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Lane1IsChecked
        {
            get
            {
                return _lane1IsChecked;
            }

            set
            {
                if (_lane1IsChecked == value)
                {
                    return;
                }

                _lane1IsChecked = value;
                RaisePropertyChanged(Lane1IsCheckedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Lane2IsChecked" /> property's name.
        /// </summary>
        public const string Lane2IsCheckedPropertyName = "Lane2IsChecked";

        private bool _lane2IsChecked = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Lane2IsChecked
        {
            get
            {
                return _lane2IsChecked;
            }

            set
            {
                if (_lane2IsChecked == value)
                {
                    return;
                }

                _lane2IsChecked = value;
                RaisePropertyChanged(Lane2IsCheckedPropertyName);

            }
        }

        /// <summary>
        /// The <see cref="Lane3IsChecked" /> property's name.
        /// </summary>
        public const string Lane3IsCheckedPropertyName = "Lane3IsChecked";

        private bool _lane3IsChecked = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Lane3IsChecked
        {
            get
            {
                return _lane3IsChecked;
            }

            set
            {
                if (_lane3IsChecked == value)
                {
                    return;
                }

                _lane3IsChecked = value;
                RaisePropertyChanged(Lane3IsCheckedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Lane4IsChecked" /> property's name.
        /// </summary>
        public const string Lane4IsCheckedPropertyName = "Lane4IsChecked";

        private bool _lane4IsChecked = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Lane4IsChecked
        {
            get
            {
                return _lane4IsChecked;
            }

            set
            {
                if (_lane4IsChecked == value)
                {
                    return;
                }

                _lane4IsChecked = value;
                RaisePropertyChanged(Lane4IsCheckedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Lane5IsChecked" /> property's name.
        /// </summary>
        public const string Lane5IsCheckedPropertyName = "Lane5IsChecked";

        private bool _lane5IsChecked = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Lane5IsChecked
        {
            get
            {
                return _lane5IsChecked;
            }

            set
            {
                if (_lane5IsChecked == value)
                {
                    return;
                }

                _lane5IsChecked = value;
                RaisePropertyChanged(Lane5IsCheckedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="Lane6IsChecked" /> property's name.
        /// </summary>
        public const string Lane6IsCheckedPropertyName = "Lane6IsChecked";

        private bool _lane6IsChecked = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool Lane6IsChecked
        {
            get
            {
                return _lane6IsChecked;
            }

            set
            {
                if (_lane6IsChecked == value)
                {
                    return;
                }

                _lane6IsChecked = value;
                RaisePropertyChanged(Lane6IsCheckedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsWeightFilterActive" /> property's name.
        /// </summary>
        public const string IsWeightFilterActivePropertyName = "IsWeightFilterActive";

        private bool _isWeightFilterActive = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsWeightFilterActive
        {
            get
            {
                return _isWeightFilterActive;
            }

            set
            {
                if (_isWeightFilterActive == value)
                {
                    return;
                }

                _isWeightFilterActive = value;
                RaisePropertyChanged(IsWeightFilterActivePropertyName);
                UpdateDateCommand.Execute(IsWeightFilterActivePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsWeightFilterActive" /> property's name.
        /// </summary>
        public const string IsSpeedFilterActivePropertyName = "IsSpeedFilterActive";

        private bool _isSpeedFilterActive = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsSpeedFilterActive
        {
            get
            {
                return _isSpeedFilterActive;
            }

            set
            {
                if (_isSpeedFilterActive == value)
                {
                    return;
                }

                _isSpeedFilterActive = value;
                RaisePropertyChanged(IsSpeedFilterActivePropertyName);
                UpdateDateCommand.Execute(IsSpeedFilterActivePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsLengthClassChecked" /> property's name.
        /// </summary>
        public const string IsLengthClassCheckedPropertyName = "IsLengthClassChecked";

        private bool _isLengthClassChecked = true;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLengthClassChecked
        {
            get
            {
                return _isLengthClassChecked;
            }

            set
            {
                if (_isLengthClassChecked == value)
                {
                    return;
                }

                _isLengthClassChecked = value;
                RaisePropertyChanged(IsLengthClassCheckedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsLengthValueChecked" /> property's name.
        /// </summary>
        public const string IsLengthValueCheckedPropertyName = "IsLengthValueChecked";

        private bool _isLengthValueChecked = true;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLengthValueChecked
        {
            get
            {
                return _isLengthValueChecked;
            }

            set
            {
                if (_isLengthValueChecked == value)
                {
                    return;
                }

                _isLengthValueChecked = value;
                RaisePropertyChanged(IsLengthValueCheckedPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="IsAxleFilterActive" /> property's name.
        /// </summary>
        public const string IsTimeObservedCheckedPropertyName = "IsTimeObservedChecked";

        private bool _isTimeObservedChecked = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsTimeObservedChecked
        {
            get
            {
                return _isTimeObservedChecked;
            }

            set
            {
                if (_isTimeObservedChecked == value)
                {
                    return;
                }

                _isTimeObservedChecked = value;
                RaisePropertyChanged(IsTimeObservedCheckedPropertyName);
                UpdateDateCommand.Execute(IsTimeObservedCheckedPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="IsAxleFilterActive" /> property's name.
        /// </summary>
        public const string IsAxleFilterActivePropertyName = "IsAxleFilterActive";

        private bool _isAxleFilterActive = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsAxleFilterActive
        {
            get
            {
                return _isAxleFilterActive;
            }

            set
            {
                if (_isAxleFilterActive == value)
                {
                    return;
                }

                _isAxleFilterActive = value;
                RaisePropertyChanged(IsAxleFilterActivePropertyName);
                UpdateDateCommand.Execute(IsAxleFilterActivePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsLaneFilterActive" /> property's name.
        /// </summary>
        public const string IsLaneFilterActivePropertyName = "IsLaneFilterActive";

        private bool _isLaneFilterActive = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLaneFilterActive
        {
            get
            {
                return _isLaneFilterActive;
            }

            set
            {
                if (_isLaneFilterActive == value)
                {
                    return;
                }

                _isLaneFilterActive = value;
                RaisePropertyChanged(IsLaneFilterActivePropertyName);
                UpdateDateCommand.Execute(IsLaneFilterActivePropertyName);
            }
        }
        /// <summary>
        /// The <see cref="IsLengthFilterActive" /> property's name.
        /// </summary>
        public const string IsLengthFilterActivePropertyName = "IsLengthFilterActive";

        private bool _isLengthFilterActive = false;

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLengthFilterActive
        {
            get
            {
                return _isLengthFilterActive;
            }

            set
            {
                if (_isLengthFilterActive == value)
                {
                    return;
                }

                _isLengthFilterActive = value;
                RaisePropertyChanged(IsLengthFilterActivePropertyName);
                UpdateDateCommand.Execute(IsLengthFilterActivePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="ComparisonItems" /> property's name.
        /// </summary>
        public const string ComparisonItemsPropertyName = "ComparisonItems";

        private List<ComparisonItem> _comparisonItems = new List<ComparisonItem>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<ComparisonItem> ComparisonItems
        {
            get
            {
                return _comparisonItems;
            }

            set
            {
                if (_comparisonItems == value)
                {
                    return;
                }

                _comparisonItems = value;
                RaisePropertyChanged(ComparisonItemsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="WeightComparisonItems" /> property's name.
        /// </summary>
        public const string WeightComparisonItemsPropertyName = "WeightComparisonItems";

        private List<ComparisonItem> _weightComparisonItems = new List<ComparisonItem>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<ComparisonItem> WeightComparisonItems
        {
            get
            {
                return _weightComparisonItems;
            }

            set
            {
                if (_weightComparisonItems == value)
                {
                    return;
                }

                _weightComparisonItems = value;
                RaisePropertyChanged(WeightComparisonItemsPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="WeightPercentile" /> property's name.
        /// </summary>
        public const string WeightPercentilePropertyName = "WeightPercentile";

        private int _weightPercentile = 85;

        /// <summary>
        /// Sets and gets the WeightPercentile property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int WeightPercentile
        {
            get
            {
                return _weightPercentile;
            }

            set
            {
                if (_weightPercentile == value)
                {
                    return;
                }

                _weightPercentile = value;
                RaisePropertyChanged(WeightPercentilePropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsAverageDisplayChecked" /> property's name.
        /// </summary>
        public const string IsAverageDisplayCheckedPropertyName = "IsAverageDisplayChecked";

        private bool _IsAverageDisplayChecked = false;

        /// <summary>
        /// Sets and gets the IsAverageDisplayChecked property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsAverageDisplayChecked
        {
            get
            {
                return _IsAverageDisplayChecked;
            }

            set
            {
                if (_IsAverageDisplayChecked == value)
                {
                    return;
                }

                _IsAverageDisplayChecked = value;
                RaisePropertyChanged(IsAverageDisplayCheckedPropertyName);
                UpdateDateCommand.Execute(IsAverageDisplayCheckedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsHighestDisplayChecked" /> property's name.
        /// </summary>
        public const string IsHighestDisplayCheckedPropertyName = "IsHighestDisplayChecked";

        private bool _IsHighestDisplayChecked = false;

        /// <summary>
        /// Sets and gets the IsHighestDisplayChecked property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsHighestDisplayChecked
        {
            get
            {
                return _IsHighestDisplayChecked;
            }

            set
            {
                if (_IsHighestDisplayChecked == value)
                {
                    return;
                }

                _IsHighestDisplayChecked = value;
                RaisePropertyChanged(IsHighestDisplayCheckedPropertyName);
                UpdateDateCommand.Execute(IsHighestDisplayCheckedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsLowestDisplayChecked" /> property's name.
        /// </summary>
        public const string IsLowestDisplayCheckedPropertyName = "IsLowestDisplayChecked";

        private bool _IsLowestDisplayChecked = false;

        /// <summary>
        /// Sets and gets the IsLowestDisplayChecked property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsLowestDisplayChecked
        {
            get
            {
                return _IsLowestDisplayChecked;
            }

            set
            {
                if (_IsLowestDisplayChecked == value)
                {
                    return;
                }

                _IsLowestDisplayChecked = value;
                RaisePropertyChanged(IsLowestDisplayCheckedPropertyName);
                UpdateDateCommand.Execute(IsLowestDisplayCheckedPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="IsNthTileChecked" /> property's name.
        /// </summary>
        public const string IsNthTileCheckedPropertyName = "IsNthTileChecked";

        private bool _isNthTileChecked = false;

        /// <summary>
        /// Sets and gets the IsNthTileChecked property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public bool IsNthTileChecked
        {   
            get
            {
                return _isNthTileChecked;
            }

            set
            {
                if (_isNthTileChecked == value)
                {
                    return;
                }

                _isNthTileChecked = value;
                RaisePropertyChanged(IsNthTileCheckedPropertyName);
                UpdateDateCommand.Execute(IsNthTileCheckedPropertyName);
            }
        }

        ///// <summary>
        ///// The <see cref="IsWeightLaneFilterChecked" /> property's name.
        ///// </summary>
        //public const string IsWeightLaneFilterCheckedPropertyName = "IsWeightLaneFilterChecked";

        //private bool _isWeightLaneFilterChecked = false;

        ///// <summary>
        ///// Sets and gets the IsWeightLaneFilterChecked property.
        ///// Changes to that property's value raise the PropertyChanged event. 
        ///// </summary>
        //public bool IsWeightLaneFilterChecked
        //{
        //    get
        //    {
        //        return _isWeightLaneFilterChecked;
        //    }

        //    set
        //    {
        //        if (_isWeightLaneFilterChecked == value)
        //        {
        //            return;
        //        }

        //        _isWeightLaneFilterChecked = value;
        //        RaisePropertyChanged(IsWeightLaneFilterCheckedPropertyName);
        //        UpdateDateCommand.Execute(IsWeightLaneFilterCheckedPropertyName);
        //    }
        //}

        /// <summary>
        /// The <see cref="SelectedTab" /> property's name.
        /// </summary>
        public const string SelectedTabPropertyName = "SelectedTab";

        private int _selectedTab = 0;

        /// <summary>
        /// Sets and gets the SelectedTab property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public int SelectedTab
        {
            get
            {
                return _selectedTab;
            }

            set
            {
                if (_selectedTab == value)
                {
                    return;
                }

                _selectedTab = value;
                RaisePropertyChanged(SelectedTabPropertyName);
                ComparisonItems = new List<ComparisonItem>();
                IsTimeUnitsEnable = true;
                UpdateDateCommand.Execute(SelectedTabPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AverageTrafficFlowChartData" /> property's name.
        /// </summary>
        public const string AverageTrafficFlowChartDataPropertyName = "AverageTrafficFlowChartData";

        private List<KeyValuePair<string,object>> _averageTrafficFlowChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the SelectedTab property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> AverageTrafficFlowChartData
        {
            get
            {
                return _averageTrafficFlowChartData;
            }

            set
            {
                if (_averageTrafficFlowChartData == value)
                {
                    return;
                }

                _averageTrafficFlowChartData = value;
                RaisePropertyChanged(AverageTrafficFlowChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AverageTrafficDensityChartData" /> property's name.
        /// </summary>
        public const string AverageTrafficDensityChartDataPropertyName = "AverageTrafficDensityChartData";

        private List<KeyValuePair<string, object>> _averageTrafficDensityChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the SelectedTab property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> AverageTrafficDensityChartData
        {
            get
            {
                return _averageTrafficDensityChartData;
            }

            set
            {
                if (_averageTrafficDensityChartData == value)
                {
                    return; 
                }

                _averageTrafficDensityChartData = value;
                RaisePropertyChanged(AverageTrafficDensityChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AverageLengthChartData" /> property's name.
        /// </summary>
        public const string AverageLengthChartDataPropertyName = "AverageLengthChartData";

        private List<KeyValuePair<string, object>> _averageLengthChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> AverageLengthChartData
        {
            get
            {
                return _averageLengthChartData;
            }

            set
            {
                if (_averageLengthChartData == value)
                {
                    return;
                }

                _averageLengthChartData = value;
                RaisePropertyChanged(AverageLengthChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="HighestLengthChartData" /> property's name.
        /// </summary>
        public const string HighestLengthChartDataPropertyName = "HighestLengthChartData";

        private List<KeyValuePair<string, object>> _highestLengthChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> HighestLengthChartData
        {
            get
            {
                return _highestLengthChartData;
            }

            set
            {
                if (_highestLengthChartData == value)
                {
                    return;
                }

                _highestLengthChartData = value;
                RaisePropertyChanged(HighestLengthChartDataPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="LowestLengthChartData" /> property's name.
        /// </summary>
        public const string LowestLengthChartDataPropertyName = "LowestLengthChartData";

        private List<KeyValuePair<string, object>> _lowestLengthChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> LowestLengthChartData
        {
            get
            {
                return _lowestLengthChartData;
            }

            set
            {
                if (_lowestLengthChartData == value)
                {
                    return;
                }

                _lowestLengthChartData = value;
                RaisePropertyChanged(LowestLengthChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LowestLengthChartData" /> property's name.
        /// </summary>
        public const string NthTileLengthChartDataPropertyName = "NthTileLengthChartData";

        private List<KeyValuePair<string, object>> _nthTileLengthChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> NthTileLengthChartData
        {
            get
            {
                return _nthTileLengthChartData;
            }

            set
            {
                if (_nthTileLengthChartData == value)
                {
                    return;
                }

                _nthTileLengthChartData = value;
                RaisePropertyChanged(NthTileLengthChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LengthHistogramChartData" /> property's name.
        /// </summary>
        public const string LengthHistogramChartDataPropertyName = "LengthHistogramChartData";

        private List<KeyValuePair<string, object>> _LengthHistogramChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> LengthHistogramChartData
        {
            get
            {
                return _LengthHistogramChartData;
            }

            set
            {
                if (_LengthHistogramChartData == value)
                {
                    return;
                }

                _LengthHistogramChartData = value;
                RaisePropertyChanged(LengthHistogramChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LengthPercentileChartData" /> property's name.
        /// </summary>
        public const string LengthPercentileChartDataPropertyName = "LengthPercentileChartData";

        private List<KeyValuePair<string, object>> _LengthPercentileChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> LengthPercentileChartData
        {
            get
            {
                return _LengthPercentileChartData;
            }

            set
            {
                if (_LengthPercentileChartData == value)
                {
                    return;
                }

                _LengthPercentileChartData = value;
                RaisePropertyChanged(LengthPercentileChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="AverageHeadwayChartData" /> property's name.
        /// </summary>
        public const string AverageHeadwayChartDataPropertyName = "AverageHeadwayChartData";

        private List<KeyValuePair<string, object>> _averageHeadwayChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> AverageHeadwayChartData
        {
            get
            {
                return _averageHeadwayChartData;
            }

            set
            {
                if (_averageHeadwayChartData == value)
                {
                    return;
                }

                _averageHeadwayChartData = value;
                RaisePropertyChanged(AverageHeadwayChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="HighestHeadwayChartData" /> property's name.
        /// </summary>
        public const string HighestHeadwayChartDataPropertyName = "HighestHeadwayChartData";

        private List<KeyValuePair<string, object>> _highestHeadwayChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> HighestHeadwayChartData
        {
            get
            {
                return _highestHeadwayChartData;
            }

            set
            {
                if (_highestHeadwayChartData == value)
                {
                    return;
                }

                _highestHeadwayChartData = value;
                RaisePropertyChanged(HighestHeadwayChartDataPropertyName);
            }
        }


        /// <summary>
        /// The <see cref="LowestHeadwayChartData" /> property's name.
        /// </summary>
        public const string LowestHeadwayChartDataPropertyName = "LowestHeadwayChartData";

        private List<KeyValuePair<string, object>> _lowestHeadwayChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> LowestHeadwayChartData
        {
            get
            {
                return _lowestHeadwayChartData;
            }

            set
            {
                if (_lowestHeadwayChartData == value)
                {
                    return;
                }

                _lowestHeadwayChartData = value;
                RaisePropertyChanged(LowestHeadwayChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="LowestHeadwayChartData" /> property's name.
        /// </summary>
        public const string NthTileHeadwayChartDataPropertyName = "NthTileHeadwayChartData";

        private List<KeyValuePair<string, object>> _nthTileHeadwayChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> NthTileHeadwayChartData
        {
            get
            {
                return _nthTileHeadwayChartData;
            }

            set
            {
                if (_nthTileHeadwayChartData == value)
                {
                    return;
                }

                _nthTileHeadwayChartData = value;
                RaisePropertyChanged(NthTileHeadwayChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="HeadwayHistogramChartData" /> property's name.
        /// </summary>
        public const string HeadwayHistogramChartDataPropertyName = "HeadwayHistogramChartData";

        private List<KeyValuePair<string, object>> _HeadwayHistogramChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> HeadwayHistogramChartData
        {
            get
            {
                return _HeadwayHistogramChartData;
            }

            set
            {
                if (_HeadwayHistogramChartData == value)
                {
                    return;
                }

                _HeadwayHistogramChartData = value;
                RaisePropertyChanged(HeadwayHistogramChartDataPropertyName);
            }
        }

        /// <summary>
        /// The <see cref="HeadwayPercentileChartData" /> property's name.
        /// </summary>
        public const string HeadwayPercentileChartDataPropertyName = "HeadwayPercentileChartData";

        private List<KeyValuePair<string, object>> _HeadwayPercentileChartData = new List<KeyValuePair<string, object>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, object>> HeadwayPercentileChartData
        {
            get
            {
                return _HeadwayPercentileChartData;
            }

            set
            {
                if (_HeadwayPercentileChartData == value)
                {
                    return;
                }

                _HeadwayPercentileChartData = value;
                RaisePropertyChanged(HeadwayPercentileChartDataPropertyName);
            }
        }

        #endregion

        private void UpdateViewModel(SearchPageData searchPageData)
        {
            // Vehilce page
            VehicleChartData = searchPageData.VehicleChartData ?? new List<KeyValuePair<string,object>>();
            LaneUsedChartData = searchPageData.UsedLaneChartData ?? new List<KeyValuePair<string,object>>();

            // Weight page
            AverageWeigthChartData = searchPageData.AverageWeigthChartData ?? new List<KeyValuePair<string,object>>();
            LowestWeigthChartData = searchPageData.LowestWeigthChartData ?? new List<KeyValuePair<string,object>>();
            HighestWeigthChartData = searchPageData.HighestWeigthChartData ?? new List<KeyValuePair<string,object>>();
            NthTileWeigthChartData = searchPageData.NthTileWeigthChartData ?? new List<KeyValuePair<string,object>>();
            WeightHistogramChartData = searchPageData.WeightHistogramChartData ?? new List<KeyValuePair<string,object>>();
            WeightPercentileChartData = searchPageData.WeightPercentileChartData ?? new List<KeyValuePair<string,object>>();
    
            // Calculated data page
            AverageTrafficFlowChartData = searchPageData.AverageTrafficFlowChartData ?? new List<KeyValuePair<string,object>>();
            AverageTrafficDensityChartData = searchPageData.AverageTrafficDensityChartData ?? new List<KeyValuePair<string,object>>();

            // Speed page
            AverageSpeedChartData = searchPageData.AverageSpeedChartData ?? new List<KeyValuePair<string,object>>();
            LowestSpeedChartData = searchPageData.LowestSpeedChartData ?? new List<KeyValuePair<string,object>>();
            HighestSpeedChartData = searchPageData.HighestSpeedChartData ?? new List<KeyValuePair<string,object>>();
            NthTileSpeedChartData = searchPageData.NthTileSpeedChartData ?? new List<KeyValuePair<string,object>>();
            SpeedHistogramChartData = searchPageData.SpeedHistogramChartData ?? new List<KeyValuePair<string,object>>();
            SpeedPercentileChartData = searchPageData.SpeedPercentileChartData ?? new List<KeyValuePair<string,object>>();

            // Length page
            AverageLengthChartData = searchPageData.AverageLengthChartData ?? new List<KeyValuePair<string,object>>();
            LowestLengthChartData = searchPageData.LowestLengthChartData ?? new List<KeyValuePair<string,object>>();
            HighestLengthChartData = searchPageData.HighestLengthChartData ?? new List<KeyValuePair<string,object>>();
            NthTileLengthChartData = searchPageData.NthTileLengthChartData ?? new List<KeyValuePair<string,object>>();
            LengthHistogramChartData = searchPageData.LengthHistogramChartData ?? new List<KeyValuePair<string,object>>();
            LengthPercentileChartData = searchPageData.LengthPercentileChartData ?? new List<KeyValuePair<string,object>>();

            // Head page
            AverageHeadwayChartData = searchPageData.AverageHeadwayChartData ?? new List<KeyValuePair<string,object>>();
            LowestHeadwayChartData = searchPageData.LowestHeadwayChartData ?? new List<KeyValuePair<string,object>>();
            HighestHeadwayChartData = searchPageData.HighestHeadwayChartData ?? new List<KeyValuePair<string,object>>();
            NthTileHeadwayChartData = searchPageData.NthTileHeadwayChartData ?? new List<KeyValuePair<string,object>>();
            HeadwayHistogramChartData = searchPageData.HeadwayHistogramChartData ?? new List<KeyValuePair<string,object>>();
            HeadwayPercentileChartData = searchPageData.HeadwayPercentileChartData ?? new List<KeyValuePair<string,object>>();



        }

        private RelayCommand<string> _updateDateCommand;
        public RelayCommand<string> UpdateDateCommand
        {
            get
            {
                return _updateDateCommand
                  ?? (_updateDateCommand = new RelayCommand<string>(
                    (param) =>
                    {
                        var notToUpdateHistogramCommands = new List<string>() { IsAverageDisplayCheckedPropertyName, IsHighestDisplayCheckedPropertyName, IsLowestDisplayCheckedPropertyName, IsNthTileCheckedPropertyName };
                        var isToUpdateHistogram = true; //!notToUpdateHistogramCommands.Contains(param);

                        List<QueryInfo> queryInfos = new List<QueryInfo>();
                        if(ComparisonItems.Count == 0 || ComparisonItems.TrueForAll(ci => !ci.IsChecked))
                        {
                            QueryInfo queryInfo = GetQueryInfo(1);
                            queryInfos.Add(queryInfo);
                        }
                        else
                        {
                            QueryInfo queryInfo = ComparisonItems.FindLast(ci => ci.IsChecked).QueryInfo;
                            queryInfo.Filters = CreateFilter();
                            queryInfo.ActiveInfos = CreateDisplayInfos();

                            queryInfos = ComparisonItems.Where(c => c.IsChecked).Select(ci => ci.QueryInfo).ToList();
                        }

                        SearchPageData updatedModel = null;

                        switch(_selectedTab)
                        {
                            case 0: // Totals
                                updatedModel = _dataService.GetSearchPageData(queryInfos);
                                break;
                            case 1: // Weight data
                                
                                updatedModel = _dataService.GetSearchPageDataForWeight(queryInfos);
                                break;
                            case 2: // Speed data

                                updatedModel = _dataService.GetSearchPageDataForSpeed(queryInfos);
                                break;
                            case 3: // Length data

                                updatedModel = _dataService.GetSearchPageDataForLenght(queryInfos);
                                break;
                            case 4: // Headway data

                                updatedModel = _dataService.GetSearchPageDataForHeadway(queryInfos);
                                break;

                            case 5: // Calculated data
                                updatedModel = _dataService.GetSearchPageDataForCalculatedData(queryInfos.FirstOrDefault());
                                break;

                            default:
                                updatedModel = new SearchPageData();
                                break;
                        }
                        UpdateViewModel(updatedModel);
                    }));
            }
        }

        private QueryInfo GetQueryInfo(int index)
        {
            var queryInfo = new QueryInfo();
            queryInfo.StartTime = _selectedStartDate;
            queryInfo.EndTime = _selectedEndDate;
            queryInfo.TimeUnit = _selectedTimeUnit;
            queryInfo.Filters = CreateFilter();
            queryInfo.ActiveInfos = CreateDisplayInfos();
            queryInfo.Index = index;
            return queryInfo;
        }

        private RelayCommand _addComparisonCommand;
        public RelayCommand AddComparisonCommand
        {
            get
            {
                return _addComparisonCommand
                  ?? (_addComparisonCommand = new RelayCommand(
                    () =>
                    {
                        var index = 1;
                        var newComparisonItems = new List<ComparisonItem>();
                        foreach (var item in ComparisonItems)
                        {
                            item.Index = index;
                            newComparisonItems.Add(item);
                            index++;
                        }
                        var newComaprisonItem = new ComparisonItem()
                        {
                            Index = index,
                            Text = "(" + index + ") " + _selectedStartDate.ToShortDateString() + " " + _selectedEndDate.ToShortDateString(),
                            StartDate = _selectedStartDate,
                            EndDate = _selectedEndDate,
                            QueryInfo = GetQueryInfo(index),
                        };
                        newComparisonItems.Add(newComaprisonItem);

                        ComparisonItems = newComparisonItems;

                        IsTimeUnitsEnable = false;
                    }));
            }
        }

        private RelayCommand _clearComparisonCommand;
        public RelayCommand ClearComparisonCommand
        {
            get
            {
                return _clearComparisonCommand
                  ?? (_clearComparisonCommand = new RelayCommand(
                    () =>
                    {
                        IsTimeUnitsEnable = true;
                        ComparisonItems = new List<ComparisonItem>();
                        var updatedModel = GetUpdatedModel();
                        UpdateViewModel(updatedModel);
                    }));
            }
        }


        private Dictionary<string, List<KeyValuePair<bool, object>>> CreateFilter()
        {
            var filters = new Dictionary<string, List<KeyValuePair<bool, object>>>();

            if (IsWeightFilterActive)
            {
                filters.Add("Weight",
                    new List<KeyValuePair<bool, object>>() {
                    new KeyValuePair<bool, object>(IsWeightFilterActive, MinWeightFilter),
                    new KeyValuePair<bool, object>(IsWeightFilterActive, MaxWeightFilter),
                    });
            }

            if (IsAxleFilterActive)
            {

                filters.Add("Axle",
                    new List<KeyValuePair<bool, object>>() {
                    new KeyValuePair<bool, object>(IsAxleFilterActive, AxleFilter),
                    });
            }

            if (IsLengthFilterActive)
            {
                filters.Add("Length",
                    new List<KeyValuePair<bool, object>>() {
                    new KeyValuePair<bool, object>(IsLengthClassChecked, MinLengthFilter),
                    new KeyValuePair<bool, object>(IsLengthClassChecked, MaxLengthFilter),
                    });
            }

            if (IsSpeedFilterActive)
            {
                filters.Add("Speed",
                    new List<KeyValuePair<bool, object>>() {
                    new KeyValuePair<bool, object>(IsSpeedFilterActive, MinSpeedFilter),
                    new KeyValuePair<bool, object>(IsSpeedFilterActive, MaxSpeedFilter),
                    });
            }

            if (IsLaneFilterActive)
            {
                filters.Add("Lane",
                    new List<KeyValuePair<bool, object>>() {
                    new KeyValuePair<bool, object>(IsLaneFilterActive, Lane1IsChecked),
                    new KeyValuePair<bool, object>(IsLaneFilterActive, Lane2IsChecked),
                    new KeyValuePair<bool, object>(IsLaneFilterActive, Lane3IsChecked),
                    new KeyValuePair<bool, object>(IsLaneFilterActive, Lane4IsChecked),
                    new KeyValuePair<bool, object>(IsLaneFilterActive, Lane5IsChecked),
                    new KeyValuePair<bool, object>(IsLaneFilterActive, Lane6IsChecked),
                    });
            }

            if (IsTimeObservedChecked)
            {
                filters.Add("Time",
                   new List<KeyValuePair<bool, object>>() {
                    new KeyValuePair<bool, object>(IsTimeObservedChecked, SelectedFromDateTime),
                    new KeyValuePair<bool, object>(IsTimeObservedChecked, SelectedToDateTime),
                   });
            }

            filters.Add("Percentile",
                  new List<KeyValuePair<bool, object>>() {
                    new KeyValuePair<bool, object>(true, WeightPercentile),
                  });

            return filters;
        }

        public Dictionary<string, bool> CreateDisplayInfos()
        {
            var infos = new Dictionary<string, bool>();

            infos.Add("Average", IsAverageDisplayChecked);
            infos.Add("Highest", IsHighestDisplayChecked);
            infos.Add("Lowest", IsLowestDisplayChecked);
            infos.Add("NthTile", IsNthTileChecked);

            infos.Add("Histogram", true);
            infos.Add("Percentile", true);

            return infos;
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public SearchViewModel(IDataService dataService)
        {
            _dataService = dataService;

            //MessengerInstance.Register<string>(this, payload => SomeAction(payload));

            Messenger.Default.Register<NotificationMessage<ComparisonItem>>(
            this, nm =>

            {
                SearchPageData updatedModel = GetUpdatedModel();
                UpdateViewModel(updatedModel);

            });
        }

        private SearchPageData GetUpdatedModel()
        {
            var queryInfos = new List<QueryInfo>();
            foreach (var comparisonItem in ComparisonItems)
            {
                if (comparisonItem.IsChecked)
                {
                    queryInfos.Add(comparisonItem.QueryInfo);
                }
            }

            SearchPageData updatedModel;
            switch (_selectedTab)
            {
                case 0: // Totals
                    updatedModel = _dataService.GetSearchPageData(queryInfos);
                    break;
                case 1: // Weight data
                    updatedModel = _dataService.GetSearchPageDataForWeight(queryInfos);
                    break;
                case 2: // Speed data
                    updatedModel = _dataService.GetSearchPageDataForSpeed(queryInfos);
                    break;
                case 3: // Lenght data
                    updatedModel = _dataService.GetSearchPageDataForLenght(queryInfos);
                    break;
                case 4: // Headway data
                    updatedModel = _dataService.GetSearchPageDataForHeadway(queryInfos);
                    break;
                case 5: // Calculated data
                    updatedModel = _dataService.GetSearchPageDataForCalculatedData(queryInfos.FirstOrDefault());
                    break;

                default:
                    updatedModel = new SearchPageData();
                    break;
            }

            return updatedModel;
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }

    public class ComparisonItem : ViewModelBase
    {
        /// <summary>
        /// The <see cref="Text" /> property's name.
        /// </summary>
        public const string TextPropertyName = "Text";

        private string _text = "Here";

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }

            set
            {
                if (_text == value)
                {
                    return;
                }

                _text = value;
                RaisePropertyChanged(TextPropertyName);
            }
        }

        public DateTime StartDate
        {
            get; set;
        }
        public DateTime EndDate
        {
            get; set;
        }

        public QueryInfo QueryInfo { get; set; }

        /// <summary>
        /// The <see cref="IsChecked" /> property's name.
        /// </summary>
        public const string IsCheckedName = "IsChecked";

        private bool _isChecked = false;

        public bool IsChecked
        {
            get
            {
                return _isChecked;
            }

            set
            {
                if (_isChecked == value)
                {
                    return;
                }

                _isChecked = value;
                RaisePropertyChanged(IsCheckedName);
                Messenger.Default.Send<NotificationMessage<ComparisonItem>>(
                new NotificationMessage<ComparisonItem>(this, this, "Add"));
            }
        }

        public int Index { get; internal set; }
    }
}