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
        string GetCurrentUserId();
        bool isAuthenticated();

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
            auth.isAuthenticated();
            return true;
        }

        public static string GetCurrentUserId()
        {
            auth.GetCurrentUserId();
            return "";
        }

    }
}
