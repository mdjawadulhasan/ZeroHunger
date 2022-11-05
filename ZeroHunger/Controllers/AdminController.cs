using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Models;
using ZeroHunger.Repo;

namespace ZeroHunger.Controllers
{
    public class AdminController : Controller
    {
      
        public ActionResult Index()
        {
            return View(RestaurantRepo.Get());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RestaurantModel restaurant)
        {
            try
            {
               
                if (ModelState.IsValid)
                {
                    RestaurantRepo.Create(restaurant);
                    return RedirectToAction("Index");
                }
               
                return View(restaurant);

            }
            catch
            {
                
                return View(restaurant);
            }
        }


        public ActionResult Edit(int id)
        {
            var restaurant = RestaurantRepo.Get(id);
            return View(restaurant);
        }

      
        [HttpPost]
        public ActionResult Edit(RestaurantModel restaurant)
        {
            try
            {
                RestaurantRepo.Edit(restaurant);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public ActionResult Delete(int id)
        {
            var restaurant = RestaurantRepo.Get(id);
            RestaurantRepo.Delete(restaurant);
            return RedirectToAction("Index");
        }


        public ActionResult ShowEmp()
        {
            return View(EmployeeRepo.Get());
        }

        public ActionResult CreateEmp()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateEmp(EmployeeModel emp)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    EmployeeRepo.Create(emp);
                    return RedirectToAction("ShowEmp");
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