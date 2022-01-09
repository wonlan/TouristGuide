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

namespace TouristGuide.Droid
{
    [Activity
        (Label = "SplashActivity1",
        MainLauncher =true,
        Theme ="@style/MainTheme.Splash",
        NoHistory =true,
        Icon ="@mipmap/icon")]
    public class SplashActivity1 : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.splash);
            Task startupWork = new Task(() => { SimulateStartupAsync(); });
            startupWork.Start();
            // Create your application here
        }
        private async Task SimulateStartupAsync()
        {
            await Task.Delay(1800);
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));

        }
    }
}