using ProjectV_MVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace ProjectV_MVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(mvcUser User)
        {
            if(User != null)
            {
                HttpClient WebApiClient = new HttpClient();
                WebApiClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["WebApiEndpoint"]);
                WebApiClient.DefaultRequestHeaders.Clear();
                WebApiClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = WebApiClient.PostAsJsonAsync("Login", User).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["LoginMessage"] = "Login Successful";
                    Session["User"] = User;
                    return RedirectToRoute(new
                    {
                        controller = "Projects",
                        action = "Index"
                    });
                }
            }
            
            TempData["LoginError"] = "Invalid Credentials";
            Session["User"] = null;
            return View();
        }

        public ActionResult Logout()
        {
            if (Session["User"] != null)
            {
                Session["User"] = null;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }


    }
}