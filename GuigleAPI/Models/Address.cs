using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuigleAPI.Model
{
    public class AddressResponse
    {
        [JsonProperty("results")]
        public List<Address> Results { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
    }

    public class Address
    {
        [JsonProperty("address_components")]
        public List<AddressComponent> AddressComponents { get; set; }
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
        /// <summary>
        /// From enum AddressType
        /// </summary>
        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }

    public class AddressComponent
    {
        [JsonProperty("long_name")]
        public string LongName { get; set; }
        [JsonProperty("short_name")]
        public string ShortName { get; set; }
        /// <summary>
        /// From enum AddressType
        /// </summary>
        [JsonProperty("types")]
        public List<string> Types { get; set; }
    }

    public class Geometry
    {
        [JsonProperty("location")]
        public Location Location { get; set; }
        [JsonProperty("location_type")]
        public string LocationType { get; set; }
        [JsonProperty("viewport")]
        public ViewPort ViewPort { get; set; }
    }

    public class Location
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }
        [JsonProperty("lng")]
        public double Lng { get; set; }
    }

    public class ViewPort
    {
        [JsonProperty("northeast")]
        public Location Northeast { get; set; }
        [JsonProperty("southwest")]
        public Location Southwest { get; set; }
    }

    public enum AddressType
    {
        street_number,
        street_address,
        route,
        intersection,
        neighborhood,
        political,
        locality,
        ward,
        sublocality,
        sublocality_level_1,
        sublocality_level_2,
        sublocality_level_3,
        sublocality_level_4,
        sublocality_level_5,
        premise,
        subpremise,
        natural_feature,
        airport,
        park,
        point_of_interest,
        administrative_area_level_1,
        administrative_area_level_2,
        administrative_area_level_3,
        administrative_area_level_4,
        administrative_area_level_5,
        colloquial_area,
        country,
        postal_code,
        floor,
        establishment,
        parking,
        post_box,
        postal_town,
        room,
        bus_station,
        train_station,
        transit_station
    }
}
