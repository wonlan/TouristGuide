using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using TouristGuide.Models;
using TouristGuide.Other;
using TouristGuide.Views;

namespace TouristGuide.ViewModels
{
    class PostListViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Post> Posts { get; set; }
        private Post selectedPost;

        public event PropertyChangedEventHandler PropertyChanged;

        public Post SelectedPost
        {
            get { return selectedPost; }
            set
            {
                selectedPost = value;
                if (selectedPost != null)
                    App.Current.MainPage.Navigation.PushAsync(new PostDetailPage(selectedPost));
            }
        }
        public PostListViewModel()
        {
            Posts = new ObservableCollection<Post>();
        }
        public async void GetPosts()
        {
            Posts.Clear();
            var posts = await Firestore.Read();
            foreach (var post in posts)
            {
                Posts.Add(post);
            }
            OnPropertyChanged("Posts");
        }
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
