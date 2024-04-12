using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProjectV_MVC.Models
{
    public class mvcUser
    {
        [Required(ErrorMessage = "Username cannot be empty")]
        [DisplayName("Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password cannot be empty")]
        [DisplayName("Password")]
        public string Password { get; set; }
    }
}