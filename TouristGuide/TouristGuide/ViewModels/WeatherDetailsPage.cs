using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TouristGuide.Models.Forecast;

namespace TouristGuide.ViewModels
{
    class WeatherDetailsViewModel : INotifyPropertyChanged
    {
        private Daily daily;
        public Daily Daily
        {
            get => daily;
            set
            {
                daily = value;
                OnPropertyChanged("Daily");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
