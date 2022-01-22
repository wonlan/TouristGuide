using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.Other;
using TouristGuide.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouristGuide
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            if (Auth.isAuthenticated())
            {
                Navigation.PushAsync(new HomePage());
            }
        }
    }
}