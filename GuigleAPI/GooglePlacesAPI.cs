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
    public static class GooglePlacesAPI
    {
        public static string GeoPlacesUrl { get; set; } = "https://maps.googleapis.com/maps/api/place/";
        public static int MaxResponseContentBufferSize { get; set; } = 256000;
        public static string GoogleAPIKey { get; set; }

        /// <summary>
        /// Gets up to 20 places returned from Google Places API based on the coordinates provided.
        /// </summary>
        /// <param name="client">The HttpClient object. Make sure it's not passed closed.</param>
        /// <param name="lat">The latitude to search on Google API.</param>
        /// <param name="lng">The longitude to search on Google API.</param>
        /// <param name="radiusInMeters">The maximum distance to search. Narrow this value down to get fewer and more accurate results.</param>
        /// <param name="language">See https://developers.google.com/maps/faq?authuser=1#languagesupport.</param>
        /// <param name="type">The type of the place. E.g. restaurant.</param>
        /// <param name="keyWord">Any key word to search for. E.g. cruise.</param>
        /// <param name="rankBy">Rank by distance or prominence.</param>
        /// <param name="moreOptionalParameters">There are more optional parameters that can be added to the search request. Check Google Developers API for more info.</param>
        /// <param name="moreOptionalParameters">Make sure you provide the full key/value pair starting with "&". E.g. "&minprice=20".</param>
        /// <returns></returns>
        public static async Task<PlaceResponse> SearchPlaceNearBy(HttpClient client, double lat, double lng, int radiusInMeters = 50000, string language = null, PlaceType? type = null, string keyWord = null, RankBy? rankBy = null, string moreOptionalParameters = null)
        {
            client.MaxResponseContentBufferSize = MaxResponseContentBufferSize;

            var languageStr = string.IsNullOrEmpty(language) ? string.Empty : "&language=" + language;
            var typeStr = type.HasValue ? "&type=" + type.ToString() : string.Empty;
            var keyWordStr = string.IsNullOrEmpty(keyWord) ? string.Empty : "&keyword=" + keyWord.Replace(" ", "+");
            var rankByStr = rankBy.HasValue ? "&rankby=" + rankBy.ToString() : string.Empty;
            var moreOptionalParametersStr = string.IsNullOrEmpty(moreOptionalParameters) ? string.Empty : moreOptionalParameters;

            var uri = new Uri(string.Format($"{GeoPlacesUrl}nearbysearch/json?location={lat},{lng}&radius={radiusInMeters}{languageStr}{typeStr}{keyWordStr}{moreOptionalParametersStr}&key={GoogleAPIKey}", string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PlaceResponse>(content);
            }
            else
            {
                throw new HttpRequestException($"Request status code {response.StatusCode}. More details: {await response.Content?.ReadAsStringAsync()}");
            }
        }

        /// <summary>
        /// Gets up to 20 places returned from Google Places API based on the coordinates provided.
        /// </summary>
        /// <param name="lat">The latitude to search on Google API.</param>
        /// <param name="lng">The longitude to search on Google API.</param>
        /// <param name="radiusInMeters">The maximum distance to search. Narrow this value down to get fewer and more accurate results.</param>
        /// <param name="language">See https://developers.google.com/maps/faq?authuser=1#languagesupport.</param>
        /// <param name="type">The type of the place. E.g. restaurant.</param>
        /// <param name="keyWord">Any key word to search for. E.g. cruise.</param>
        /// <param name="rankBy">Rank by distance or prominence.</param>
        /// <param name="moreOptionalParameters">There are more optional parameters that can be added to the search request. Check Google Developers API for more info.</param>
        /// <param name="moreOptionalParameters">Make sure you provide the full key/value pair starting with "&". E.g. "&minprice=20".</param>
        /// <returns></returns>
        public static async Task<PlaceResponse> SearchPlaceNearBy(double lat, double lng, int radiusInMeters = 50000, string language = null, PlaceType? type = null, string keyWord = null, RankBy? rankBy = null, string moreOptionalParameters = null)
        {
            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = MaxResponseContentBufferSize;
                return await SearchPlaceNearBy(client, lat, lng, radiusInMeters, language, type, keyWord, rankBy, moreOptionalParameters);
            }
        }

        /// <summary>
        /// Gets up to 20 places returned from Google Places API based on the query provided.
        /// </summary>
        /// <param name="client">The HttpClient object. Make sure it's not passed closed.</param>
        /// <param name="query">The query to be used to search on Google API.</param>
        /// <param name="lat">The latitude to search on Google API.</param>
        /// <param name="lng">The longitude to search on Google API.</param>
        /// <param name="radiusInMeters">The maximum distance to search. Narrow this value down to get fewer and more accurate results.</param>
        /// <param name="language">See https://developers.google.com/maps/faq?authuser=1#languagesupport.</param>
        /// <param name="type"></param>
        /// <param name="moreOptionalParameters">There are more optional parameters that can be added to the search request. Check Google Developers API for more info.</param>
        /// <param name="moreOptionalParameters">Make sure you provide the full key/value pair starting with "&". E.g. "&minprice=20".</param>
        /// <returns></returns>
        public static async Task<PlaceResponse> SearchPlaceNearBy(HttpClient client, string query, double? lat = null, double? lng = null, int? radiusInMeters = null, string language = null, PlaceType? type = null, string moreOptionalParameters = null)
        {
            var locationStr = (lat.HasValue && lng.HasValue) ? $"&location={lat},{lng}" : string.Empty;
            var radiusInMetersStr = radiusInMeters.HasValue ? "&radius=" + radiusInMeters.ToString() : string.Empty;
            var languageStr = string.IsNullOrEmpty(language) ? string.Empty : "&language=" + language;
            var typeStr = type.HasValue ? "&type=" + type.ToString() : string.Empty;
            var moreOptionalParametersStr = string.IsNullOrEmpty(moreOptionalParameters) ? string.Empty : moreOptionalParameters;

            var uri = new Uri(string.Format($"{GeoPlacesUrl}textsearch/json?query={query.Replace(" ", "+")}{locationStr}{radiusInMetersStr}{languageStr}{typeStr}{moreOptionalParametersStr}&key={GoogleAPIKey}", string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PlaceResponse>(content);
            }
            else
            {
                throw new HttpRequestException($"Request status code {response.StatusCode}. More details: {await response.Content?.ReadAsStringAsync()}");
            }
        }

        /// <summary>
        /// Gets up to 20 places returned from Google Places API based on the query provided.
        /// </summary>
        /// <param name="client">The HttpClient object. Make sure it's not passed closed.</param>
        /// <param name="query">The query to be used to search on Google API.</param>
        /// <param name="lat">The latitude to search on Google API.</param>
        /// <param name="lng">The longitude to search on Google API.</param>
        /// <param name="radiusInMeters">The maximum distance to search. Narrow this value down to get fewer and more accurate results.</param>
        /// <param name="language">See https://developers.google.com/maps/faq?authuser=1#languagesupport.</param>
        /// <param name="type"></param>
        /// <param name="moreOptionalParameters">There are more optional parameters that can be added to the search request. Check Google Developers API for more info.</param>
        /// <param name="moreOptionalParameters">Make sure you provide the full key/value pair starting with "&". E.g. "&minprice=20".</param>
        /// <returns></returns>
        public static async Task<PlaceResponse> SearchPlaceNearBy(string query, double? lat = null, double? lng = null, int? radiusInMeters = null, string language = null, PlaceType? type = null, string moreOptionalParameters = null)
        {
            using (var client = new HttpClient())
            {
                return await SearchPlaceNearBy(client, query, lat, lng, radiusInMeters, language, type, moreOptionalParameters);
            }
        }

        /// <summary>
        /// Gets next page (or next 20 places) returned from Google Places API based on the token provided.
        /// </summary>
        /// <param name="client">The HttpClient object. Make sure it's not passed closed.</param>
        /// <param name="pageToken">The token returned on a previous search. This token will be used to retrieve the next page (20 results).</param>
        /// <returns></returns>
        public static async Task<PlaceResponse> SearchPlaceNearBy(HttpClient client, string pageToken)
        {
            client.MaxResponseContentBufferSize = MaxResponseContentBufferSize;
            var uri = new Uri(string.Format($"{GeoPlacesUrl}nearbysearch/json?pagetoken={pageToken}&key={GoogleAPIKey}", string.Empty));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<PlaceResponse>(content);
            }
            else
            {
                throw new HttpRequestException($"Request status code {response.StatusCode}. More details: {await response.Content?.ReadAsStringAsync()}");
            }
        }

        /// <summary>
        /// Gets next page (or next 20 places) returned from Google Places API based on the token provided.
        /// </summary>
        /// <param name="pageToken">The token returned on a previous search. This token will be used to retrieve the next page (20 results).</param>
        /// <returns></returns>
        public static async Task<PlaceResponse> SearchPlaceNearBy(string pageToken)
        {
            using (var client = new HttpClient())
            {
                client.MaxResponseContentBufferSize = MaxResponseContentBufferSize;
                return await SearchPlaceNearBy(client, pageToken);
            }
        }
    }
}