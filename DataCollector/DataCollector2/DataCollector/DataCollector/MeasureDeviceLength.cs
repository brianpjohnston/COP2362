using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;   // Add a reference to Windows for the hack in 
using System.Threading.Tasks;
using System.Windows.Threading;

namespace DataCollector
{
	public class MeasureLengthDevice : IMeasuringDevice, INotifyPropertyChanged
	{
		//Declare fields
		DispatcherTimer timer;
		private UnitsEnumeration unitsToUse;
		private decimal[] dataCaptured = new decimal[] { 0, 0, 0, 0, 0 };
		private int mostRecentMeasure;
		public int MostRecentMeasure
		{
			get { return mostRecentMeasure; }
			set
			{
				mostRecentMeasure = value;
				MostRecentImperial = ImperialValue(mostRecentMeasure);
				MostRecentMetric = MetricValue(mostRecentMeasure);
			}
		}

		private Device device;

        /// <summary>
        /// Units property -- get only
        /// </summary>
        public UnitsEnumeration Units
        {
            get { return unitsToUse; }
        }

		private DateTime currentDate;
		public DateTime CurrentDate
		{
			get { return currentDate; }
			set
			{
				currentDate = value;
				OnPropertyChanged("CurrentDate");
			}
		}

		private decimal mostRecentImperial;
		public decimal MostRecentImperial
		{
			get { return mostRecentImperial; }
			set
			{
				mostRecentImperial = value;
				OnPropertyChanged("MostRecentImperial");
			}
		}

		private decimal mostRecentMetric;
		public decimal MostRecentMetric
		{
			get { return mostRecentMetric; }
			set
			{
				mostRecentMetric = value;
				OnPropertyChanged("MostRecentMetric");
			}
		}

		//Default Constructor to initialize fields
		public MeasureLengthDevice()
		{
			timer = new DispatcherTimer();
            timer.Tick += timer_Tick;        
			unitsToUse = UnitsEnumeration.Imperial;
			device = new Device();
			MostRecentMeasure = device.GetMeasurement();
		}

		public decimal MetricValue(int mostRecentMeasure)
		{
			decimal mrm = Convert.ToDecimal(mostRecentMeasure);
			const decimal conversion = 2.54M;
			//Convert mostRecentMeasure to centimeters
			mrm *= conversion;
			return mrm;
		}

		public decimal ImperialValue(int mostRecentMeasure)
		{
			decimal mrm = Convert.ToDecimal(mostRecentMeasure);
			const decimal conversion = 0.39370M;
			//Convert mostRecentMeasure to inches
			mrm *= conversion;
			return mrm;
		}

		public void StartCollecting()
		{
            
			timer.Interval = new System.TimeSpan(0, 0, 15); //tick every 15 seconds

            timer.Start();  //Start timer
		}

		//Add mostRecentMesaure to the dataCaptured int array
		private void timer_Tick(object sender, EventArgs e)
		{
			
            MainWindow main = (MainWindow) Application.Current.MainWindow;  // Get the MainWindow object

            MostRecentMeasure = device.GetMeasurement();  //Run device to get measurement
            
            if (main.imperialRadioBtn.IsChecked == true)  //Check user input
			{
				unitsToUse = UnitsEnumeration.Imperial; //Set  unitsToUse                
				//Add data to array
				for (int i = 0; i < dataCaptured.Length; i++)
				{
                    
                    CurrentDate = DateTime.Now;
                    decimal recentCapture = ImperialValue(MostRecentMeasure);  //Convert

                    if (dataCaptured[i] == 0)
                    {
                        dataCaptured[i] = recentCapture;
                        break;
                    }
				}
			}
			else
			{
				unitsToUse = UnitsEnumeration.Metric;  //Set unitsToUse
				//Add data to array
				for (int i = 0; i < dataCaptured.Length; i++)
				{
                   
                    CurrentDate = DateTime.Now;
                    decimal recentCapture = MetricValue(MostRecentMeasure); //Convert
                    
                    if (dataCaptured[i] == 0)
					{
						dataCaptured[i] = recentCapture;
                        break;
					}
				}
			}
		}

		//Stop Timer
		public void StopCollecting()
		{
			timer.Stop();
		}

		//Return dataCaptured int array
		public decimal[] GetRawData()
		{
			return dataCaptured;
		}

		public event PropertyChangedEventHandler PropertyChanged;

		private void OnPropertyChanged(string propertyName)
		{
			PropertyChangedEventHandler handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
