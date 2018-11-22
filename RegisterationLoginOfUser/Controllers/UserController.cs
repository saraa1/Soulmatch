using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RegisterationLoginOfUser.Models;
using System.IO;

namespace RegisterationLoginOfUser.Controllers
{

    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()

        {
            user usermodel = new user();
            return View(usermodel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(user usermodel)
        {
            string filename = Path.GetFileNameWithoutExtension(usermodel.Imagefile.FileName);
            string extension = Path.GetExtension(usermodel.Imagefile.FileName);
            filename = filename + DateTime.Now.ToString("yymmssfff") + extension;
            usermodel.Image = "~/Image/" + filename;
            filename = Path.Combine(Server.MapPath("~/Image/"), filename);
            usermodel.Imagefile.SaveAs(filename);

            using (DbModels dbModel = new DbModels())

            {

                if (dbModel.users.Any(x => x.Username == usermodel.Username))
                {
                    ViewBag.DuplicateMessage = "Username already exists";
                    return View("Register", usermodel);
                }
                dbModel.users.Add(usermodel);
                dbModel.SaveChanges();
            }
            ModelState.Clear();
            ViewBag.SuccessMessage = "Registeration successful";
            return View("Register", new user());
        }
        [HttpGet]
        public ActionResult Login()
        {
            user usermodel = new user();
            return View(usermodel);

        }
        [HttpPost]
        public ActionResult Login(user usermodel)
        {

            using (DbModels db = new DbModels())
            {

                if (db.users.Any(x => x.Email == usermodel.Email && x.Password == usermodel.Password))
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
            user usermodel = new user();
            return View(usermodel);
        }
    }


    
    



}


