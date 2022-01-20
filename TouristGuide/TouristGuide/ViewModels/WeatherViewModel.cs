using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.Models;
using TouristGuide.Models.Forecast;
using TouristGuide.Views;
using Xamarin.Forms;

namespace TouristGuide.ViewModels
{
    class WeatherViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Daily> DailyForecasts { get; set; }
        public ObservableCollection<Geocode> Geocodes { get; set; }
        public Command WeatherDetailsNavigationCommand { get; set; }
        public WeatherViewModel()
        {
            DailyForecasts = new ObservableCollection<Daily>();
            Geocodes = new ObservableCollection<Geocode>();
            WeatherDetailsNavigationCommand = new Command<Daily>(WeatherDetailsNavigation);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public async void GetDailyForecast()
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            var forecast = await ForecastRoot.GetWeather(position.Latitude, position.Longitude);
            var geocode = await GeocodeRoot.GetReverseGeocodingLocation(position.Latitude, position.Longitude);
            Geocodes.Clear();
            Geocodes.Add(geocode);
            DailyForecasts.Clear();
            OnPropertyChanged("Geocodes");
            foreach (var dailyForecast in forecast.daily)
            {
                DailyForecasts.Add(dailyForecast);
            }
            ChangeTime();
            ChangeWindSpeed();
            OnPropertyChanged("DailyForecasts");
        }
        private void ChangeTime()
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            foreach (var dailyForecast in DailyForecasts)
            {
                dailyForecast.date = dtDateTime.AddSeconds(dailyForecast.dt).ToLocalTime();
                dailyForecast.moonrise_dt = dtDateTime.AddSeconds(dailyForecast.moonrise).ToLocalTime();
                dailyForecast.moonset_dt = dtDateTime.AddSeconds(dailyForecast.moonset).ToLocalTime();
                dailyForecast.sunrise_dt = dtDateTime.AddSeconds(dailyForecast.sunrise).ToLocalTime();
                dailyForecast.sunset_dt = dtDateTime.AddSeconds(dailyForecast.sunset).ToLocalTime();
            }
        }
        private void ChangeWindSpeed()
        {
            foreach (var dailyForecast in DailyForecasts)
            {
                dailyForecast.wind_speed /= 10;
            }
        }
        private async void WeatherDetailsNavigation(Daily daily)
        {
            await App.Current.MainPage.Navigation.PushAsync(new WeatherDetailsPage(daily));

        }
    }
}
