using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.Models
{
    class GeocodeRoot
    {
        public static string GenerateURL(double latitude, double longitude)
        {
            string url = "https://api.openweathermap.org/geo/1.0/reverse?lat=" + latitude.ToString() + "&lon=" + longitude.ToString() + "&appid=d122c0ab9ab8bed07ad9650660342492";
            return url;
        }
        public static async Task<Geocode> GetReverseGeocodingLocation(double latitude, double longitude)
        {
            Geocode geocode = new Geocode();
            string url = GenerateURL(latitude, longitude);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                geocode = JsonConvert.DeserializeObject<Geocode>(json.Substring(1, json.Length - 2));
            }
            return geocode;
        }
    }
    public class LocalNames
    {
        public string zh { get; set; }
        public string la { get; set; }
        public string uk { get; set; }
        public string it { get; set; }
        public string fa { get; set; }
        public string el { get; set; }
        public string ro { get; set; }
        public string en { get; set; }
        public string es { get; set; }
        public string fr { get; set; }
        public string mk { get; set; }
        public string fi { get; set; }
        public string mt { get; set; }
        public string be { get; set; }
        public string eo { get; set; }
        public string ca { get; set; }
        public string eu { get; set; }
        public string sk { get; set; }
        public string hu { get; set; }
        public string sl { get; set; }
        public string pt { get; set; }
        public string de { get; set; }
        public string hr { get; set; }
        public string ja { get; set; }
        public string pl { get; set; }
        public string ar { get; set; }
        public string lv { get; set; }
        public string sr { get; set; }
        public string lt { get; set; }
        public string cs { get; set; }
        public string nl { get; set; }
        public string ru { get; set; }
    }

    public class Geocode
    {
        public string name { get; set; }
        public LocalNames local_names { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string country { get; set; }
        public string state { get; set; }
        public string label => name+" | "+country;
    }
}
