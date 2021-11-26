using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin_BarcodeReader.Models;

namespace Xamarin_BarcodeReader.Code
{
    class RESTHandler
    {
        readonly string baseURL;

        public RESTHandler()
        {
            baseURL = "https://hermesso-barcodescanner.azurewebsites.net/api";
        }


        private void CallRestService(string url, object postData)
        {
            var client = new RestClient(url);

            client.Timeout = -1;
            var request = new RestRequest(Method.POST);

            request.AddHeader("ApiKey", "1234");
            request.AddHeader("Content-Type", "application/json");

            var body = JsonConvert.SerializeObject(postData);
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);


            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Error on calling service: {response.StatusCode}  {response.Content} {response.ErrorMessage}");
        }

        public void CallRestService(RequestObject reqObj)
        {
            string url = baseURL + "/ScannerData";

            CallRestService(url, reqObj);
        }
    }
}