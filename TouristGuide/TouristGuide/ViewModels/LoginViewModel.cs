using TouristGuide.Other;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using TouristGuide.Views;

namespace TouristGuide.ViewModels
{
    class LoginViewModel : INotifyPropertyChanged
    {

        private string email;
        private string password;

        public Command LoginCommand { get; set; }
        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged("EntriesHaveText");
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("EntriesHaveText");
            }
        }
        private bool entriesHaveText { get; set; }
        public bool EntriesHaveText
        {
            get
            {
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
            }
        }
        public LoginViewModel()
        {
            LoginCommand = new Command<bool>(Login, CanLogin);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void Login(bool parameter)
        {
            //authenticate
            if (await Auth.LoginUser(Email, Password))
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());

        }
        private bool CanLogin(bool parameter)
        {
            return EntriesHaveText;
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
