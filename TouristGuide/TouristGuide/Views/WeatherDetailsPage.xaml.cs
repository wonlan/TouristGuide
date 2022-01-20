using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.Models.Forecast;
using TouristGuide.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouristGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherDetailsPage : ContentPage
    {
        private WeatherDetailsViewModel vm;
        public WeatherDetailsPage()
        {
            InitializeComponent();
        }
        public WeatherDetailsPage(Daily daily)
        {
            InitializeComponent();
            vm = Resources["vm"] as WeatherDetailsViewModel;
            vm.Daily = daily;
        }
    }
}