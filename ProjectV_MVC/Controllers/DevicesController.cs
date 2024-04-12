using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectV_MVC.Models;
using System.Net.Http;

namespace ProjectV_MVC.Controllers
{
    public class DevicesController : Controller
    {
        GlobalVariables gv;

        //GET: Devices
        public ActionResult Index()
        {
            if(Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);

                IEnumerable<mvcDeviceModel> DeviceList;
                HttpResponseMessage response = gv.WebApiClient.GetAsync("Devices").Result;
                if (response.IsSuccessStatusCode)
                    DeviceList = response.Content.ReadAsAsync<IEnumerable<mvcDeviceModel>>().Result;
                else
                {
                    DeviceList = null;
                    TempData["ErrorMessage"] = "Error Fetching Data";
                }
                return View(DeviceList);
            }
            else
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index"
                });
        }


        public ActionResult AddorEditDevices(short DeviceID = 0)
        {
            if(Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);

                IEnumerable<mvcProjectsModel> ProjectList;
                HttpResponseMessage res = gv.WebApiClient.GetAsync("Projects").Result;
                if (res.IsSuccessStatusCode)
                {
                    ProjectList = res.Content.ReadAsAsync<IEnumerable<mvcProjectsModel>>().Result;
                    ViewBag.Projects = ProjectList;
                }
                else
                {
                    TempData["ErrorMessage"] = "Error Fetching Data";
                }

                if (DeviceID == 0)
                    return View(new mvcDeviceModel());
                else
                {
                    HttpResponseMessage response = gv.WebApiClient.GetAsync("Devices/" + DeviceID.ToString()).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        return View(response.Content.ReadAsAsync<mvcDeviceModel>().Result);
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Error Fetching Data";
                        return View(new mvcDeviceModel());
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
        public ActionResult AddorEditDevices(mvcDeviceModel device)
        {
            if(Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);

                //Create New
                if (device.DeviceID == 0)
                {
                    HttpResponseMessage response = gv.WebApiClient.PostAsJsonAsync("Devices", device).Result;
                    if (response.IsSuccessStatusCode)
                        TempData["SuccessMessage"] = "Saved Successfully";
                    else
                        TempData["ErrorMessage"] = "Error Saving Data";
                }

                //Update
                else
                {
                    HttpResponseMessage response = gv.WebApiClient.PutAsJsonAsync("Devices/" + device.DeviceID.ToString(), device).Result;
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


        public ActionResult DeleteDevice(short? DeviceID)
        {
            if(Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);

                if (!DeviceID.HasValue)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    HttpResponseMessage response = gv.WebApiClient.DeleteAsync("Devices/" + DeviceID.ToString()).Result;
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


        public ActionResult Values(short? DeviceID)
        {
            if(Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);

                if (!DeviceID.HasValue)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    HttpResponseMessage response = gv.WebApiClient.GetAsync("Devices/" + DeviceID.ToString()).Result;
                    mvcDeviceModel Device = null;
                    if (response.IsSuccessStatusCode)
                        Device = response.Content.ReadAsAsync<mvcDeviceModel>().Result;
                    else
                        TempData["ErrorMessage"] = "Error Fetching Data";
                    return View(Device);
                }
            }
            else
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index"
                });
        }


        public ActionResult AddorEditValues(short? DeviceID)
        {
            if(Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);

                if (!DeviceID.HasValue)
                    return RedirectToAction("Index");
                else
                {
                    HttpResponseMessage response = gv.WebApiClient.GetAsync("Devices/" + DeviceID.ToString()).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        mvcDeviceModel Device = response.Content.ReadAsAsync<mvcDeviceModel>().Result;
                        mvcValuesRecModel values = Device.ValueRecs.Count == 0 ? new mvcValuesRecModel() : Device.ValueRecs.First();
                        Tuple<mvcDeviceModel, mvcValuesRecModel> tupleModel = new Tuple<mvcDeviceModel, mvcValuesRecModel>(Device, values);
                        return View(tupleModel);
                    }
                    else
                        return View(new Tuple<mvcDeviceModel, mvcValuesRecModel>(null, null));
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
        public ActionResult AddorEditValues([Bind(Include = "DeviceID, CreateDate, RecordedVal", Prefix ="Item2")] mvcValuesRecModel valueRec)
        {

            if(Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);

                HttpResponseMessage response = gv.WebApiClient.PostAsJsonAsync("Devices/AddValue", valueRec).Result;
                if (response.IsSuccessStatusCode)
                {
                    TempData["SuccessMessage"] = "Saved Successfully";
                }
                else
                {
                    TempData["ErrorMessage"] = "Error Saving Data";
                }
                return RedirectToAction("Values", new { DeviceID = valueRec.DeviceID });
            }
            else
                return RedirectToRoute(new
                {
                    controller = "Login",
                    action = "Index"
                });
        }


        public ActionResult DeleteValue(short? ValueID)
        {
            if (Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);

                short deviceID = 0;
                if (!ValueID.HasValue)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    HttpResponseMessage response = gv.WebApiClient.DeleteAsync("Devices/DeleteValue/" + ValueID.ToString()).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        TempData["SuccessMessage"] = "Deleted Successfully";
                        deviceID = response.Content.ReadAsAsync<mvcValuesRecModel>().Result.DeviceID;
                    }
                    else
                        TempData["ErrorMessage"] = "Error Deleting Data";
                    return RedirectToAction("Values", new { DeviceID = deviceID });
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
        public JsonResult GetValuesForChart(short? DeviceID)
        {
            if (Session["User"] != null)
            {
                gv = new GlobalVariables(Session["User"] as mvcUser);

                if (!DeviceID.HasValue)
                {
                    return null;
                }
                else
                {
                    HttpResponseMessage response = gv.WebApiClient.GetAsync("Devices/" + DeviceID.ToString()).Result;
                    mvcDeviceModel Device = null;
                    if (response.IsSuccessStatusCode)
                    {
                        Device = response.Content.ReadAsAsync<mvcDeviceModel>().Result;
                        List<mvcValuesRecModel> ValueRecs = Device.ValueRecs.OrderBy(x => x.CreateDate).ToList();
                        List<object> data = new List<object>();
                        data.Add(ValueRecs.Select(x => x.CreateDate).ToList());
                        data.Add(ValueRecs.Select(x => x.RecordedVal).ToList());
                        return Json(data, JsonRequestBehavior.AllowGet);
                    }
                    return null;
                }
            }
            else
                return null;
        }

    }
}