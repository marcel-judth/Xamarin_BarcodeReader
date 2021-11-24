using System;
using System.Data;
using System.Data.SqlClient;

namespace Xamarin_BarcodeReader.Code
{
    class EFHandler
    {
        string server;
        string user;
        string password;
        string db;

        public EFHandler(string _server, string _db, string _user, string _password)
        {
            server = _server;
            user = _user;
            password = _password;
            db = _db;
        }

        public bool TestConnection()
        {
            //server = "HermessoDC\\hermessoSQL";
            //db = "Hermesso_Easybase";
            //user = "meso";
            //password = "Her@55mes";

            //server = "User-HP\\SQL2019";
            //db = "PZE_Lind_Apo";
            //user = "sa";
            //password = "CWL0mes0";

            string conString = new SqlConnectionStringBuilder()
            {
                DataSource = server,
                InitialCatalog = db,
                UserID = user,
                Password = password
            }.ConnectionString;
            var cnn = new SqlConnection(conString);
            try
            {
                cnn.Open();
                cnn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal void Insert(DateTime now, string empNr, string satzart, string place, string eancode, string quantity)
        {
            string connetionString = new SqlConnectionStringBuilder()
            {
                DataSource = server,
                InitialCatalog = db,
                UserID = user,
                Password = password
            }.ConnectionString;

            var connection = new SqlConnection(connetionString);

            try
            {
                try
                {
                    connection.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception("Verbindung fehlgeschlagen! Bitte Internetverbindung überprüfen.");
                }

                string sql = "INSERT INTO DATAFOX_LAGER(DATUM_UHRZEIT, MA_ID, SATZART, AUFTRAG, EANCODE, MENGE) VALUES(@param1,@param2,@param3,@param4,@param5,@param6)";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.DateTime).Value = now;
                    cmd.Parameters.Add("@param2", SqlDbType.VarChar).Value = empNr;
                    cmd.Parameters.Add("@param3", SqlDbType.VarChar).Value = satzart ?? "";
                    cmd.Parameters.Add("@param4", SqlDbType.VarChar).Value = place;
                    cmd.Parameters.Add("@param5", SqlDbType.VarChar).Value = eancode;
                    cmd.Parameters.Add("@param6", SqlDbType.VarChar).Value = quantity;
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