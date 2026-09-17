using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace MovieTicketBookingSystem.Models
{
    public class UserModel
    {
        public int User_id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string User_name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Enter a valid email address")]
        public string Email_id { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string User_password { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Phone Number is required")]
        public long Phone_no { get; set; }
    }
}