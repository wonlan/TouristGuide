using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TouristGuide.Models;
using TouristGuide.Other;
using Xamarin.Forms;

namespace TouristGuide.ViewModels
{

    class PostDetailViewModel : INotifyPropertyChanged
    {
        public PostDetailViewModel()
        {
            UpdateCommand = new Command(Update);
            DeleteCommand = new Command(Delete);
        }

        private string name;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged("Name");
                OnPropertyChanged("EntriesHaveText");
            }
        }

        private string description;

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged("Description");
                OnPropertyChanged("EntriesHaveText");
            }
        }
        private string adress;

        public string Adress
        {
            get => adress;
            set
            {
                adress = value;
                OnPropertyChanged("Adress");
            }
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Post SelectedPost { get; set; }

        public Command UpdateCommand { get; set; }
        public Command DeleteCommand { get; set; }
        private bool entriesHaveText { get; set; }
        public bool EntriesHaveText
        {
            get
            {
                return !string.IsNullOrEmpty(Name);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Update()
        {
            SelectedPost.Name = Name;
            SelectedPost.Description = Description;
            bool result = await Firestore.Update(SelectedPost);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
        }
        private async void Delete()
        {
            bool result = await Firestore.Delete(SelectedPost);
            if (result)
                await App.Current.MainPage.Navigation.PopAsync();
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
