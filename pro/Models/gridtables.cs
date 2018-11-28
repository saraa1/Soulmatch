using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pro.Models
{
    public class gridtables
    {
        public int UserID { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> CNIC { get; set; }
        public string Address { get; set; }
        public Nullable<int> Contact_no { get; set; }
        public string Email { get; set; }
        public Nullable<int> Salary { get; set; }
        public string Gender { get; set; }
        public string Religion { get; set; }
        public string Caste { get; set; }
        public string Profession { get; set; }
        public Nullable<int> Account_no { get; set; }
    }
}