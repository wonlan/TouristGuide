using Android.App;
using Android.Content;
using Android.Gms.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore;
using Java.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.Models;
using TouristGuide.Other;
using Xamarin.Forms;

[assembly: Dependency(typeof(TouristGuide.Droid.Dependencies.Firestore))]
namespace TouristGuide.Droid.Dependencies
{
    class Firestore : Java.Lang.Object, IFirestore, IOnCompleteListener
    {
        bool hasReaded = false;
        List<Post> posts;

        public Firestore()
        {
            posts = new List<Post>();
        }

        public bool Insert(Post post)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>
                {
                    { "name", post.Name },
                    { "description", post.Description },
                    { "latitude", post.Latitude },
                    { "longitude", post.Longitude },
                    { "adress", post.Adress },
                    { "userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                };
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Add(new HashMap(postDocument));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Post post)
        {
            try
            {
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Document(post.Id).Delete();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Update(Post post)
        {
            try
            {
                var postDocument = new Dictionary<string, Java.Lang.Object>
                {
                    { "name", post.Name },
                    { "description", post.Description },
                    { "latitude", post.Latitude },
                    { "longitude", post.Longitude },
                    { "adress", post.Adress },
                    { "userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid },
                };
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                collection.Document(post.Id).Update(postDocument);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<List<Post>> Read()
        {
            try
            {
                hasReaded = false;
                var collection = Firebase.Firestore.FirebaseFirestore.Instance.Collection("posts");
                var query = collection.WhereEqualTo("userId", Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid);
                query.Get().AddOnCompleteListener(this);
                for (int i = 0; i < 30; i++)
                {
                    await System.Threading.Tasks.Task.Delay(100);
                    if (hasReaded)
                        break;
                }
                return posts;
            }
            catch
            {
                return posts;
            }
        }

        public void OnComplete(Android.Gms.Tasks.Task task)
        {
            if (task.IsSuccessful)
            {
                var documents = (QuerySnapshot)task.Result;
                posts.Clear();
                foreach (var document in documents.Documents)
                {
                    Post newPost = new Post()
                    {
                        Name = document.Get("name").ToString(),
                        Description = document.Get("description").ToString(),
                        Adress = document.Get("adress").ToString(),
                        Latitude = (double)document.Get("latitude"),
                        Longitude = (double)document.Get("longitude"),
                        UserId = document.Get("userId").ToString(),
                        Id = document.Id

                    };
                    posts.Add(newPost);
                }
            }
            else
            {
                posts.Clear();
            }
            hasReaded = true;
        }
    }
}