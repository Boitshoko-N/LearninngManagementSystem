using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class StudentLogInViewModel
    {
        [Required(ErrorMessage = "Username is required")]
        public int StudentNo { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}