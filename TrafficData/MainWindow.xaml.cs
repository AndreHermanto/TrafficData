using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TrafficData.ViewModel;
using System.Windows.Media;

namespace TrafficData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();

            totalVehiclesChart.LegendStyle = new Style(typeof(Legend));
            totalVehiclesChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            totalVehiclesChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            totalVehiclesChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));


            //totalGrossWeightChart.LegendStyle = new Style(typeof(Legend));
            //totalGrossWeightChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            //totalGrossWeightChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            //totalGrossWeightChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));

            averageSpeedChart.LegendStyle = new Style(typeof(Legend));
            averageSpeedChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            averageSpeedChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            averageSpeedChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));

            //usedLaneChart.LegendStyle = new Style(typeof(Legend));
            //usedLaneChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            //usedLaneChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            //usedLaneChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            //usedLaneChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Visible));
            


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var searchWindow = new SearchWindow();
            searchWindow.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            var aboutUS = new AboutUsWindow();
            aboutUS.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var contactWindow = new ContactWindow();
            contactWindow.Show();
            this.Close();
        }
    }
}