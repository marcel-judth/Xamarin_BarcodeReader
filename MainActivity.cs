using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Snackbar;
using System;
using Xamarin.Essentials;

namespace Xamarin_BarcodeReader
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetupMainView();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        public bool OnNavigationItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.navigation_home:
                    SetupView();
                    SetupMainView();
                    return true;
                case Resource.Id.navigation_dashboard:
                    SetupSecondView();
                    return true;
                case Resource.Id.navigation_notifications:
                    SetupThirdView();
                    return true;
            }
            return false;
        }

        public void OnSaveClick(object sender, EventArgs e)
        {
            View view = (View)sender;
            var Scan_Data = FindViewById<TextView>(Resource.Id.textEAN);
            var Quantitiy = FindViewById<TextView>(Resource.Id.textQty);

            Snackbar.Make(view, Scan_Data.Text, Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();

            Quantitiy.Text = "1";
            Scan_Data.Text = "";
        }

        public void OnSettingsSave(object sender, EventArgs e)
        {
            View view = (View)sender;
            var employeeNr = FindViewById<TextView>(Resource.Id.textEmpNr);
            var place = FindViewById<TextView>(Resource.Id.textPlace);

            Preferences.Set("employeeNr", employeeNr.Text);
            Preferences.Set("place", place.Text);


            Snackbar.Make(view, "Speichern erfolgreich", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();
        }


        private void SetupMainView()
        {
            SetContentView(Resource.Layout.activity_main);
            SetupView();
            Button save = FindViewById<Button>(Resource.Id.btnSave);
            save.Click += OnSaveClick;

        }

        private void SetupSecondView()
        {
            SetContentView(Resource.Layout.activity_inventory);
            SetupView();
            Button save = FindViewById<Button>(Resource.Id.btnSave);
            save.Click += OnSaveClick;

        }

        private void SetupThirdView()
        {
            SetContentView(Resource.Layout.activity_settings);
            SetupView();
            Button save = FindViewById<Button>(Resource.Id.btnSave);
            save.Click += OnSettingsSave;
            var employeeNr = FindViewById<TextView>(Resource.Id.textEmpNr);
            var place = FindViewById<TextView>(Resource.Id.textPlace);

            var empNr = Preferences.Get("employeeNr", "MA01");
            var pl = Preferences.Get("place", "L01");

            employeeNr.Text = empNr;
            place.Text = pl;
        }

        private void SetupView()
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }
    }
}

