using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;// [DisplayName]
using System.ComponentModel.DataAnnotations;

namespace Form_Auth.Models
{
    public class UserFields
    {
        public int Id { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]// use hide input characters in text input
        [DisplayName("Confirm Password")]
        [Compare("Password",ErrorMessage = "Confirm Password not matched. Try again!!!!!")]
        public string ConfirmPassword { get; set; }
    }
}