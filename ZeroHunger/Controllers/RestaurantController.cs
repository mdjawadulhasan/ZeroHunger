﻿using System;
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

                    crm.CrId = rm.Rid;
                    crm.Date = DateTime.Now;
                    CollectionRequestRepo.Create(crm);
                    return RedirectToAction("Profile");
                }

                return View();

            }
            catch
            {
                return View();
            }
        }

    }
}