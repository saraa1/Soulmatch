using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Soul.Models;

namespace Soul.Controllers
{
    public class UserController : Controller
    {
        private DbModels db = new DbModels();

        //*****************************************************//

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
            try
            {
                using (DbModels db = new DbModels())
                {

                    if (db.registered_users.Any(x => x.Email == usermodel.Email && x.Password == usermodel.Password))
                    {

                        ViewBag.SuccessMessage = "Login Successful";
                        Session["email"] = usermodel.Email.ToString();
                        return RedirectToAction("UserProfile", "User");

                    }

                    ViewBag.LoginErrorMessage = "Wrong Email and password";
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

            return View("Login", usermodel);
        }



        public ActionResult Userprofile()

        {


            string displayimg = Session["email"].ToString();
            string CS = "Data Source=HP\\SQLEXPRESS; Initial Catalog = mydatabase; Integrated Security=True";
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT Image FROM registered_users WHERE Email='" + displayimg + "'", con);
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

            var user = (from s in db.registered_users
                        select new gridtable
                        {
                            UserID = s.UserID,
                            Fullname = s.Fullname,
                            City = s.City,
                            Image = s.Image,
                            Username = s.Username,
                            Password = s.Password,
                            Age = s.Age,
                            CNIC = s.CNIC,
                            Adress = s.Adress,
                            Contact_no = s.Contact_no,
                            Email = s.Email,
                            Salary = s.Salary,
                            Gender = s.Gender,
                            Religion = s.Religion,
                            Cast = s.Cast,
                            Profession = s.Profession,
                            Account_no = s.Account_no
                        });

            if (!String.IsNullOrEmpty(searching))
            {

                user = user.Where(s => s.Religion.Contains(searching) || s.Profession.Contains(searching) || s.Cast.Contains(searching) || searching == null);
            }



            return View(user.ToList());

        }
        [HttpGet]
        public ActionResult SendRequest(int? id)
        {
            request r = new request();
            string displayimg = Session["email"].ToString();
            string CS = "Data Source=HP\\SQLEXPRESS; Initial Catalog = mydatabase; Integrated Security=True";
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT Username FROM registered_users WHERE Email='" + displayimg + "'", con);
            con.Open();

            cmd.Parameters.AddWithValue("Email", Session["email"].ToString());
            SqlDataReader sdr = cmd.ExecuteReader();


            if (sdr.Read())
            {
                r.sender = sdr["Username"].ToString();


            }



            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            registered_users s = db.registered_users.Find(id);
            if (s == null)
            {
                return HttpNotFound();
            }



            r.receiver = s.Username;
            db.requests.Add(r);
            db.SaveChanges();

            con.Close();

            return View();

        }

        //*****************************************************//

        // GET: User
        public ActionResult Index()
        {
            return View(db.users.ToList());
        }

        // GET: User/Details/5
        public ActionResult Details(int? id)
        {
            registered_users u = db.registered_users.Find(id);
            if (u == null)
            {
                return HttpNotFound();
            }
            string CS = "Data Source=HP\\SQLEXPRESS; Initial Catalog = mydatabase; Integrated Security=True";
            SqlConnection con = new SqlConnection(CS);
            SqlCommand cmd = new SqlCommand("SELECT Image FROM registered_users WHERE Email='" + u.Email + "'", con);
            con.Open();

            //cmd.Parameters.AddWithValue("Email", Session["email"].ToString());
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                string s = sdr["Image"].ToString();
                ViewData["Img"] = s;
            }
            con.Close();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: User/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,Image,Fullname,Username,Password,Age,CNIC,Adress,Contact_no,Email,Salary,Gender,Religion,Cast,Profession,Account_no,City")] user user)
        {
            if (ModelState.IsValid)
            {
                db.users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: User/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,Image,Fullname,Username,Password,Age,CNIC,Adress,Contact_no,Email,Salary,Gender,Religion,Cast,Profession,Account_no,City")] user user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: User/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            user user = db.users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: User/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            user user = db.users.Find(id);
            db.users.Remove(user);
            db.SaveChanges();
            return RedirectToAction("requests","Broker");
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
