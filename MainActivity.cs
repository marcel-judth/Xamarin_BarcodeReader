﻿using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.App;
using Google.Android.Material.BottomNavigation;
using Google.Android.Material.Snackbar;
using System;
using Xamarin.Essentials;

namespace Xamarin_BarcodeReader
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true, WindowSoftInputMode = SoftInput.StateAlwaysHidden)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetupMainView();
            var inputManager = (InputMethodManager)GetSystemService(InputMethodService);
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

            Snackbar.Make(view, "Speichern erfolgreich", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();

            Quantitiy.Text = "1";
            Scan_Data.Text = "";
            Scan_Data.RequestFocus();
        }

        public void OnSaveInventoryClick(object sender, EventArgs e)
        {
            View view = (View)sender;
            var Scan_Data = FindViewById<TextView>(Resource.Id.textEAN);
            var Quantitiy = FindViewById<TextView>(Resource.Id.textQty);

            Snackbar.Make(view, "Speichern erfolgreich", Snackbar.LengthLong)
                .SetAction("Action", (View.IOnClickListener)null).Show();

            Quantitiy.Text = "";
            Scan_Data.Text = "";
            Scan_Data.RequestFocus();
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

            FindViewById<TextView>(Resource.Id.textEAN).ShowSoftInputOnFocus = false;
            FindViewById<TextView>(Resource.Id.textQty).ShowSoftInputOnFocus = false;
            FindViewById<TextView>(Resource.Id.textEAN).RequestFocus();
        }

        private void SetupSecondView()
        {
            SetContentView(Resource.Layout.activity_inventory);
            SetupView(Resource.Id.navigation_dashboard);
            Button save = FindViewById<Button>(Resource.Id.btnSave);
            save.Click += OnSaveInventoryClick;
            FindViewById<TextView>(Resource.Id.textEAN).ShowSoftInputOnFocus = false;
            FindViewById<TextView>(Resource.Id.textQty).ShowSoftInputOnFocus = false;
            FindViewById<TextView>(Resource.Id.textEAN).RequestFocus();
        }

        private void SetupThirdView()
        {
            SetContentView(Resource.Layout.activity_settings);
            SetupView(Resource.Id.navigation_notifications);
            Button save = FindViewById<Button>(Resource.Id.btnSave);
            save.Click += OnSettingsSave;
            var employeeNr = FindViewById<TextView>(Resource.Id.textEmpNr);
            var place = FindViewById<TextView>(Resource.Id.textPlace);

            employeeNr.ShowSoftInputOnFocus = false;
            place.ShowSoftInputOnFocus = false;

            var empNr = Preferences.Get("employeeNr", "MA01");
            var pl = Preferences.Get("place", "L01");

            employeeNr.Text = empNr;
            place.Text = pl;

        }

        private void SetupView(int id = Resource.Id.navigation_home)
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }
    }
}

