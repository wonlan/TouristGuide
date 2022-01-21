using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.Models.Forecast
{
    static class ForecastRoot
    {
        public static string GenerateURL(double latitude, double longitude)
        {
            string url = "https://api.openweathermap.org/data/2.5/onecall?lat="+latitude.ToString()+"&lon="+longitude.ToString()+"&units=metric"+"&appid=d122c0ab9ab8bed07ad9650660342492";
            return url;
        }
        //test
        public static async Task<Forecast> GetWeather(double latitude, double longitude)
        {
            Forecast forecast = new Forecast();
            string url = GenerateURL(latitude, longitude);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                forecast = JsonConvert.DeserializeObject<Forecast>(json);
            }
            return forecast;
        }

    }
    public class Weather
    {
        public int id { get; set; }
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string icon_url => string.Format("{0}{1}{2}", "https://openweathermap.org/img/wn/", icon, "@4x.png");
    }

    public class Current
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }
        public IList<Weather> weather { get; set; }
    }
    public class Minutely
    {
        public int dt { get; set; }
        public int precipitation { get; set; }
    }
    public class Hourly
    {
        public int dt { get; set; }
        public double temp { get; set; }
        public double feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double uvi { get; set; }
        public int clouds { get; set; }
        public int visibility { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }
        public IList<Weather> weather { get; set; }
        public double pop { get; set; }
    }

    public class Temp
    {
        public double day { get; set; }
        public double min { get; set; }
        public double max { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    public class FeelsLike
    {
        public double day { get; set; }
        public double night { get; set; }
        public double eve { get; set; }
        public double morn { get; set; }
    }

    public class Daily
    {
        public int dt { get; set; }
        public int sunrise { get; set; }
        public int sunset { get; set; }
        public int moonrise { get; set; }
        public int moonset { get; set; }
        public double moon_phase { get; set; }
        public Temp temp { get; set; }
        public FeelsLike feels_like { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double dew_point { get; set; }
        public double wind_speed { get; set; }
        public int wind_deg { get; set; }
        public double wind_gust { get; set; }
        public IList<Weather> weather { get; set; }
        public int clouds { get; set; }
        public double pop { get; set; }
        public double snow { get; set; }
        public double rain { get; set; }
        public double uvi { get; set; }
        public DateTime date { get; set; }
        public double precipitation => snow + rain;
        public double probability => pop * 100;
        public DateTime sunset_dt { get; set; }
        public DateTime sunrise_dt { get; set; }
        public DateTime moonrise_dt { get; set; }
        public DateTime moonset_dt { get; set; }

    }

    public class Forecast
    {
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; }
        public int timezone_offset { get; set; }
        public Current current { get; set; }
        public IList<Minutely> minutely { get; set; }
        public IList<Hourly> hourly { get; set; }
        public IList<Daily> daily { get; set; }
    }
}