using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using TrafficData.Model;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TrafficData.ViewModel
{
    
    public class AboutUsViewModel : ViewModelBase
    {
        private readonly IDataService _dataService;

        #region Properties 
        

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
        /// The <see cref="AboutUsText" /> property's name.
        /// </summary>
        public const string AboutUsTextPropertyName = "AboutUsText";

        private string _aboutUsText = "BBLA BLA BLa bla bla bla BBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla blaBBLA BLA BLa bla bla bla";

        /// <summary>
        /// Sets and gets the VehicleChartData property.
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        public string AboutUsText
        {
            get
            {
                return _aboutUsText;
            }

            set
            {
                if (_aboutUsText == value)
                {
                    return;
                }

                _aboutUsText = value;
                RaisePropertyChanged(AboutUsTextPropertyName);
            }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public AboutUsViewModel(IDataService dataService)
        {
            
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}