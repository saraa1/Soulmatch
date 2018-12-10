using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Soul.Models;

namespace Soul.Controllers
{
    public class BrokerController : Controller
    {
        private DbModels db = new DbModels();
        //*********************************************************************//

        // GET: broker
        [HttpGet]
        public ActionResult Login()
        {
            broker Brokermodel = new broker();
            return View(Brokermodel);

        }

        [HttpPost]
        public ActionResult Login(broker brokermodel)
        {

            using (DbModels db = new DbModels())
            {

                if (db.brokers.Any(x => x.Name == brokermodel.Name && x.Code == brokermodel.Code))
                {

                    ViewBag.SuccessMessage = "Login Successful";
                    Session["BrokerID"] = brokermodel.BrokerID;
                    return RedirectToAction("Userprofile", "Broker");
                }
                ViewBag.LoginErrorMessage = "Wrong Name and password";
                return View("Login", brokermodel);
            }

        }

        public ActionResult Userprofile()
        {
            user usermodel = new user();
            return View(usermodel);
        }
        public ActionResult requests()
        {

            var result = (from c in db.users
                          select new gridtable
                          {
                              UserID = c.UserID,
                              Image = c.Image,
                              Username = c.Username,
                              Password = c.Password,
                              Age = c.Age,
                              CNIC = c.CNIC,
                              Adress = c.Adress,
                              Contact_no = c.Contact_no,
                              Email = c.Email,
                              Salary = c.Salary,
                              Gender = c.Gender,
                              Religion = c.Religion,
                              Cast = c.Cast,
                              Profession = c.Profession,
                              Account_no = c.Account_no

                          }).ToList();
            return View(result);

        }


        [HttpGet]
        public ActionResult Allow(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user table_2 = db.users.Find(id);
            if (table_2 == null)
            {
                return HttpNotFound();
            }
            return View(table_2);
        }

        [HttpPost]

        [ActionName("Allow")]
        public ActionResult Allow(int id)
        {

            user table_2 = db.users.Find(id);

            registered_users rguser = new registered_users
            {
                UserID = table_2.UserID,
                Fullname = table_2.Fullname,
                Image = table_2.Image,
                Account_no = table_2.Account_no,
                Adress = table_2.Adress,
                Age = table_2.Age,
                Cast = table_2.Cast,
                City = table_2.City,
                CNIC = table_2.CNIC,
                Contact_no = table_2.Contact_no,
                Email = table_2.Email,
                Gender = table_2.Gender,
                Password = table_2.Password,
                Profession = table_2.Profession,
                Religion = table_2.Religion,
                Salary = table_2.Salary,
                Username = table_2.Username
            };

            db.registered_users.Add(rguser);
            db.SaveChanges();
            return RedirectToAction("requests");

        }
        //*********************************************************************//


        // GET: Broker
        public ActionResult Index()
        {
            return View(db.brokers.ToList());
        }

        // GET: Broker/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            broker broker = db.brokers.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        // GET: Broker/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Broker/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Email,City,CNIC,Contact_No,BrokerID")] broker broker)
        {
            if (ModelState.IsValid)
            {
                db.brokers.Add(broker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(broker);
        }

        // GET: Broker/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            broker broker = db.brokers.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        // POST: Broker/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Email,City,CNIC,Contact_No,BrokerID")] broker broker)
        {
            if (ModelState.IsValid)
            {
                db.Entry(broker).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(broker);
        }

        // GET: Broker/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            broker broker = db.brokers.Find(id);
            if (broker == null)
            {
                return HttpNotFound();
            }
            return View(broker);
        }

        // POST: Broker/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            broker broker = db.brokers.Find(id);
            db.brokers.Remove(broker);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
