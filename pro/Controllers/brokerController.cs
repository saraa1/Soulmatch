using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using pro.Models;


namespace pro.Controllers
{
    public class brokerController : Controller
    {
        brokerEntities10 db = new brokerEntities10();
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

            using (brokerEntities10 db = new brokerEntities10())
            {

                if (db.brokers.Any(x => x.Name == brokermodel.Name && x.Code == brokermodel.Code))
                {

                    ViewBag.SuccessMessage = "Login Successful";
                    Session["BrokerID"] = brokermodel.BrokerID;
                    return RedirectToAction("profile", "Broker");
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




        public ActionResult profile()
        {
            return View();
            
        }
        public ActionResult pro()
        {
          
            var result = (from c in db.users
                          select new gridtables
                          {
                              UserID = c.UserID,
                              Image = c.Image,
                              Username= c.Username,
                              Password = c.Password,
                              Age = c.Age,
                              CNIC= c.CNIC,
                              Address= c.Address,
                              Contact_no= c.Contact_no,
                              Email= c.Email,
                              Salary=c.Salary,
                              Gender=c.Gender,
                              Religion=c.Religion,
                              Caste=c.Caste,
                              Profession=c.Profession,
                              Account_no=c.Account_no

                          }).ToList();
            return View(result);

        }

        public ActionResult Details(int? id)
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
        public ActionResult Delete(int? id)
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
       
        [ActionName("Delete")]
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
                return RedirectToAction("pro");
        }


    }
}