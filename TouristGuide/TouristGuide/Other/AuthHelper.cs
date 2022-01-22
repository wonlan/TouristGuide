using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TouristGuide;
using Xamarin.Forms;

namespace TouristGuide.Other
{
    public interface IAuth
    {
        Task<bool> RegisterUser(string email, string password);
        Task<bool> LoginUser(string email, string password);
        Task<bool> DeleteUser();
        Task<bool> ChangePassword();
        Task<bool> LogoutUser();
        string GetCurrentUserId();
        bool isAuthenticated();
        string GetCurrentUserEmail();

    }

    public class Auth
    {
        private static IAuth auth = DependencyService.Get<IAuth>();
        public Auth()
        {
        }

        public static async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                return await auth.LoginUser(email, password);
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                return false;
            }

        }

        public static async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                return await auth.RegisterUser(email, password);
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                return false;
            }
        }

        public static bool isAuthenticated()
        {
            return auth.isAuthenticated();
        }

        public static string GetCurrentUserId()
        {
            auth.GetCurrentUserId();
            return "";
        }
        public static async Task<bool> DeleteUser()
        {
            try
            {
                return await auth.DeleteUser();
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                return false;
            }
        }
        public static async Task<bool> ChangePassword()
        {
            try
            {
                return await auth.ChangePassword();
            }
            catch (Exception e)
            {
                App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                return false;
            }
        }
        public static async Task<bool> LogoutUser()
        {
            try
            {
                return await auth.LogoutUser();
            }
            catch (Exception e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
                return false;
            }
        }
        public static string GetCurrentUserEmail()
        {
            return auth.GetCurrentUserEmail();
        }

    }
}
