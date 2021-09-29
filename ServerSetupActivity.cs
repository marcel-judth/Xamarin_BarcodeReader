using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Snackbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin_BarcodeReader.Code;

namespace Xamarin_BarcodeReader
{
    [Activity(Label = "ServerSetupActivity")]
    public class ServerSetupActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_serversetup);
            Button save = FindViewById<Button>(Resource.Id.btnSave);
            save.Click += OnSaveClick;

            Button test = FindViewById<Button>(Resource.Id.btnTest);
            test.Click += OnTestClick;
        }

        public void OnTestClick(object sender, EventArgs e)
        {
            View view = (View)sender;
            var server = FindViewById<TextView>(Resource.Id.txtServername);
            var user = FindViewById<TextView>(Resource.Id.txtUser);
            var password = FindViewById<TextView>(Resource.Id.txtPassword);

            var utile = new Utile(server.Text, user.Text, password.Text);
            if (utile.TestConnection())
                Snackbar.Make(view, "Verbindung erfolgreich", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
            else
                Snackbar.Make(view, "Verbindung fehlgeschlagen", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }

        public void OnSaveClick(object sender, EventArgs e)
        {
            var server = FindViewById<TextView>(Resource.Id.txtServername);
            var user = FindViewById<TextView>(Resource.Id.txtUser);
            var password = FindViewById<TextView>(Resource.Id.txtPassword);


            Preferences.Set("server", server.Text);
            Preferences.Set("user", user.Text);
            Preferences.Set("password", password.Text);
            FinishAffinity();
        }
    }
}