using System;
using System.Collections.Generic;
using System.Text;
using TouristGuide.Other;
using Xamarin.Forms;

namespace TouristGuide.ViewModels
{
    class ProfileViewModel
    {
        public string Email { get; set; }
        public Command ChangePasswordCommand { get; set; }
        public Command LogOutCommand { get; set; }
        public Command DeleteAccountCommand { get; set; }
        public ProfileViewModel()
        {
            Email = Auth.GetCurrentUserEmail();
            ChangePasswordCommand = new Command(ChangePassword);
            LogOutCommand = new Command(LogOut);
            DeleteAccountCommand = new Command(DeleteAccount);
            
        }
        private async void ChangePassword()
        {
            await Auth.ChangePassword();
        }
        private async void LogOut()
        {
            await Auth.LogoutUser();
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
        private async void DeleteAccount()
        {
            await Auth.DeleteUser();
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
    }
}
