using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace TouristGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewPostPage : ContentPage
    {
        private NewPostViewModel vm;
        public NewPostPage()
        {
            InitializeComponent();
        }
        public NewPostPage(MapClickedEventArgs e)
        {
            InitializeComponent();
            vm = Resources["vm"] as NewPostViewModel;
            vm.Latitude = e.Position.Latitude;
            vm.Longitude = e.Position.Longitude;
            vm.GetAdress();
        }
    }
}