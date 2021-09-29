using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Xamarin_BarcodeReader.Code
{
    class EFHandler
    {
        readonly string server;
        readonly string user;
        readonly string password;

        public EFHandler(string _server, string _user, string _password)
        {
            server = _server;
            user = _user;
            password = _password;
        }

        public bool TestConnection()
        {
            string connetionString = $"Data Source={server};Initial Catalog=DatabaseName;User ID={user};Password={password}";
            var cnn = new SqlConnection(connetionString);
            try
            {
                cnn.Open();
                cnn.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}