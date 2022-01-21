using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.Other;
using Newtonsoft.Json;

namespace TouristGuide.Models
{
    public class Icon
    {
        public string prefix { get; set; }
        public string suffix { get; set; }
    }

    public class Category
    {
        public int id { get; set; }
        public string name { get; set; }
        public Icon icon { get; set; }
    }

    public class Main
    {
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class Geocodes
    {
        public Main main { get; set; }
    }

    public class Location
    {
        public string address { get; set; }
        public string country { get; set; }
        public string cross_street { get; set; }
        public string locality { get; set; }
        public string postcode { get; set; }
        public string region { get; set; }
        public IList<string> neighborhood { get; set; }
    }

    public class Parent
    {
        public string fsq_id { get; set; }
        public string name { get; set; }
    }

    public class RelatedPlaces
    {
        public Parent parent { get; set; }
    }

    public class Result
    {
        public string fsq_id { get; set; }
        public IList<Category> categories { get; set; }
        public IList<object> chains { get; set; }
        public int distance { get; set; }
        public Geocodes geocodes { get; set; }
        public Location location { get; set; }
        public string name { get; set; }
        public RelatedPlaces related_places { get; set; }
        public string timezone { get; set; }

        public static async Task<List<Result>> GetNearbyVenues(double latitude, double longitude)
        {
            List<Result> venues = new List<Result>();
            var url = VenueRoot.GenerateURL_forsquarePlacesNearby(latitude, longitude);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "fsq3LhmjhqnLL8417FVHrd+wF8e7yQQdfLscps84MwpiNNs=");
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

                venues = venueRoot.results as List<Result>;
            }
            return venues;
        }

        public static async Task<List<Result>> GetPlace(double latitude, double longitude, string name)
        {
            List<Result> venues = new List<Result>();
            var url = VenueRoot.GenerateURL_Place(latitude, longitude,name);
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "fsq3LhmjhqnLL8417FVHrd+wF8e7yQQdfLscps84MwpiNNs=");
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();

                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);

                venues = venueRoot.results as List<Result>;
            }
            return venues;
        }
    }


    class VenueRoot
    {
        public List<Result> results { get; set; }
        public static string GenerateURL_forsquarePlacesNearby(double latitude, double longitude)
        {
            string url = "https://api.foursquare.com/v3/places/nearby?ll=" + latitude.ToString() + "," +
                         longitude.ToString();
            return url;
        }
        public static string GenerateURL_Place(double latitude, double longitude, string name)
        {
            string url = "https://api.foursquare.com/v3/places/search?query="+name+"&ll=" + latitude.ToString() + "," +
                         longitude.ToString() + "&radius=100000";
            return url;
        }
    }
}
