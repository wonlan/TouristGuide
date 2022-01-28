using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.Models;
using TouristGuide.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TouristGuide.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostDetailPage : ContentPage
    {
        private PostDetailViewModel vm;
        public PostDetailPage(Post selectedPost)
        {
            InitializeComponent();
            vm = Resources["vm"] as PostDetailViewModel;
            vm.SelectedPost = selectedPost;
            vm.Adress = selectedPost.Adress;
            vm.Description = selectedPost.Description;
            vm.Name = selectedPost.Name;

        }
    }
}