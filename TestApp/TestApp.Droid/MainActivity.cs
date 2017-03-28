using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using MaterialIcons.FormsPlugin.iOS;

namespace TestApp.Droid
{
    [Activity(Label = "TestApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

			Plugin.Iconize.Iconize.With(new Plugin.Iconize.Fonts.FontAwesomeModule())
				  .With(new Plugin.Iconize.Fonts.MaterialModule());

			MaterialIconControls.Init();

            global::Xamarin.Forms.Forms.Init(this, bundle);

            //FormsPlugin.Iconize.Droid.IconControls.Init(Resource.Id.toolbar, Resource.Id.tabs);

            LoadApplication(new App());
   
        }
    }
}

