using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using TrafficData.ViewModel;

namespace TrafficData
{
    /// <summary>
    /// Interaction logic for AboutUsWindow.xaml
    /// </summary>
    public partial class AboutUsWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public AboutUsWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();

        

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

        /// <summary>
        /// TO be used if dynamic map needed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_Loaded(object sender, RoutedEventArgs e)
        {
            string location = "Fradelos+Famalicao";

            BitmapImage b = new BitmapImage();
            b.BeginInit();
            b.UriSource = new Uri("http://maps.googleapis.com/maps/api/staticmap?" +
                    "size=500x400&markers=size:mid%7Ccolor:red%7C" +
                    location + "&zoom=15"  + "&maptype=roadmap" + "&sensor=false");
            b.EndInit();

            // ... Get Image reference from sender.
            var image = sender as Image;
            // ... Assign Source.
            image.Source = b;
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}