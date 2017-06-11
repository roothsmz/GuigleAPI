using GuigleAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Press anything to start calling API");
            var ok = Console.ReadKey();

            GoogleGeoCodeAPI.GoogleAPIKey = TesterRes.GoogleAPIKey; // create your own TesterRes resource and put your own google api there

            var result1 = Task.Run(async () => await GoogleGeoCodeAPI.SearchAddressAsync("100 Market St, Southbank")).Result;

            var result2 = Task.Run(async () => await GoogleGeoCodeAPI.GetCoordinatesFromAddressAsync("100 Market St, Southbank")).Result;
        }
    }
}