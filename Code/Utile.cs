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

namespace Xamarin_BarcodeReader.Code
{
    class Utile
    {
        EFHandler efhandler;

        public Utile(string server, string user, string password)
        {
            efhandler = new EFHandler(server, user, password);
        }

        public bool TestConnection()
        {
            return efhandler.TestConnection();
        }
    }
}