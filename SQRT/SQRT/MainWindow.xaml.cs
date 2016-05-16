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



namespace SQRT
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void calc()
        {
            double origNum = double.Parse(num.Text);
            if (origNum >= 0) //determine if number is whole
            {
                double circleRadi = ((origNum + 1) / 2); //intresecting circle radius
                double square = Math.Cos(Math.Asin((circleRadi - 1) / circleRadi)) * circleRadi; //square root https://en.wikipedia.org/wiki/Square_root_of_3#Geometry_and_trigonometry
                square = Math.Round(square, 14, MidpointRounding.AwayFromZero);
                outputLabel.Content = square.ToString(); //convert to string to populate label
            }
            else //if number is negative
            {
                outputLabel.Content = "Please enter a positive number";
            }

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            try
            {
                calc(); //calls calc method to determine sqrt
            }
         
            catch (FormatException) //format execption
            {
                outputLabel.Content = "Please enter a double"; 
            }

           
            

        
            
        }

        
    }
}

