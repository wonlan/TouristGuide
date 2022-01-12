using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TouristGuide.Other;
using TouristGuide.Views;
using Xamarin.Forms;

namespace TouristGuide.ViewModels
{
    class RegistrationViewModel : INotifyPropertyChanged
    {
        private string email;
        public string Email
        { 
            get=>email;
            set
            {
                email = value;
                OnPropertyChanged("EntriesHaveText");
            }
        }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged("EntriesHaveText");
            }
        }

        private string confirmPassword;
        public string ConfirmPassword
        {
            get => confirmPassword;
            set
            {
                confirmPassword=value;
                OnPropertyChanged("EntriesHaveText");
            }
        }

        private bool entriesHaveText { get; set; }
        public bool EntriesHaveText
        {
            get
            {
                return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(ConfirmPassword);
            }
        }

        public Command RegisterCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public RegistrationViewModel()
        {
            RegisterCommand = new Command<bool>(Register,CanRegister);
        }
        private async void Register(bool parameter)
        {
            if (await Auth.RegisterUser(email, password))
                await App.Current.MainPage.Navigation.PushAsync(new HomePage());
        }
        private bool CanRegister(bool parameter)
        {
            return EntriesHaveText;
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
