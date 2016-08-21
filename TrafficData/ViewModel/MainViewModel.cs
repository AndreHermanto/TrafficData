using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using TrafficData.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TrafficData.ViewModel
{
    //View Model for dashboard page
    public class MainViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        #region Properties 
        /// <summary>
        /// The <see cref="SelectedDate" /> property's name.
        /// </summary>
        public const string SelectedDatePropertyName = "SelectedDate";

        private DateTime _selectedDate = DateTime.Now;

        /// <summary>
        /// Sets and gets the SelectedDate property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public DateTime SelectedDate
        {
            get
            {
                return _selectedDate;
            }

            set
            {
                if (_selectedDate == value)
                {
                    return;
                }

                _selectedDate = value;
                RaisePropertyChanged(SelectedDatePropertyName);
                UpdateDateCommand.Execute(new KeyValuePair<DateTime, string>(_selectedDate, _selectedTimeUnit));
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
                UpdateDateCommand.Execute(new KeyValuePair<DateTime,string>(_selectedDate, _selectedTimeUnit));
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
        /// The <see cref="TotalVehicles" /> property's name.
        /// </summary>
        public const string TotalVehiclesPropertyName = "TotalVehicles";

        private long _totalVehicles = 0;

        /// <summary>
        /// Sets and gets the TotalVehicles property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public long TotalVehicles
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

        private List<KeyValuePair<string, long>> _vehicleChartData = new List<KeyValuePair<string, long>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, long>> VehicleChartData
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

        private long _totalGrossWeight = 0;

        /// <summary>
        /// Sets and gets the TotalVehicles property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public long TotalGrossWeight
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

        private List<KeyValuePair<string, long>> _grossWeightChartData = new List<KeyValuePair<string, long>>();

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public List<KeyValuePair<string, long>> GrossWeightChartData
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

        #endregion

        private void UpdateViewModel(MainPageData mainPageData)
        {
            TotalVehicles = mainPageData.TotalVehicles;
            VehicleChartData = mainPageData.VehicleChartData.ConvertAll(entry => new KeyValuePair<string, long>(entry.Key, Convert.ToInt64(entry.Value)));

            TotalGrossWeight = (long) mainPageData.TotalGrossWeight;
            GrossWeightChartData = mainPageData.GrossWeightChartData.ConvertAll(entry => new KeyValuePair<string, long>(entry.Key, Convert.ToInt64(entry.Value)));

            AverageSpeed = mainPageData.AverageSpeed;
            AverageSpeedChartData = mainPageData.AverageSpeedChartData;

            MostUsedLane = mainPageData.MostUsedLane;
            LeastUsedLane = mainPageData.LeastUsedLane;
            LaneUsedChartData = mainPageData.UsedLaneChartData;
        }

        private RelayCommand<KeyValuePair<DateTime,string>> _updateDateCommand;
        public RelayCommand<KeyValuePair<DateTime, string>> UpdateDateCommand
        {
            get
            {
                return _updateDateCommand
                  ?? (_updateDateCommand = new RelayCommand<KeyValuePair<DateTime, string>>(
                    (dateTimeInfo) =>
                    {
                        var updatedModel =_dataService.GetMainPageData(dateTimeInfo);
                        UpdateViewModel(updatedModel);
                    }));
            }
        }



        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            _dataService = dataService;
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}