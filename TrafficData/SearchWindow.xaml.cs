using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization;
using System.Windows.Data;
using TrafficData.ViewModel;

namespace TrafficData
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SearchWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public SearchWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();

            totalVehiclesChart.LegendStyle = new Style(typeof(Legend));
            totalVehiclesChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            totalVehiclesChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            totalVehiclesChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));

            weightHistogramChart.LegendStyle = new Style(typeof(Legend));
            weightHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            weightHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            weightHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));

            weightPercentileChart.LegendStyle = new Style(typeof(Legend));
            weightPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            weightPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            weightPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));

            weightChart.LegendStyle = new Style(typeof(Legend));
            weightChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 120.0));
            weightChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 100.0));
            weightChart.LegendStyle.Setters.Add(new Setter(Legend.BorderThicknessProperty, new Thickness(0.0)));
            //weightChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));

            averageTraficDensityChart.LegendStyle = new Style(typeof(Legend));
            averageTraficDensityChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            averageTraficDensityChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            averageTraficDensityChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));

            averageTraficFlowChart.LegendStyle = new Style(typeof(Legend));
            averageTraficFlowChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            averageTraficFlowChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            averageTraficFlowChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));

            speedChart.LegendStyle = new Style(typeof(Legend));
            speedChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 120.0));
            speedChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 100.0));
            speedChart.LegendStyle.Setters.Add(new Setter(Legend.BorderThicknessProperty, new Thickness(0.0)));

            speedHistogramChart.LegendStyle = new Style(typeof(Legend));
            speedHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            speedHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            speedHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));
            
            speedPercentileChart.LegendStyle = new Style(typeof(Legend));
            speedPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            speedPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            speedPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));

            // Length
            lengthChart.LegendStyle = new Style(typeof(Legend));
            lengthChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 120.0));
            lengthChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 100.0));
            lengthChart.LegendStyle.Setters.Add(new Setter(Legend.BorderThicknessProperty, new Thickness(0.0)));
            
            lengthHistogramChart.LegendStyle = new Style(typeof(Legend));
            lengthHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            lengthHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            lengthHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));
            
            lengthPercentileChart.LegendStyle = new Style(typeof(Legend));
            lengthPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            lengthPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            lengthPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));

            // Headway
            headwayChart.LegendStyle = new Style(typeof(Legend));
            headwayChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 120.0));
            headwayChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 100.0));
            headwayChart.LegendStyle.Setters.Add(new Setter(Legend.BorderThicknessProperty, new Thickness(0.0)));
            
            headwayHistogramChart.LegendStyle = new Style(typeof(Legend));
            headwayHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            headwayHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            headwayHistogramChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));
            
            headwayPercentileChart.LegendStyle = new Style(typeof(Legend));
            headwayPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.WidthProperty, 0.0));
            headwayPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.HeightProperty, 0.0));
            headwayPercentileChart.LegendStyle.Setters.Add(new Setter(Legend.VisibilityProperty, Visibility.Collapsed));

        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (timeObservedCb.IsChecked ?? false)
            {
                //var observedTime = new ObservedTime();
                //observedTime.Show();
            }
            else
            {
                // TODO add logic
            }
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void TextBlock_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void IntegerUpDown_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !IsNumericAllowed(e.Text);
        }

        private bool IsNumericAllowed(string text)
        {
            Regex regex = new Regex("[0-14]");
            return !regex.IsMatch(text);
        }

        private void CheckBox_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(tabControl.SelectedIndex == 5)
            {
                timeObservedCb.IsEnabled = false;
                timeObservedCb.Visibility = Visibility.Hidden;

                toLabel.Visibility = Visibility.Hidden;
                toTimePicker.Visibility = Visibility.Hidden;
                fromLabel.Visibility = Visibility.Hidden;
                fromTimePicker.Visibility = Visibility.Hidden;
            }
            else
            {
                timeObservedCb.IsEnabled = true;
                timeObservedCb.Visibility = Visibility.Visible;

                toLabel.Visibility = Visibility.Visible;
                toTimePicker.Visibility = Visibility.Visible;
                fromLabel.Visibility = Visibility.Visible;
                fromTimePicker.Visibility = Visibility.Visible;
            }
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

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}