using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using TouristGuide.Other;
using TouristGuide.Views;
using Xamarin.Forms;

namespace TouristGuide.ViewModels
{
    class ProfileViewModel : INotifyPropertyChanged
    {
        public string Email { get; set; }
        public int PostCount { get; set; }
        public Command ChangePasswordCommand { get; set; }
        public Command LogOutCommand { get; set; }
        public Command DeleteAccountCommand { get; set; }
        public Command PostListNavigationCommand { get; set; }
        public ProfileViewModel()
        {
            Email = Auth.GetCurrentUserEmail();
            ChangePasswordCommand = new Command(ChangePassword);
            LogOutCommand = new Command(LogOut);
            DeleteAccountCommand = new Command(DeleteAccount);
            PostListNavigationCommand = new Command(PostListNavigation);
            GetPostCount();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void ChangePassword()
        {
            await Auth.ChangePassword();
        }
        private async void LogOut()
        {
            await Auth.LogoutUser();
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
            var existingPages = App.Current.MainPage.Navigation.NavigationStack.ToList();
            App.Current.MainPage.Navigation.RemovePage(existingPages[0]);
        }
        private async void DeleteAccount()
        {
            await Auth.DeleteUser();
            await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
        private async void PostListNavigation()
        {
            await App.Current.MainPage.Navigation.PushAsync(new PostListPage());
        }
        public async void GetPostCount()
        {
            var temp = await Firestore.Read();
            PostCount = temp.Count;
            OnPropertyChanged("PostCount");
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
