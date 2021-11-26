using System;

namespace Xamarin_BarcodeReader.Models
{
    class RequestObject
    {
        public DateTime date { get; set; }
        public string maID { get; set; }
        public string type { get; set; }
        public string order { get; set; }
        public string eanCode { get; set; }
        public double quantity { get; set; }
        public int companyNr { get; set; }
    }
}