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
//using static EmsEntities.CustomValidation;//RequiredIf
using System.ComponentModel;//Description Attribute

namespace EmsEntities
{
    public class EmployeeEntities
    {
        public EmployeeEntities()
        {
            //this.EId = 0;//not require initialisation EId=0 here. It's already add in [\Employee\_AddEdit.cshtml] - @Html.HiddenFor(m => m.EId)
            Name = "Employee";
            Salary = 10000;
            //Gender = true;
            //Active = true;
            //JobType = 4;
            //AddressType = 1;
            BirthDate = DateTime.Now;//Save current date and time.
            //Set current date in DOB / BirthDate
            DOB = BirthDate.ToString("dd/MMM/yyyy");//Use for get formated date only and display
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

        [Required(ErrorMessage = "Enter your Salary")]//Using [Required] for custom message, Default Message - @Html.ValidationMessageFor()
        [Range(10000, 150000, ErrorMessage = "Salary between 10,000 to 1,50,000")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Gender can't be empty")]
        public byte Gender { get; set; }
        public string Sex { get; set; }

        [Required(ErrorMessage = "Choose your JobType.")]
        [DisplayName("Job")]
        //[RequiredIf("Active", true, ErrorMessage = "Select JobType")]
        public byte JobType { get; set; } // same name as SQL table column
        public string JobName { get; set; }// display JobName on Index page
        [Display(Name = "Active")]
        public bool Active { get; set; }
        [RequiredIf("Active", true, ErrorMessage = "Active text is required")]
        //[Required]
        public string Text { get; set; }
        public bool Status { get; set; }

        public enum Genders
        {
            Male = 1,
            Female
        }

        public enum Job : int//System.ComponentModel
        {
            [Description("Full-Time")]
            FullTime = 1,
            [Description("Part-Time")]
            PartTime,
            [Description("Fixed")]
            Permanent,
            [Description("Trainee")]
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
        [RequiredIf("Active", true, ErrorMessage = "Select Address")]
        public Nullable<int> AddressType { get; set; }
        public AddressEntity AddressFields { get; set; }
        public SelectList CountryList;
        //[DataType(DataType.Date)]//Using Calender - If not use [DataType] then error msg occured- The field BirthDate must be a date.
        public DateTime BirthDate { get; set; }
        [Required(ErrorMessage = "Date of Birth is required.")]
        public String DOB { get; set; }//use for date format only
        public string SubmitOn { get; set; }
        public string EditOn { get; set; } // using [?] for creating nullable "DateTime" variable
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
    public class AddressEntity : EmployeeEntities
    {
        public bool IsAddress { get; set; }
        public int IsAddressType { get; set; }
        public EmployeeEntities GetAddress { get; set; }
        //[RequiredIf("IsAddress", true, ErrorMessage = "Please enter employer name.")]
        [RequiredIf("GetAddress.AddressType", "0", ErrorMessage = "Employer Name is required")]
        //[RequiredIf("IsAddressType", "0", ErrorMessage = "Please enter your Employer Name.")]
        //[Required(ErrorMessage = "Please enter your Employer Name.")]
        public string EmployerName { get; set; }
        public string Street { get; set; }
        public string Landmark { get; set; }
        public string City { get; set; }
        //[RequiredIf("AddressType", 0, ErrorMessage = "Please select your Country.")]
        public Nullable<int> CountryId { get; set; }
    }
    #region Get description of Enums name [Job]
    public static class Enums//Get description of Enum Job
    {
        /// <summary>
        /// Get Description of the enum variables
        /// </summary>
        /// <remarks>Author: Harri</remarks>
        /// <param name="value">enum value</param>
        /// <returns>description of enum passed</returns>
        public static string Description(this Enum value)
        {
            var enumType = value.GetType();
            var field = enumType.GetField(value.ToString());
            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length == 0
                ? value.ToString()
                : ((DescriptionAttribute)attributes[0]).Description;
        }

        public static string TodayDate()
        {
            var today = DateTime.Today;
            return today.ToString();
        }
    }
    #endregion
}
