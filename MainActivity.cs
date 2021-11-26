using Android.App;
using Android.Content;
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
using Xamarin_BarcodeReader.Code;

namespace Xamarin_BarcodeReader
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", WindowSoftInputMode = SoftInput.StateAlwaysHidden, MainLauncher = true)]
    public class MainActivity : AppCompatActivity, BottomNavigationView.IOnNavigationItemSelectedListener
    {
        string companyNr;
        private Utile utile;


        protected override void OnCreate(Bundle savedInstanceState)
        {
            CheckServerSettings();


            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);
            SetupThirdView();
        }

        private void SetEmpTexts()
        {
            var empNr = Preferences.Get("employeeNr", "MA01");
            var pl = Preferences.Get("place", "L01");

            FindViewById<TextView>(Resource.Id.txtEmpNr).Text = "Ma-Nr.: " + empNr;
            FindViewById<TextView>(Resource.Id.txtPlace).Text = "Lagerplatz: " + pl;
        }

        private void CheckServerSettings()
        {
            companyNr = Preferences.Get("companyNr", null);

            if (string.IsNullOrEmpty(companyNr))
            {
                Intent intent = new Intent(this, typeof(ServerSetupActivity));
                StartActivity(intent);
            }

            int.TryParse(companyNr, out int result);

            utile = new Utile(result);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

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
            Button save = FindViewById<Button>(Resource.Id.btnSave);

            try
            {
                save.Enabled = false;
                var Scan_Data = FindViewById<TextView>(Resource.Id.textEAN);
                var Quantity = FindViewById<TextView>(Resource.Id.textQty);
                var empNr = Preferences.Get("employeeNr", "MA01");
                var pl = Preferences.Get("place", "L01");

                double qty = -1;

                double.TryParse(Quantity.Text, out qty);

                if (qty < 0 || qty > 1000)
                    throw new Exception("Ungültige Menge!");

                if (string.IsNullOrEmpty(Scan_Data.Text))
                    throw new Exception("Ungültige Barcodenummer!");

                utile.AddScannerData(Scan_Data.Text, qty, empNr, pl, "I");

                Quantity.Text = "1";
                Scan_Data.Text = "";
                Scan_Data.RequestFocus();
                Snackbar.Make(view, "Speichern erfolgreich", Snackbar.LengthLong)
                    .SetAction("Action", (View.IOnClickListener)null).Show();
                save.Enabled = true;

            }
            catch (Exception ex)
            {
                Snackbar.Make(view, ex.Message, Snackbar.LengthLong)
                    .SetAction("Action", (View.IOnClickListener)null).Show();
                save.Enabled = true;
            }
        }

        public void OnSaveInventoryClick(object sender, EventArgs e)
        {
            View view = (View)sender;
            Button save = FindViewById<Button>(Resource.Id.btnSave);

            try
            {
                save.Enabled = false;
                var Scan_Data = FindViewById<TextView>(Resource.Id.textEAN);
                var Quantity = FindViewById<TextView>(Resource.Id.textQty);
                var empNr = Preferences.Get("employeeNr", "MA01");
                var pl = Preferences.Get("place", "L01");

                double qty = -1;

                double.TryParse(Quantity.Text, out qty);

                if (qty < 0 || qty > 1000)
                    throw new Exception("Ungültige Menge!");

                if (string.IsNullOrEmpty(Scan_Data.Text))
                    throw new Exception("Ungültige Barcodenummer!");

                utile.AddScannerData(Scan_Data.Text, qty, empNr, pl, "I");

                Quantity.Text = "";
                Scan_Data.Text = "";
                Scan_Data.RequestFocus();
                Snackbar.Make(view, "Speichern erfolgreich", Snackbar.LengthLong)
                    .SetAction("Action", (View.IOnClickListener)null).Show();
                save.Enabled = true;
            }
            catch (Exception ex)
            {
                Snackbar.Make(view, ex.Message, Snackbar.LengthLong)
                    .SetAction("Action", (View.IOnClickListener)null).Show();
                save.Enabled = true;
            }
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
            SetupView();
            Button save = FindViewById<Button>(Resource.Id.btnSave);
            save.Click += OnSaveInventoryClick;
            FindViewById<TextView>(Resource.Id.textEAN).ShowSoftInputOnFocus = false;
            FindViewById<TextView>(Resource.Id.textQty).ShowSoftInputOnFocus = false;
            FindViewById<TextView>(Resource.Id.textEAN).RequestFocus();
            SetEmpTexts();
        }

        private void SetupThirdView()
        {
            SetContentView(Resource.Layout.activity_settings);
            SetupView();
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

        private void SetupView()
        {
            BottomNavigationView navigation = FindViewById<BottomNavigationView>(Resource.Id.navigation);
            navigation.SetOnNavigationItemSelectedListener(this);
        }
    }
}

