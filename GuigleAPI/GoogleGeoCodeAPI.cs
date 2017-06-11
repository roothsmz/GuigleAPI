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
    public static class GoogleGeoCodeAPI
    {
        public static readonly string GeoCodeUrl = "https://maps.googleapis.com/maps/api/geocode/";
        public static readonly string GeoPlacesUrl = "https://maps.googleapis.com/maps/api/place/";
        public static string GoogleAPIKey { get; set; }

        public static async Task<AddressResponse> GetAddress(double lat, double lng)
        {
            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = 256000;
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
        }

        public static async Task<AddressResponse> GetAddress(HttpClient client, double lat, double lng)
        {
            client.MaxResponseContentBufferSize = 256000;
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
    }
}