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
        public List<Place> Results { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("html_attributions")]
        public List<string> HtmlAttributions { get; set; }
        [JsonProperty("next_page_token")]
        public string NextPageToken { get; set; }
    }

    public class Place
    {
        [JsonProperty("icon")]
        public string Icon { get; set; }
        [JsonProperty("geometry")]
        public Geometry Geometry { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("opening_hours")]
        public OpeningHours OpeningHours { get; set; }
        [JsonProperty("photos")]
        public List<Photo> Photos { get; set; }
        [JsonProperty("place_id")]
        public string PlaceId { get; set; }
        [JsonProperty("scope")]
        public string Scope { get; set; }
        /// <summary>
        /// key is place_id and value is scope
        /// </summary>
        [JsonProperty("alt_ids")]
        public Dictionary<string, string> AltIds { get; set; }
        /// <summary>
        /// Scale is from 0 to 4
        /// </summary>
        [JsonProperty("price_level")]
        public int PriceLevel { get; set; }
        /// <summary>
        /// From 1.0 to 5.0
        /// </summary>
        [JsonProperty("rating")]
        public double Rating { get; set; }
        /// <summary>
        /// Token used to request Place Details
        /// </summary>
        [JsonProperty("reference")]
        public string Reference { get; set; }
        /// <summary>
        /// From enum PlaceType OR AddressType (can be either)
        /// </summary>
        [JsonProperty("types")]
        public List<string> Types { get; set; }
        [JsonProperty("vicinity")]
        public string Vicinity { get; set; }
        [JsonProperty("formatted_address")]
        public string FormattedAddress { get; set; }
        [JsonProperty("permanently_closed")]
        public bool PermanentlyClosed { get; set; }
    }

    public class OpeningHours
    {
        [JsonProperty("open_now")]
        public bool OpenNow { get; set; }
    }

    public class Photo
    {
        [JsonProperty("photo_reference")]
        public string PhotoReference { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("html_attributions")]
        public List<string> HtmlAttributions { get; set; }
    }

    public enum RankBy
    {
        Distance,
        Prominence
    }

    public enum PlaceType
    {
        accounting,
        airport,
        amusement_park,
        aquarium,
        art_gallery,
        atm,
        bakery,
        bank,
        bar,
        beauty_salon,
        bicycle_store,
        book_store,
        bowling_alley,
        bus_station,
        cafe,
        campground,
        car_dealer,
        car_rental,
        car_repair,
        car_wash,
        casino,
        cemetery,
        church,
        city_hall,
        clothing_store,
        convenience_store,
        courthouse,
        dentist,
        department_store,
        doctor,
        electrician,
        electronics_store,
        embassy,
        fire_station,
        florist,
        funeral_home,
        furniture_store,
        gas_station,
        general_contractor,
        grocery_or_supermarket,
        gym,
        hair_care,
        hardware_store,
        hindu_temple,
        home_goods_store,
        hospital,
        insurance_agency,
        jewelry_store,
        laundry,
        lawyer,
        library,
        liquor_store,
        local_government_office,
        locksmith,
        lodging,
        meal_delivery,
        meal_takeaway,
        mosque,
        movie_rental,
        movie_theater,
        moving_company,
        museum,
        night_club,
        painter,
        park,
        parking,
        pet_store,
        pharmacy,
        physiotherapist,
        plumber,
        police,
        post_office,
        real_estate_agency,
        restaurant,
        roofing_contractor,
        rv_park,
        school,
        shoe_store,
        shopping_mall,
        spa,
        stadium,
        storage,
        store,
        subway_station,
        synagogue,
        taxi_stand,
        train_station,
        transit_station,
        travel_agency,
        university,
        veterinary_care,
        zoo
    }
}