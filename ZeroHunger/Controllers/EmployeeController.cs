using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZeroHunger.Models;
using ZeroHunger.Repo;

namespace ZeroHunger.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Login(EmployeeModel em)
        {
            var restaurent = new EmployeeModel();
            restaurent = EmployeeRepo.Get(em.EmpUserName,em.EmpPassword);
            if (restaurent.EmpUserName == null)
            {
                TempData["emp"] = "null";
                return View();
            }
            else
            {
                Session["emp"] = restaurent;
                return RedirectToAction("Profile");
            }

        }


        public ActionResult Profile()
        {
            return View();
        }

        public ActionResult SeeAvlReq()
        {
            EmployeeModel em = new EmployeeModel();
            em = (EmployeeModel)Session["emp"]; 
            return View(CollectionRequestRepo.GetAvlReq(em.Empid));
        }


        public ActionResult CompleteReq(int id)
        {
            EmployeeModel em = new EmployeeModel();
            em = (EmployeeModel)Session["emp"];
            CollectionRequestRepo.CompleteReq(id,em);
            return RedirectToAction("SeeAvlReq");
        }
    }
}