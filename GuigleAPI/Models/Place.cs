using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuigleAPI.Model
{
    public class PlaceResponse
    {
        [JsonProperty("results")]
        public List<Address> Results { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("html_attributions ")]
        public string HtmlAttributions { get; set; }
        [JsonProperty("next_page_token  ")]
        public string NextPageToken { get; set; }
    }
}