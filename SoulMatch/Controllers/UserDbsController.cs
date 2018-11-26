using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SoulMatch.Models;
using System.Linq.Expressions;
using System.IO;

namespace SoulMatch.Controllers
{
    public class UserDbsController : Controller
    {
        private SoulMatchEntities db = new SoulMatchEntities();

        // GET: UserDbs
        public ActionResult Index()
        {
            return View(db.UserDbs.ToList());
        }

        // GET: UserDbs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDb userDb = db.UserDbs.Find(id);
            if (userDb == null)
            {
                return HttpNotFound();
            }
            return View(userDb);
        }

        // GET: UserDbs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserDbs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Fullname,Username,Password,Gender,Age,Cast,Religion,City,Adress,Contact_No,CNIC,Profession,Salary")] UserDb userDb)
        {
            if (ModelState.IsValid)
            {
                db.UserDbs.Add(userDb);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userDb);
        }

        // GET: UserDbs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDb userDb = db.UserDbs.Find(id);
            if (userDb == null)
            {
                return HttpNotFound();
            }
            return View(userDb);
        }

        // POST: UserDbs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Fullname,Username,Password,Gender,Age,Cast,Religion,City,Adress,Contact_No,CNIC,Profession,Salary")] UserDb userDb)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userDb);
        }

        // GET: UserDbs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserDb userDb = db.UserDbs.Find(id);
            if (userDb == null)
            {
                return HttpNotFound();
            }
            return View(userDb);
        }

        // POST: UserDbs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserDb userDb = db.UserDbs.Find(id);
            db.UserDbs.Remove(userDb);
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




        //SARA //

        [HttpGet]
        public ActionResult Register()

        {
            UserDb usermodel = new UserDb();
            return View(usermodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserDb usermodel)
        {
            string filename = Path.GetFileNameWithoutExtension(usermodel.Imagefile.FileName);
            string extension = Path.GetExtension(usermodel.Imagefile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            usermodel.Image = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            usermodel.Imagefile.SaveAs(filename);

            using (SoulMatchEntities dbModel = new SoulMatchEntities())

            {

                if (dbModel.UserDbs.Any(x => x.Username == usermodel.Username))
                {
                    ViewBag.DuplicateMessage = "Username already exists";
                    return View("Register", usermodel);
                }
                dbModel.UserDbs.Add(usermodel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registeration successful";
            return View("Register", new UserDb());
        }
        [HttpGet]
        public ActionResult Login()
        {
            UserDb usermodel = new UserDb();
            return View(usermodel);

        }
        [HttpPost]
        public ActionResult Login(UserDb usermodel)
        {

            using (SoulMatchEntities db = new SoulMatchEntities())
            {

                if (db.UserDbs.Any(x => x.Email == usermodel.Email && x.Password == usermodel.Password))
                {

                    ViewBag.SuccessMessage = "Login Successful";
                    Session["userID"] = usermodel.UserID;
                    return RedirectToAction("UserProfile", "User");
                }
                ViewBag.LoginErrorMessage = "Wrong Email and password";
                return View("Login", usermodel);
            }

        }
        public ActionResult Userprofile()
        {
            UserDb usermodel = new UserDb();
            return View(usermodel);
        }
        [HttpGet]
        public ActionResult Searchuser(string searching)
        {
            var user = from s in db.RegisteredUserDbs
                       select s;
            if (!String.IsNullOrEmpty(searching))
            {

                user = user.Where(s => s.Religion.Contains(searching) || s.Profession.Contains(searching) || s.Cast.Contains(searching) || searching == null);
            }


            return View(user.ToList());

        }

        //SARA //
    }
}
