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

            GoogleGeoCodeAPI.GoogleAPIKey = GooglePlacesAPI.GoogleAPIKey = TesterRes.GoogleAPIKey; // create your own TesterRes resource and put your google api there


            //GEOCODE

            var result1 = Task.Run(async () => await GoogleGeoCodeAPI.SearchAddressAsync("100 Market St, Southbank")).Result;

            var result2 = Task.Run(async () => await GoogleGeoCodeAPI.GetCoordinatesFromAddressAsync("100 Market St, Southbank")).Result;


            //PLACES

            var firstResult = result1.Results.FirstOrDefault();
            
            var result3 = Task.Run(async () => await GooglePlacesAPI.SearchPlaceNearBy(lat: firstResult.Geometry.Location.Lat, lng: firstResult.Geometry.Location.Lng, radiusInMeters: 500, type: GuigleAPI.Model.PlaceType.liquor_store)).Result;

            var result4 = Task.Run(async () => await GooglePlacesAPI.SearchPlaceNearBy(query: "100 Market St, Southbank")).Result;
        }
    }
}