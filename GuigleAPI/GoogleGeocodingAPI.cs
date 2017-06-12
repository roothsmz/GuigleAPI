using GuigleAPI.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GuigleAPI
{
    public static class GoogleGeocodingAPI
    {
        public static string GeoCodeUrl { get; set; } = "https://maps.googleapis.com/maps/api/geocode/";
        public static int MaxResponseContentBufferSize { get; set; } = 256000;
        public static string GoogleAPIKey { get; set; }

        /// <summary>
        /// Gets all addresses returned from Google GeoCode API based on the coordinates provided.
        /// </summary>
        /// <param name="client">The HttpClient object. Make sure it's not passed closed.</param>
        /// <param name="lat">The latitude to search on Google API.</param>
        /// <param name="lng">The longitude to search on Google API.</param>
        /// <returns>Returns all results from Google API as an AddressResponse.</returns>
        public static async Task<AddressResponse> GetAddressFromCoordinatesAsync(HttpClient client, double lat, double lng)
        {
            client.MaxResponseContentBufferSize = MaxResponseContentBufferSize;
            var uri = new Uri(string.Format($"{GeoCodeUrl}json?latlng={lat},{lng}&key={GoogleAPIKey}", string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AddressResponse>(content);
            }
            else
            {
                throw new HttpRequestException($"Request status code {response.StatusCode}. More details: {await response.Content?.ReadAsStringAsync()}");
            }
        }

        /// <summary>
        /// Gets all addresses returned from Google GeoCode API based on the coordinates provided.
        /// </summary>
        /// <param name="lat">The latitude to search on Google API.</param>
        /// <param name="lng">The longitude to search on Google API.</param>
        /// <returns>Returns all results from Google API as an AddressResponse.</returns>
        public static async Task<AddressResponse> GetAddressFromCoordinatesAsync(double lat, double lng)
        {
            using (var client = new HttpClient())
            {
                return await GetAddressFromCoordinatesAsync(client, lat, lng);
            }
        }

        /// <summary>
        /// Gets the city, state and country from a list of address componentes.
        /// </summary>
        /// <param name="client">The HttpClient object. Make sure it's not passed closed.</param>
        /// <param name="lat">The latitude to search on Google API.</param>
        /// <param name="lng">The longitude to search on Google API.</param>
        /// <returns>Returns a Tuple<string, string, string> where item1 is the city short name, item2 is the state short name and item3 is the country long name. Returns a Tuple containing nulls if nothing is returned from the API.</returns>
        public static async Task<Tuple<string, string, string>> GetCityFromCoordinatesAsync(HttpClient client, double lat, double lng)
        {
            client.MaxResponseContentBufferSize = MaxResponseContentBufferSize;
            var uri = new Uri(string.Format($"{GeoCodeUrl}json?latlng={lat},{lng}&key={GoogleAPIKey}", string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var contentResult = JsonConvert.DeserializeObject<AddressResponse>(content);

                var addressComponentes = contentResult.Results.SelectMany(t => t.AddressComponents);

                var city = addressComponentes.FirstOrDefault(r => r.Types.Contains(AddressType.administrative_area_level_2.ToString()))?.ShortName ?? addressComponentes.FirstOrDefault(r => r.Types.Contains(AddressType.administrative_area_level_3.ToString()))?.ShortName ?? addressComponentes.FirstOrDefault(r => r.Types.Contains(AddressType.locality.ToString()))?.ShortName;
                var state = addressComponentes.FirstOrDefault(r => r.Types.Contains(AddressType.administrative_area_level_1.ToString()))?.ShortName;
                var country = addressComponentes.FirstOrDefault(r => r.Types.Contains(AddressType.country.ToString()))?.LongName;

                return new Tuple<string, string, string>(city, state, country);
            }
            else
            {
                throw new HttpRequestException($"Request status code {response.StatusCode}. More details: {await response.Content?.ReadAsStringAsync()}");
            }
        }

        /// <summary>
        /// Gets the city, state and country from a list of address componentes.
        /// </summary>
        /// <param name="lat">The latitude to search on Google API.</param>
        /// <param name="lng">The longitude to search on Google API.</param>
        /// <returns>Returns a Tuple<string, string, string> where item1 is the city short name, item2 is the state short name and item3 is the country long name. Returns a Tuple containing nulls if nothing is returned from the API.</returns>
        public static async Task<Tuple<string, string, string>> GetCityFromCoordinatesAsync(double lat, double lng)
        {
            using (var client = new HttpClient())
            {
                return await GetCityFromCoordinatesAsync(client, lat, lng);
            }
        }

        /// <summary>
        /// Search for an address and returns all results from Google API.
        /// </summary>
        /// <param name="client">The HttpClient object. Make sure it's not passed closed.</param>
        /// <param name="address">The address to search on Google API.</param>
        /// <returns>Returns all results from Google API as an AddressResponse.</returns>
        public static async Task<AddressResponse> SearchAddressAsync(HttpClient client, string address)
        {
            client.MaxResponseContentBufferSize = MaxResponseContentBufferSize;
            var uri = new Uri(string.Format($"{GeoCodeUrl}json?address={address.Replace(" ", "+")}&key={GoogleAPIKey}", string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AddressResponse>(content);
            }
            else
            {
                throw new HttpRequestException($"Request status code {response.StatusCode}. More details: {await response.Content?.ReadAsStringAsync()}");
            }
        }

        /// <summary>
        /// Search for an address and returns all results from Google API.
        /// </summary>
        /// <param name="address">The address to search on Google API.</param>
        /// <returns>Returns all results from Google API as an AddressResponse.</returns>
        public static async Task<AddressResponse> SearchAddressAsync(string address)
        {
            using (var client = new HttpClient())
            {
                return await SearchAddressAsync(client, address);
            }
        }

        /// <summary>
        /// Gets the first coordinates from a possible list of address componentes.
        /// </summary>
        /// <param name="client">The HttpClient object. Make sure it's not passed closed.</param>
        /// <param name="address">The address to search on Google API.</param>
        /// <returns>Returns a Tuple<double, double> where item1 is latitude and item2 is longitude. Returns null if nothing is returned from the API.</returns>
        public static async Task<Tuple<double, double>> GetCoordinatesFromAddressAsync(HttpClient client, string address)
        {
            var result = await SearchAddressAsync(client, address);
            var firstResult = result.Results.FirstOrDefault();
            if (firstResult != null)
                return new Tuple<double, double>(firstResult.Geometry?.Location?.Lat ?? 0, firstResult.Geometry?.Location?.Lng ?? 0);
            else
                return null;
        }

        /// <summary>
        /// Gets the first coordinates from a possible list of address componentes.
        /// </summary>
        /// <param name="address">The address to search on Google API.</param>
        /// <returns>Returns a Tuple<double, double> where item1 is latitude and item2 is longitude. Returns null if nothing is returned from the API.</returns>
        public static async Task<Tuple<double, double>> GetCoordinatesFromAddressAsync(string address)
        {
            using (var client = new HttpClient())
            {
                return await GetCoordinatesFromAddressAsync(client, address);
            }
        }

        /// <summary>
        /// Search for an address preferring results within the viewport provided and returns all results from Google API.
        /// </summary>
        /// <param name="client">The HttpClient object. Make sure it's not passed closed.</param>
        /// <param name="address">The address to search on Google API.</param>
        /// <param name="southwest">The south west coordinates of the bounding box.</param>
        /// <param name="northeast">The north east coordinates of the bounding box.</param>
        /// <returns>Returns all results from Google API as an AddressResponse.</returns>
        public static async Task<AddressResponse> SearchAddressAsync(HttpClient client, string address, Tuple<double, double> southwest, Tuple<double, double> northeast)
        {
            client.MaxResponseContentBufferSize = MaxResponseContentBufferSize;
            var uri = new Uri(string.Format($"{GeoCodeUrl}json?address={address.Replace(" ", "+")}&bounds={southwest.Item1},{southwest.Item2}|{northeast.Item1},{northeast.Item2}&key={GoogleAPIKey}", string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<AddressResponse>(content);
            }
            else
            {
                throw new HttpRequestException($"Request status code {response.StatusCode}. More details: {await response.Content?.ReadAsStringAsync()}");
            }
        }

        /// <summary>
        /// Search for an address preferring results within the viewport provided and returns all results from Google API.
        /// </summary>
        /// <param name="address">The address to search on Google API.</param>
        /// <param name="southwest">The south west coordinates of the bounding box.</param>
        /// <param name="northeast">The north east coordinates of the bounding box.</param>
        /// <returns>Returns all results from Google API as an AddressResponse.</returns>
        public static async Task<AddressResponse> SearchAddressAsync(string address, Tuple<double, double> southwest, Tuple<double, double> northeast)
        {
            using (var client = new HttpClient())
            {
                return await SearchAddressAsync(client, address, southwest, northeast);
            }
        }
    }
}