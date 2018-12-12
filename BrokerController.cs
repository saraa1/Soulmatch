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
        DbModels db = new DbModels();
        // GET: broker
        [HttpGet]
        public ActionResult login()
        {
            broker Brokermodel = new broker();
            return View(Brokermodel);

        }

        [HttpPost]
        public ActionResult Login(broker brokermodel)
        {
            try
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
                
            }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var entityValidationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in entityValidationErrors.ValidationErrors)
                    {
                        Response.Write("Property: " + validationError.PropertyName + " Error: " + validationError.ErrorMessage);
                    }
                }
            }
            return View("Login", brokermodel);

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
        

        public ActionResult DetailsUser(int? id)
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


        //get
        public ActionResult DeleteUser(int? id)
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

        // POST: Table_2/Delete/5
        [HttpPost]

        [ActionName("DeleteUser")]
        public ActionResult DeleteConfirmed(int id)
        {

            try
            {
                user table_2 = db.users.Find(id);

                db.users.Remove(table_2);
                db.SaveChanges();
            }

            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return RedirectToAction("requests");
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
                  UserID=table_2.UserID,
                  Fullname=table_2.Fullname,
                  Image=table_2.Image,
                  Account_no=table_2.Account_no,
                  Adress=table_2.Adress,
                  Age=table_2.Age,
                  Cast=table_2.Cast,
                  City=table_2.City,
                  CNIC=table_2.CNIC,
                  Contact_no=table_2.Contact_no,
                  Email=table_2.Email,
                  Gender=table_2.Gender,
                  Password=table_2.Password,
                  Profession=table_2.Profession,
                  Religion=table_2.Religion,
                  Salary=table_2.Salary,
                  Username=table_2.Username
              };

              db.registered_users.Add(rguser);
            db.users.Remove(table_2);
              db.SaveChanges();
              return RedirectToAction("requests");

      }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CNIC,City,Contact_No,Email,BrokerID,Code")] broker broker)
        {
            if (ModelState.IsValid)
            {
                db.brokers.Add(broker);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(broker);
        }

    }
}
