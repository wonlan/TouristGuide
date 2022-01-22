using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Auth;
using TouristGuide.Other;
using Xamarin.Forms;

[assembly: Dependency(typeof(TouristGuide.Droid.Dependencies.Auth))]
namespace TouristGuide.Droid.Dependencies
{
    public class Auth : IAuth
    {
        public Auth()
        {
        }

        public async Task<bool> LoginUser(string email, string password)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (FirebaseAuthWeakPasswordException e)
            {
                throw new Exception(e.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                throw new Exception(e.Message);
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                throw new Exception("There is no user record corresponding to this identifier");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> RegisterUser(string email, string password)
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                return true;
            }
            catch (FirebaseAuthWeakPasswordException e)
            {
                throw new Exception(e.Message);
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                throw new Exception(e.Message);
            }
            catch (FirebaseAuthUserCollisionException e)
            {
                throw new Exception("User already exists");
            }
            catch (Exception e)
            {
                throw new Exception("There was an unknown error");
            }
        }

        public bool isAuthenticated()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser != null;
        }

        public string GetCurrentUserId()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Uid;
        }

        public async Task<bool> DeleteUser()
        {
            try
            {
                await Firebase.Auth.FirebaseAuth.Instance.CurrentUser.DeleteAsync();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<bool> ChangePassword()
        {
            try
            {
                var test = Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Email;
                await Firebase.Auth.FirebaseAuth.Instance.SendPasswordResetEmailAsync(Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Email);
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task <bool> LogoutUser()
        {
            try
            {
                Firebase.Auth.FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public string GetCurrentUserEmail()
        {
            return Firebase.Auth.FirebaseAuth.Instance.CurrentUser.Email;
        }
    }
}