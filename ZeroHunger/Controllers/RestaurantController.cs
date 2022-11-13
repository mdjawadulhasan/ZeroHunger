using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Models;
using ZeroHunger.Repo;

namespace ZeroHunger.Controllers
{
    public class RestaurantController : Controller
    {
        // GET: Restaurant
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(RestaurantModel rm)
        {
            var restaurent = new RestaurantModel();
            restaurent= RestaurantRepo.Get(rm.ResUname, rm.ResPass);
            if (restaurent.ResUname == null)
            {
                TempData["value"] = "invalid";
                return View();
            }
            else
            {
                Session["rs"] = restaurent;
                return RedirectToAction("Profile");
            }

        }

        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult CreateRequest()
        {
            return View();
        }


        [HttpPost]
        public ActionResult CreateRequest(CollectionRequestModel crm)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    RestaurantModel rm = new RestaurantModel();
                    rm = (RestaurantModel)Session["rs"];

                    if (CollectionRequestRepo.checkRequest(rm.Rid) <1)
                    {
                        crm.CrId = rm.Rid;
                        crm.Date = DateTime.Now;
                        CollectionRequestRepo.Create(crm);
                        return RedirectToAction("TrackRequest");
                    }
                    else
                    {
                        TempData["crreq"] = "avlbl";
                        return RedirectToAction("TrackRequest");
                       
                    }

                   
                }

                return View();

            }
            catch
            {
                return View();
            }
        }


        public ActionResult TrackRequest()
        {
            RestaurantModel rm = new RestaurantModel();
            rm = (RestaurantModel)Session["rs"];
            return View(CollectionRequestRepo.GetReq(rm.Rid));
        }



        public ActionResult Delete(int id)
        {
            CollectionRequestRepo.Delete(id);
            return RedirectToAction("TrackRequest");
        }


        public ActionResult EmpDetails(int id)
        {
            return View(EmployeeRepo.Get(id));
        }


    }
}