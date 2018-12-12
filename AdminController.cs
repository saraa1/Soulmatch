using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Soul.Models;

namespace Soul.Controllers
{
    public class AdminController : Controller
    {
        private DbModels db = new DbModels();
        // GET: Admin
        public ActionResult Index()
        {
            return View(db.brokers);
        }

        [HttpGet]
        public ActionResult Login()
        {
            AdminViewModel admin = new AdminViewModel();
            return View(admin);

        }
        [HttpPost]
        public ActionResult Login(AdminViewModel admin)
        {
            if (admin.Id.Equals(1234) && admin.Code.Equals("fass"))
            {
                ViewBag.SuccessMessage = "Login Successful";
                return RedirectToAction("Main", "Admin");

            }

            return RedirectToAction("Main", "Admin");
        }

        public ActionResult Main()
        {
            return View();
        }

        public ActionResult RegisterBroker(broker broker)
        {
            using (DbModels dbModel = new DbModels())
            {
                if (dbModel.brokers.Any(x => x.BrokerID == broker.BrokerID))
                {
                    ViewBag.DuplicateMessage = "BrokerID already exists";
                    return View("RegisterBroker", broker);
                }
                dbModel.brokers.Add(broker);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registeration successful";
            return View("RegisterBroker", new broker());
        }

    }
}