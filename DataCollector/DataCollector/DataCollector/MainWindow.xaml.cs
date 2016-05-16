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

		private void startCollecting_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Started collecting");  //Message box to alter user
			startCollecting.IsEnabled = false; //Disable start button
			stopCollecting.IsEnabled = true;   //Enable stop button
			mld.StartCollecting();  //Start Collecting



		}

		private void stopCollecting_Click(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Stopped collecting");  //Message box to alert user
			startCollecting.IsEnabled = true;  //Enable start button
			stopCollecting.IsEnabled = false;  //Disable stop button
			mld.StopCollecting();  //Stop Collecting

		}

		private void getRawData_Click(object sender, RoutedEventArgs e)
		{
			decimal[] grd = mld.GetRawData(); //Get raw data from int array
			//Step through array and add items to Listbox
			for (int i = 0; i < grd.Length; i++)
			{
				getRawDataListBox.Items.Add(grd[i].ToString());
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
	}
}
