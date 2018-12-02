using System;
using System.Collections.Generic;
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

        public ActionResult Login(AdminViewModel admin)
        {
            if(admin.Id == 42694514 && admin.Code == "F4A6S4S1")
            {
                return RedirectToAction("Main");
            }
            return View("Login", admin);
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