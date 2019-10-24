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

        #region Delete multiple records using checkbox
        //private EmployeeDepartment db = new EmployeeDepartment();
        //[HttpPost]
        //public ActionResult Index(FormCollection formCollection)
        //{
        //    string[] ids = formCollection["ID"].Split(new char[] { ',' });
        //    foreach (string id in ids)
        //    {
        //        var employee = this.db.Employees.Find(int.Parse(id));
        //        this.db.Employees.Remove(employee);
        //        this.db.SaveChanges();
        //    }
        //    return RedirectToAction("Index");
        //}
        #endregion

        // GET: /Employee/
        [HttpGet]
        public ActionResult Index()
        {
            //ViewBag.Save = true;
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
        public ActionResult Add(EmployeeEntities employeeEntity)//, int id)
        {
            //ViewBag.EmployeeId = id;// Imp to set viewbag for top shared menu
            employeeManager.AddEmployee(employeeEntity);
            //if (employeeManager.AddEmployee(employeeEntity) != null) { ViewBag.Save = true; } //for alert
            return RedirectToAction("Index");
        }

        #region Delete record in database using action method Delete(int id)
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
        #endregion

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
            //var edit = employeeManager.EditEmployee(id, employeeEntity);
            //if (edit != null) { ViewBag.Save = true; } //for alert
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Delete selected checkboxes
        /// </summary>
        private EmployeeDepartment db = new EmployeeDepartment();
        [HttpPost]
        public ActionResult DeleteConfirm(string Ids,FormCollection formCollection)
        {
            //char[] first = Ids.Take(1).ToArray();
            //if (first[0].ToString() == ",")
            //{
            //    Ids = Ids.Remove(0, 1);
            //}
            string[] ids = Ids.Split(new char[] { ',' });
            bool isSuccess = false;
            foreach (string ID in ids)
            {
                //bool isSuccess = false;
                try
                {
                    employeeManager.DeleteAll(int.Parse(ID));
                    isSuccess = true;
                }
                catch (Exception ex)
                {

                }
            }
            return Json(new { success = isSuccess, rows=Ids });
        }

        public bool IsActive(int id, bool forceFullHit = false)
        {
            return this.employeeManager.IsActive(id);
        }
    }
}