using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations; /*Install System.ComponentModel.DataAnnotations by Microsoft from manage NuGet package*/

namespace Entities
{
    public class LoginEntities
    {
        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
    }
    public class RegisterEntities
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
    }
    public class AgencyEntities
    {
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public string BillingCode { get; set; }
        public string API_Key { get; set; }
        public string CompanyURL { get; set; }
        public string Address2 { get; set; }
        public string State { get; set; }
        public int Phone { get; set; }
        public byte[] Logo { get; set; }
        public bool Status { get; set; }
    }

    public class ClientEntities
    {
        public string CompanyName { get; set; }
        public string Agency { get; set; }
        public string CompanyURL { get; set; }
        public string Address { get; set; }
        public string Address2 { get; set; }
        public string City { get; set; }
        public int ZipCode { get; set; }
        public int Phone { get; set; }
        public byte[] Logo { get; set; }
        public bool Status { get; set; }
    }
    public class UserEntities
    {
        public int Id { get; set; }
        [Required]
        [Display(Name ="First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool Status { get; set; }
    }
}
