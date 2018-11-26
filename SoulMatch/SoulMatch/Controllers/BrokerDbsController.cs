using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SoulMatch.Models;


namespace SoulMatch.Controllers
{
    public class BrokerDbsController : Controller
    {
        private SoulMatchEntities db = new SoulMatchEntities();

        // GET: BrokerDbs
        public ActionResult Index()
        {
            return View(db.BrokerDbs.ToList());
        }

        // GET: BrokerDbs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrokerDb brokerDb = db.BrokerDbs.Find(id);
            if (brokerDb == null)
            {
                return HttpNotFound();
            }
            return View(brokerDb);
        }

        // GET: BrokerDbs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BrokerDbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Code,Name,Email,City,CNIC,ContactNo,Adress")] BrokerDb brokerDb)
        {
            if (ModelState.IsValid)
            {
                db.BrokerDbs.Add(brokerDb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(brokerDb);
        }

        // GET: BrokerDbs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrokerDb brokerDb = db.BrokerDbs.Find(id);
            if (brokerDb == null)
            {
                return HttpNotFound();
            }
            return View(brokerDb);
        }

        // POST: BrokerDbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Code,Name,Email,City,CNIC,ContactNo,Adress")] BrokerDb brokerDb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(brokerDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(brokerDb);
        }

        // GET: BrokerDbs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BrokerDb brokerDb = db.BrokerDbs.Find(id);
            if (brokerDb == null)
            {
                return HttpNotFound();
            }
            return View(brokerDb);
        }

        // POST: BrokerDbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BrokerDb brokerDb = db.BrokerDbs.Find(id);
            db.BrokerDbs.Remove(brokerDb);
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



        //FAFA //

        // GET: broker
        [HttpGet]
        public ActionResult Login()
        {
            BrokerDb Brokermodel = new BrokerDb();
            return View(Brokermodel);

        }

        [HttpPost]
        public ActionResult Login(BrokerDb brokermodel)
        {

            using (SoulMatchEntities db = new SoulMatchEntities())
            {

                if (db.BrokerDbs.Any(x => x.Id == brokermodel.Id && x.Code == brokermodel.Code))
                {

                    ViewBag.SuccessMessage = "Login Successful";
                    Session["BrokerID"] = brokermodel.Id;
                    return RedirectToAction("pro", "User");
                }
                ViewBag.LoginErrorMessage = "Wrong Email and password";
                return View("Login", brokermodel);
            }

        }
        public ActionResult Userprofile()
        {
            UserDb usermodel = new UserDb();
            return View(usermodel);
        }


        public ActionResult Profile()
        {
            SoulMatchEntities db = new SoulMatchEntities();
            var result = (from c in db.UserDbs
                          select new Gridtables
                          {
                              UserID = c.UserID,
                              Image = c.Image,
                              Username = c.Username,
                              Password = c.Password,
                              Age = c.Age,
                              CNIC = c.CNIC,
                              Address = c.Address,
                              Contact_no = c.Contact_no,
                              Email = c.Email,
                              Salary = c.Salary,
                              Gender = c.Gender,
                              Religion = c.Religion,
                              Caste = c.Caste,
                              Profession = c.Profession,
                              Account_no = c.Account_no

                          }).ToList();
            return View(result);

        }

        //FAFA  //
    }
}
