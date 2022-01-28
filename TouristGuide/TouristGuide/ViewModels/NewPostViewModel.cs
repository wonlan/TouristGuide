using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TouristGuide.Models;
using TouristGuide.Other;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace TouristGuide.ViewModels
{
    class NewPostViewModel : INotifyPropertyChanged
    {
        private string name;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("EntriesHaveText");
            }
        }
        public string Description { get; set; }
        public string Adress { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Command SaveCommand { get; set; }
        private bool entriesHaveText { get; set; }
        public bool EntriesHaveText
        {
            get
            {
                return !string.IsNullOrEmpty(Name);
            }
        }
        public NewPostViewModel()
        {
            SaveCommand = new Command<bool>(Save,CanSave);
            Description = "";
        }
        private void Save(bool parameter)
        {
            GetAdress();
            Post post = new Post
            {
                Adress = Adress,
                Name = Name,
                Description = Description,
                Latitude = Latitude,
                Longitude = Longitude
            };
            if (Firestore.Insert(post))
            {
                App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                App.Current.MainPage.DisplayAlert("Error", "Point cannot be added", "OK");
            }
            
        }
        private bool CanSave(bool parameter)
        {
            return EntriesHaveText;
        }
        public async void GetAdress()
        {
            var test = await CrossGeolocator.Current.GetAddressesForPositionAsync(new Position(Latitude, Longitude));
            var temp = test.GetEnumerator();
            temp.MoveNext();
            var c = temp.Current;
            Adress = c.Thoroughfare + " " + c.FeatureName + ", " + c.Locality + " " + c.AdminArea + " " + c.CountryName;
            OnPropertyChanged("Adress");
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
