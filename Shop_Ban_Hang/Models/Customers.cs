using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Shop_Ban_Hang.Models
{
    public class Customers
    {
        [Display(Name = "User")]
        [Required(ErrorMessage = "{0} khong duoc trong.")]
        public string _id { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} khong duoc trong.")]
        public string _password { get; set; }
    }
}