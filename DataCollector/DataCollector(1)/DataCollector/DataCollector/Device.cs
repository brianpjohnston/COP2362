using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCollector
{
    public class Device
    {
        //Method that generates random int between 1 and 10 as a measurement of some imaginary object and returns 
        public int GetMeasurement()
        {
			Random rnd = new Random(); 
            int measurement = rnd.Next(1, 11);
            return measurement;
        }
    }
}
