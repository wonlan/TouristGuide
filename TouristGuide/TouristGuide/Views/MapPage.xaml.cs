using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using TouristGuide.Other;

namespace TouristGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            GetLocation();
            //test api
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            var venues = await Result.GetNearbyVenues(position.Latitude, position.Longitude);
            DisplayNearbyPlaces(venues);
            await DisplayPosts();
            NavigationPage.SetHasNavigationBar(this, false);
        }
        //protected override bool OnBackButtonPressed()
        //{
        //    return true;
        //}
        private void DisplayNearbyPlaces(List<Result> venues)
        {
            foreach (var venue in venues)
            {
                var pinCoordinates =
                    new Xamarin.Forms.Maps.Position(venue.geocodes.main.latitude, venue.geocodes.main.longitude);
                var pin = new Pin()
                {
                    Position = pinCoordinates,
                    Label = venue.name,
                    Address = venue.location.address,
                    Type = PinType.SearchResult
                };
                locationsMap.Pins.Add(pin);
            }

        }
        private async void GetLocation()
        {
            var status = await CheckAndRequestLocationPermission();
            if (status == PermissionStatus.Granted)
            {
                var location = await Geolocation.GetLocationAsync();

                var locator = CrossGeolocator.Current;
                locator.PositionChanged += Locator_PositionChanged;
                if (!locator.IsListening)
                {
                    await locator.StartListeningAsync(new TimeSpan(0, 0, 1), 30);
                }
                locationsMap.IsShowingUser = true;

                CenterMap(location.Latitude, location.Longitude);
            }
        }
        private async void Locator_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();
            CenterMap(e.Position.Latitude, e.Position.Longitude);
            var venues = await Result.GetNearbyVenues(position.Latitude, position.Longitude);
            DisplayNearbyPlaces(venues);

        }

        private void CenterMap(double latitude, double longitude)
        {
            Position center = new Position(latitude, longitude);
            MapSpan span = new MapSpan(center, 1, 1).WithZoom(400);
            locationsMap.MoveToRegion(span);
        }
        private async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return status;

            status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            return status;
        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            try
            {
                var locator = CrossGeolocator.Current;
                var position = await locator.GetPositionAsync();
                var venues = await Result.GetPlace(position.Latitude, position.Longitude, searchBar.Text);
                CenterMap(venues[0].geocodes.main.latitude, venues[0].geocodes.main.longitude);
                var pinCoordinates =
                        new Xamarin.Forms.Maps.Position(venues[0].geocodes.main.latitude, venues[0].geocodes.main.longitude);
                var pin = new Pin()
                {
                    Position = pinCoordinates,
                    Label = venues[0].name,
                    Type = PinType.SearchResult
                };
                locationsMap.Pins.Add(pin);
            }
            catch
            {
                DisplayAlert("Place not found","","OK");
            }
        }

        private void locationsMap_MapClicked(object sender, MapClickedEventArgs e)
        {
            Navigation.PushAsync(new NewPostPage(e));
        }
        private async Task<bool> DisplayPosts()
        {
            var posts = await Firestore.Read();
            foreach (var post in posts)
            {
                var pinCoordinates =
                    new Xamarin.Forms.Maps.Position(post.Latitude,post.Longitude);
                var pin = new Pin()
                {
                    Position = pinCoordinates,
                    Label = post.Name,
                    Address = post.Adress,
                    Type = PinType.SearchResult
                };
                locationsMap.Pins.Add(pin);
            }
            return true;
        }
    }
}