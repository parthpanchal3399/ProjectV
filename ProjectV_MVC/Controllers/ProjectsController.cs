using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectV_MVC.Models;
using System.Net.Http;

namespace ProjectV_MVC.Controllers
{
    public class ProjectsController : Controller
    {
        GlobalVariables gv;

        // GET: Projects
        public ActionResult Index()
        {
            if(Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);

                IEnumerable<mvcProjectsModel> ProjectList;
                HttpResponseMessage response = gv.WebApiClient.GetAsync("Projects").Result;
                if (response.IsSuccessStatusCode)
                    ProjectList = response.Content.ReadAsAsync<IEnumerable<mvcProjectsModel>>().Result;
                else
                {
                    ProjectList = null;
                    TempData["ErrorMessage"] = "Error Fetching Data";
                }
                return View(ProjectList);
            }
            else
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index"
                });
        }


        public ActionResult AddorEditProjects(short ProjectID = 0)
        {
            if (Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);
                if (ProjectID == 0)
                    return View(new mvcProjectsModel());
                else
                {
                    HttpResponseMessage response = gv.WebApiClient.GetAsync("Projects/" + ProjectID.ToString()).Result;
                    if (response.IsSuccessStatusCode)
                        return View(response.Content.ReadAsAsync<mvcProjectsModel>().Result);
                    else
                    {
                        TempData["ErrorMessage"] = "Error Fetching Data";
                        return View(new mvcProjectsModel());
                    }
                }
            }
            else
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index"
                });
        }

        [HttpPost]
        public ActionResult AddorEditProjects(mvcProjectsModel project)
        {
            if (Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);

                //Create New
                if (project.ProjectID == 0)
                {
                    HttpResponseMessage response = gv.WebApiClient.PostAsJsonAsync("Projects", project).Result;
                    if (response.IsSuccessStatusCode)
                        TempData["SuccessMessage"] = "Saved Successfully";
                    else
                        TempData["ErrorMessage"] = "Error Saving Data";
                }

                //Update
                else
                {
                    HttpResponseMessage response = gv.WebApiClient.PutAsJsonAsync("Projects/" + project.ProjectID.ToString(), project).Result;
                    if (response.IsSuccessStatusCode)
                        TempData["SuccessMessage"] = "Updated Successfully";
                    else
                        TempData["ErrorMessage"] = "Error Updating Data";
                }
                return RedirectToAction("Index");
            }
            else
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index"
                });
        }


        public ActionResult DeleteProject(short? ProjectID)
        {
            if (Session["User"] != null)
            {
                gv = new GlobalVariables(new mvcUser { Username = "Parth", Password = "pass123" });

                if (!ProjectID.HasValue)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    HttpResponseMessage response = gv.WebApiClient.DeleteAsync("Projects/" + ProjectID.ToString()).Result;
                    if (response.IsSuccessStatusCode)
                        TempData["SuccessMessage"] = "Deleted Successfully";
                    else
                        TempData["ErrorMessage"] = "Error Deleting Data";
                    return RedirectToAction("Index");
                }
            }
            else
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index"
                });
        }
    }
}