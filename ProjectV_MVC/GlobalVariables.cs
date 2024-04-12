using ProjectV_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace ProjectV_MVC
{
    public class GlobalVariables
    {
        public HttpClient WebApiClient = new HttpClient();

        public GlobalVariables(mvcUser User)
        {
            WebApiClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiEndpoint"]);
            WebApiClient.DefaultRequestHeaders.Clear();
            WebApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            string authInfo = User.Username + ":" + User.Password;
            authInfo = Convert.ToBase64String(Encoding.Default.GetBytes(authInfo));

            WebApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authInfo);
        }

    }
}