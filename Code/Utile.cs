using System;
using Xamarin_BarcodeReader.Models;

namespace Xamarin_BarcodeReader.Code
{
    class Utile
    {
        RESTHandler resthandler;
        int _companyNr;

        public Utile(int companyNr)
        {
            resthandler = new RESTHandler();
            _companyNr = companyNr;
        }

        internal void AddScannerData(string eancode, double quantity, string empNr, string place, string type)
        {
            var reqObj = new RequestObject
            {
                companyNr = _companyNr,
                date = DateTime.Now,
                eanCode = eancode,
                maID = empNr,
                order = place,
                quantity = quantity,
                type = type
            };
            resthandler.CallRestService(reqObj);
        }
    }
}