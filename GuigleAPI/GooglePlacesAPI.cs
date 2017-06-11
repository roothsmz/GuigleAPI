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

        
    }
}