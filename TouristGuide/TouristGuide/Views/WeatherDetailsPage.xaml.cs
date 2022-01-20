using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.Models.Forecast;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouristGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WeatherDetailsPage : ContentPage
    {
        public WeatherDetailsPage()
        {
            InitializeComponent();
        }
        public WeatherDetailsPage(Daily day)
        {
            InitializeComponent();
            test.Text = day.ToString();
        }
    }
}