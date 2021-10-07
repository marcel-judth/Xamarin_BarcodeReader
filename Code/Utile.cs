using System;

namespace Xamarin_BarcodeReader.Code
{
    class Utile
    {
        EFHandler efhandler;

        public Utile(string server, string db, string user, string password)
        {
            efhandler = new EFHandler(server, db, user, password);
        }

        public bool TestConnection()
        {
            return efhandler.TestConnection();
        }

        internal void InsertInventory(string eancode, string quantity, string empNr, string place)
        {
            efhandler.Insert(DateTime.Now, empNr, "I", place, eancode, quantity);
        }

        internal void InsertTakeaway(string eancode, string quantity, string empNr, string place)
        {
            efhandler.Insert(DateTime.Now, empNr, null, place, eancode, quantity);
        }
    }
}