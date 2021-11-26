using Android.App;
using Android.OS;
using Android.Widget;
using System;
using Xamarin.Essentials;

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
        }

        public void OnSaveClick(object sender, EventArgs e)
        {
            var companyNR = FindViewById<TextView>(Resource.Id.txtCompanyNr);

            Preferences.Set("companyNr", companyNR.Text);
            FinishAffinity();
        }
    }
}