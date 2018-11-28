using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using pro.Models;

namespace pro.Controllers
{
    public class adminController : Controller
    {
        // GET: admin
        [HttpGet]
        public ActionResult login()
        {
            admin Adminmodel = new admin();
            return View(Adminmodel);

        }
        [HttpPost]
        public ActionResult Login()
        {
            using (brokerEntities10 db = new brokerEntities10())
            {

                if (db.admins.Any(x => x.Id == "admin00" && x.Code == "12345"))
                {

                    ViewBag.SuccessMessage = "Login Successful";
                    Session["UserID"] = "12345";
                    return RedirectToAction("adminProfile", "admin");
                }
                ViewBag.LoginErrorMessage = "Wrong Email and password";
                return View();
            }

           
        }

        public ActionResult adminProfile()
        {
            
            return View();
        }


        [HttpGet]
        public ActionResult Register()

        {
            broker brokermodel = new broker();
            return View(brokermodel);
        }
        [HttpPost]

        [ValidateAntiForgeryToken]
        public ActionResult Register(broker brokermodel)
        {
            using (brokerEntities10 dbModel = new brokerEntities10())

            {

                if (dbModel.brokers.Any(x => x.Id == brokermodel.Id))
                {
                    ViewBag.DuplicateMessage = "Username already exists";
                    return View("Register", brokermodel);
                }
                dbModel.brokers.Add(brokermodel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registeration successful";
            return View("Register", new user());
        }









    }
}