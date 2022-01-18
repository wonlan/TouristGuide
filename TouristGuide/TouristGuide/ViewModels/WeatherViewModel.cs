using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.Models.Forecast;

namespace TouristGuide.ViewModels
{
    class WeatherViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Daily> DailyForecasts { get; set; }
        public WeatherViewModel()
        {
            DailyForecasts = new ObservableCollection<Daily>();
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
            DailyForecasts.Clear();
            foreach (var dailyForecast in forecast.daily)
            {
                DailyForecasts.Add(dailyForecast);
            }
            ChangeTime();
            concatDayMonth();
            OnPropertyChanged("DailyForecasts");
        }
        private void ChangeTime()
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            foreach (var dailyForecast in DailyForecasts)
            {
                dailyForecast.date= dtDateTime.AddSeconds(dailyForecast.dt).ToLocalTime();
            }
        }
        private void concatDayMonth()
        {
            foreach (var dailyForecast in DailyForecasts)
            {
                dailyForecast.dayMonth = dailyForecast.date.Day.ToString() + "." + dailyForecast.date.Month.ToString();
            }
        }
    }
}
