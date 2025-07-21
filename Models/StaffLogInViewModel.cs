using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LearninngManagementSystem.Models
{
    public class StaffLogInViewModel
    {
        [Required(ErrorMessage = "Staff number is required")]
        public int StaffNo { get; set; }

        [Required (ErrorMessage ="Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}