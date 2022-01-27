using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.Models;
using TouristGuide.Models.Forecast;
using TouristGuide.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouristGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherPage : ContentPage
    {
        private WeatherViewModel vm;
        public WeatherPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            NavigationPage.SetHasNavigationBar(this, false);

            var existingPages = Navigation.NavigationStack.ToList();
            var test = existingPages[0].ToString();
            if (existingPages.Count > 1 && existingPages[0].ToString()=="TouristGuide.LoginPage")
            {
                Navigation.RemovePage(existingPages[0]);
            }

            vm = Resources["vm"] as WeatherViewModel;
            if (!vm.isSearched)
            {
                vm.GetDailyForecast();
            }
            else
            {
                vm.GetDailyForecastForCity(vm.Geocodes[0].name);
            }
        }
        //protected override bool OnBackButtonPressed()
        //{

        //    return true;
        //}
    }
}