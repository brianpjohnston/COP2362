using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataCollector
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		//Create an instance of the MeasureLengthDevice class
		MeasureLengthDevice mld = new MeasureLengthDevice();

		public MainWindow()
		{
			InitializeComponent();
			DataContext = mld;
		}

		//Expose the radio buttons to use on other classes
		public RadioButton ImperialBtn { get { return imperialRadioBtn; } }
		public RadioButton MetricBtn { get { return metricRadioBtn; } }

        /// <summary>
        /// When form is loaded
        /// 
        /// Certain things should happen here, rather than in the form's constructor.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnFormLoaded(object sender, RoutedEventArgs e)
        {
            // Initialize the radio buttons
            if (mld.Units == UnitsEnumeration.Imperial)
            {
                ImperialBtn.IsChecked = true;
                MetricBtn.IsChecked = false;

                metricMRM.Visibility = System.Windows.Visibility.Hidden;
                metricTimestamp.Visibility = System.Windows.Visibility.Hidden;

                imperialMRM.Visibility = System.Windows.Visibility.Visible;
                imperialTimestamp.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                MetricBtn.IsChecked = true;
                ImperialBtn.IsChecked = false;

                imperialMRM.Visibility = System.Windows.Visibility.Hidden;
                imperialTimestamp.Visibility = System.Windows.Visibility.Hidden;

                metricMRM.Visibility = System.Windows.Visibility.Visible;
                metricTimestamp.Visibility = System.Windows.Visibility.Visible;
            }
        }
        
        
        private void startCollecting_Click(object sender, RoutedEventArgs e)
		{
            
			startCollecting.IsEnabled = false; //Disable start button
			stopCollecting.IsEnabled = true;   //Enable stop button
			mld.StartCollecting();  //Start Collecting



		}

		private void stopCollecting_Click(object sender, RoutedEventArgs e)
		{
            //
			startCollecting.IsEnabled = true;  //Enable start button
			stopCollecting.IsEnabled = false;  //Disable stop button
			mld.StopCollecting();  //Stop Collecting

		}

		private void getRawData_Click(object sender, RoutedEventArgs e)
		{
			decimal[] grd = mld.GetRawData(); //Get raw data from int array
            getRawDataListBox.Items.Clear();    // Clear out the message box

			//Step through array and add items to Listbox
			for (int i = 0; i < grd.Length; i++)
			{
                if (grd[i] != 0)
                {
                    getRawDataListBox.Items.Add(grd[i].ToString());
                }
			}

		}

		//Exit button
		private void exit_Click(object sender, RoutedEventArgs e)
		{
			this.Close();   //Close the form
		}

		private void imperialRadioBtn_Checked(object sender, RoutedEventArgs e)
		{
			MeasureLengthDevice data = (MeasureLengthDevice)DataContext;

			metricMRM.Visibility = System.Windows.Visibility.Hidden;
			metricTimestamp.Visibility = System.Windows.Visibility.Hidden;

			imperialMRM.Visibility = System.Windows.Visibility.Visible;
			imperialTimestamp.Visibility = System.Windows.Visibility.Visible;
		}

		private void metricRadioBtn_Checked(object sender, RoutedEventArgs e)
		{
			MeasureLengthDevice data = (MeasureLengthDevice)DataContext;

			imperialMRM.Visibility = System.Windows.Visibility.Hidden;
			imperialTimestamp.Visibility = System.Windows.Visibility.Hidden;

			metricMRM.Visibility = System.Windows.Visibility.Visible;
			metricTimestamp.Visibility = System.Windows.Visibility.Visible;
		}

        /// <summary>
        /// OnClosed event for MainWindow
        /// </summary>
        /// <param name="sender">[ignore]</param>
        /// <param name="e">[ignore]</param>
        private void OnClosed(object sender, EventArgs e)
        {
           
            Application.Current.Shutdown();
        }

	}
}
