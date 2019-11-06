using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace EmsEntities
{
    public class EmployeeEntities
    {
        public EmployeeEntities()
        {
            this.EId = 0;
            Salary = 10000;
        }

        public int EId { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        [Display(Name = "Your Name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select your Department.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public SelectList DepartmentList; // DropDownListFor
        [Required(ErrorMessage ="Enter your Salary")]//Using [Required] for custom message, Default Message - @Html.ValidationMessageFor()
        [Range(10000, 150000, ErrorMessage = "Salary between 10,000 to 1,50,000")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Gender can't be empty")]
        public bool Gender { get; set; }
        public string Sex { get; set; }
        [Required(ErrorMessage = "Choose your JobType.")]
        public int JobType { get; set; } // same name as SQL table column
        public string JobName { get; set; }// display JobName on Index page
        public bool Active { get; set; }
        public bool Status { get; set; }
        public Address category { get; set; }// display on Index page

        public enum Job:int
        {
            [Display(Name ="Full-Time")]
            FullTime,
            [Display(Name = "Part-Time")]
            PartTime,
            [Display(Name = "Fixed")]
            Permanent,
            [Display(Name = "Trainee")]
            Temporary,
        }
        public SelectList JobList;
        public enum Address
        {
            Official,
            Residential
        }
        public SelectList AddressList;

        [Display(Name = "Address")]
        public Nullable<int> AddressType { get; set; }
        public AddressEntity AddressFields { get; set; }
        public SelectList CountryList;
    }
    public class DepartmentEntities
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select your Department.")]
        public string Name { get; set; }

        //public RemoteAttribute(string action, string controller, string areaName);

        [Required(ErrorMessage = "Please enter your Department Code.")]
        //[Remote("CheckCode", "Department", ErrorMessage = "DepartmentCode already exists")] //[using System.Web.Mvc;]
        [RegularExpression("^(DC|)[0-9]{3}$", ErrorMessage = "Enter Code in format: DC000 ")]
        public string Code { get; set; }
        public bool Active { get; set; }
        public string EmployeeNames { get; set; }

        public List<EmployeeEntities> EmployeeList;
    }
    /// <summary>
    /// Adding dropdownList 
    /// </summary>
    public class CountryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class AddressEntity
    {
        [Required]
        public string EmployerName { get; set; }
        public string Street { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        [Required(ErrorMessage = "Please select your Country.")]
        public Nullable<int> CountryId { get; set; }
    }
}
