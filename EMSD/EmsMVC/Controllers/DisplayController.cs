using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmsBAL;
using EmsDAL;
using EmsEntities;
using static EmsEntities.EmployeeEntities;// for access enums
using EmsMVC.Controllers;

namespace EmsMVC.Controllers
{
    public class DisplayController : Controller
    {
        DisplayManager displayManager = new DisplayManager();
        
        // GET: Display
        public ActionResult Index()
        {
            return View();
        }
        // GET: Display/EmployeeRecords
        public ActionResult GetEmployees()
        {
            EmployeeEntities employeeEntity = new EmployeeEntities();
            var enumType = from Job j in Enum.GetValues(typeof(Job))
                           select new { id = (int)j, name = j.ToString() };
            employeeEntity.JobList = new SelectList(enumType, "id", "name");
            return Json(new { data = displayManager.AllEmployees() }, JsonRequestBehavior.AllowGet);
        }
        // GET: Display/DepartmentRecords
        public ActionResult GetDepartments()
        {
            return Json(new { data = displayManager.AllDepartments() }, JsonRequestBehavior.AllowGet);
        }        
    }
}
