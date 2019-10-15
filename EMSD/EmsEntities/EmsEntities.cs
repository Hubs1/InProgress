using System;
using System.Configuration;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmsEntities
{
    public class EmployeeEntities
    {
        public int EId { get; set; }

        [Required(ErrorMessage = "Please enter your name.")]
        [Display(Name = "Your Name")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select your Department.")]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }

        public List<DepartmentEntities> DepartmentList; // DropDownListFor
        [Required(ErrorMessage ="Enter your Salary")]//Using [Required] for custom message, Default Message - @Html.ValidationMessageFor()
        [Range(10000, 100000, ErrorMessage = "Salary between 10,000 to 1,00,000")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Gender can't be empty")]
        public bool Gender { get; set; }
        [Required(ErrorMessage = "Choose your JobType.")]
        public int JobType { get; set; } // same name as SQL table column
        public bool Active { get; set; }
        public enum Job:int
        {
            [Display(Name ="Full-Time")]
            FullTime=0,
            PartTime=1,
            Permanent=2,
            Temporary=3
        }
        public List<Job> Jobs;
    }
    public class DepartmentEntities
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select your Department.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter your Department Code.")]
        [RegularExpression("^(DC|)[0-9]{3}$", ErrorMessage = "Enter Code in format: DC000 ")]
        public string Code { get; set; }

        public List<EmployeeEntities> EmployeeList;
    }
}
