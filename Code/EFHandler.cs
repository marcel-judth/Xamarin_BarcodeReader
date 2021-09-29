using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
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

        internal void Insert(DateTime now, string empNr, string satzart, string place, string eancode, string quantity)
        {
            string connetionString = $"Data Source={server};Initial Catalog=DatabaseName;User ID={user};Password={password}";
            var connection = new SqlConnection(connetionString);


            try
            {
                connection.Open();

                string sql = "INSERT INTO DATAFOX_LAGER(DATUM_UHRZEIT, MA_ID, SATZART, AUFTRAG, EANCODE, MENGE) VALUES(@param1,@param2,@param3,@param4,@param5,@param6)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Date).Value = now;
                    cmd.Parameters.Add("@param2", SqlDbType.VarChar, 50).Value = empNr;
                    cmd.Parameters.Add("@param3", SqlDbType.VarChar, 50).Value = satzart;
                    cmd.Parameters.Add("@param4", SqlDbType.VarChar, 50).Value = place;
                    cmd.Parameters.Add("@param5", SqlDbType.VarChar, 50).Value = eancode;
                    cmd.Parameters.Add("@param6", SqlDbType.VarChar, 50).Value = quantity;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                connection.Close();

            }
            catch (Exception)
            {
                connection.Close();
                throw;
            }

        }
    }
}