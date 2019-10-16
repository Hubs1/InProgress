using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EmsBAL;
using EmsDAL;
using EmsEntities;
using static EmsEntities.EmployeeEntities;// for access enum Job

namespace EmsMVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeManager employeeManager = new EmployeeManager();
        DepartmentManager departmentManager = new DepartmentManager();

        // GET: /Employee/
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetList()
        {
            return Json(new { data = employeeManager.GetEmployees() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Add()
        {
            EmployeeEntities employeeEntity = new EmployeeEntities();
            ViewBag.DepartmentList = new SelectList(departmentManager.AllDepartments(), "ID", "Name");
            var enumData = from Job j in Enum.GetValues(typeof(Job))
                           select new { id = (int)j, name = j.ToString() };
            ViewBag.JobList = new SelectList(enumData, "id", "name");
            ViewBag.Gender = employeeEntity.Gender.ToString();
            return View();
        }
        // POST: Employee/Add
        [HttpPost]
        public ActionResult Add(EmployeeEntities employeeEntity)
        {
            employeeManager.AddEmployee(employeeEntity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool isSuccess = false;
            try
            {
                employeeManager.DeleteEmployee(id);
                isSuccess = true;
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = isSuccess });
        }

        public ActionResult Edit(int id)
        {
            ViewBag.DepartmentList = new SelectList(departmentManager.AllDepartments(), "ID", "Name");
            var enumData = from Job j in Enum.GetValues(typeof(Job))
                           select new { id = (int)j, name = j.ToString() };
            ViewBag.JobList = new SelectList(enumData, "id", "name");
            return View(employeeManager.GetEmployee(id));//Use for saved values show on page

        }
        [HttpPost]
        public ActionResult Edit(int id, EmployeeEntities employeeEntity)
        {
            employeeManager.EditEmployee(id, employeeEntity);
            return RedirectToAction("Index");
        }
    }
}