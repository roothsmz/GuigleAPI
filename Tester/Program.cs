﻿using GuigleAPI;
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
            var location1 = result1.Results.FirstOrDefault().Geometry.Location;

            var location2 = Task.Run(async () => await GoogleGeoCodeAPI.GetCoordinatesFromAddressAsync("322 Kings Way, South Melbourne VIC 3205")).Result;

            var midlePoint = Helper.GetMiddleCoordinates(location1.Lat, location2.Item1, location1.Lng, location2.Item2);            

            //PLACES
            
            var result4 = Task.Run(async () => await GooglePlacesAPI.SearchPlaceNearBy(lat: midlePoint.Item1, lng: midlePoint.Item2, radiusInMeters: 1000, type: GuigleAPI.Model.PlaceType.liquor_store)).Result;

            var result5 = Task.Run(async () => await GooglePlacesAPI.SearchPlaceNearBy(query: "100 Market St, Southbank")).Result;
        }
    }
}