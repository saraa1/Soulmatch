namespace Soul.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class user
    {
        public int UserID { get; set; }
        public string Image { get; set; }
        public HttpPostedFileBase Imagefile { get; set; }
        [DisplayName("Full Name")]
        [StringLength(25)]
        [Required]
        public string Fullname { get; set; }
        [DisplayName("Username ")]
        [Required]
        [StringLength(10)]
        public string Username { get; set; }
        [DisplayName("Password")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "minimum 4 characters required")]
        public string Password { get; set; }
        [DisplayName("Confirm Password")]

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [DataType(DataType.Password)]
        [MinLength(4, ErrorMessage = "minimum 4 characters required")]
        public string ConfirmPassword { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "This field is required")]
        [Range(16, 65)]
        public int? Age { get; set; }
        [DisplayName("CNIC")]


        [Required(AllowEmptyStrings = true, ErrorMessage = "This field is required")]
        public string CNIC { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        public string Adress { get; set; }
        [DisplayName("Contact No")]

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Contact_no { get; set; }
        [DisplayName("Email Address")]

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int? Salary { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Religion { get; set; }
        [Required]
        public string Cast { get; set; }
        [Required]
        public string Profession { get; set; }
        [Required]
        [DisplayName("Account No")]
        public string Account_no { get; set; }
        [Required]
        public string City { get; set; }
    }
}