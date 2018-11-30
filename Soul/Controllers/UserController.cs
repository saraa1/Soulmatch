using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Linq.Expressions;
using System.Web.Mvc;
using Soul.Models;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Entity.Validation;

namespace Soul.Controllers
{


    public class UserController : Controller
    {
        DbModels db = new DbModels();
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
            usermodel.Image = "/Image/" + filename;
            filename = Path.Combine(Server.MapPath("/Image/"), filename);
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
                    Session["email"] = usermodel.Email.ToString();
                    return RedirectToAction("UserProfile", "User");
                }
                ViewBag.LoginErrorMessage = "Wrong Email and password";
                return View("Login", usermodel);
            }

        }

        public ActionResult Userprofile()

        {
            string displayimg = Session["email"].ToString();
            string CS = "data source=DESKTOP-FA5LU48; database = mydatabase; integrated security=True";
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT Image FROM users WHERE Email='" + displayimg + "'", con);
            con.Open();

            cmd.Parameters.AddWithValue("Email", Session["email"].ToString());
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                string s = sdr["Image"].ToString();
                ViewData["Img"] = s;
            }
            con.Close();



            user usermodel = new user();


            return View(usermodel);
        }




        [HttpGet]
        public ActionResult Searchuser(string searching)
        {
            var user = from s in db.registered_users
                       select s;
            if (!String.IsNullOrEmpty(searching))
            {

                user = user.Where(s => s.Religion.Contains(searching) || s.Profession.Contains(searching) || s.Cast.Contains(searching) || searching == null);
            }

           
            return View(user.ToList());

        }


    }









}


