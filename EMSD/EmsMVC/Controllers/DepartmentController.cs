using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EmsBAL;
using EmsDAL;
using EmsEntities;

namespace EmsMVC.Controllers
{
    public class DepartmentController : Controller
    {
        DepartmentManager departmentManager = new DepartmentManager();
        EmployeeManager employeeManager = new EmployeeManager();
        // GET: Department
        public ActionResult Index()
        {
            return View();
        }

        // GET: Department/Details/5
        public ActionResult Details()
        {
            //List<DepartmentEntities> departmentEntities = new List<DepartmentEntities> { };
            //foreach (Department department in departmentManager.AllDepartments())
            //{
            //    DepartmentEntities departmentEntity = new DepartmentEntities();
            //    departmentEntity.Id = department.Id;
            //    departmentEntity.Name = department.Name;
            //    departmentEntity.Code = department.Code;
            //    departmentEntities.Add(departmentEntity);
            //}
            return Json(new { data = departmentManager.AllDepartments() }, JsonRequestBehavior.AllowGet);
        }

        // GET: Department/Create
        public ActionResult Create()
        {
            DepartmentEntities departmentEntity = new DepartmentEntities();
            return View(departmentEntity);
        }
        
        // POST: Department/Create
        [HttpPost]
        public ActionResult Create(DepartmentEntities departmentEntity)
        {
            try
            {
                departmentManager.AddDepartment(departmentEntity);
                if (departmentEntity != null) { TempData["Success"] = "Department created successfully"; } // for alert
                //return RedirectToAction("Index");
            }
            catch (Exception e) // for error handling database action and show in bootstrap alert
            {
                #region Print error message on Index page in Bootstrap alert(danger)
                string msg = e.InnerException.InnerException.Message;
                //if (e.Message != null) { TempData["Error"] = e.InnerException.InnerException.Message; }
                //else { TempData["Error"] = "Unknown"; }
                #endregion

                //Using ModelState Object to check Server side Validations
                if (msg.Contains("Violation of UNIQUE KEY constraint"))
                {
                    // ViewBag.UserEmailValidation = "User Email already exists. Please choose another Email.";
                    ModelState.AddModelError("Code", "Department Code already exists. Please enter a different code.");
                    return View(departmentEntity);
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Department/Edit/5
        public ActionResult Edit(int id)
        {
            return View(departmentManager.GetDepartment(id));
        }

        // POST: Department/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, DepartmentEntities departmentEntity)

        {
            try
            {
                departmentManager.EditDepartment(id, departmentEntity);
                if (departmentEntity != null) { TempData["Update"] = "DepartmentEntity updated successfully"; } // for alert
            }
            catch (Exception e) // for error handling database action and show in bootstrap alert
            {
                #region Print error message on Index page in Bootstrap alert(danger)
                //if (e.Message != null) { TempData["Error"] = e.InnerException.InnerException.Message; }
                //else { TempData["Error"] = "Unknown"; }
                //return RedirectToAction("Index");
                #endregion

                //Using ModelState Object to check Server side Validations
                string msg = e.InnerException.InnerException.Message;
                if (msg.Contains("Violation of UNIQUE KEY constraint"))
                {
                    // ViewBag.UserEmailValidation = "User Email already exists. Please choose another Email.";
                    ModelState.AddModelError("Code", "Department Code already exists. Please enter a different code.");
                    return View(departmentEntity);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(string Ids)//,FormCollection formCollection)
        {
            string[] ids = Ids.Split(new char[] { ',' });
            bool isSuccess = false;
            foreach (string ID in ids)
            {
                
                try
                {
                    departmentManager.DeleteDepartment(int.Parse(ID));
                    isSuccess = true;
                }
                catch (Exception e)
                {
                    try
                    {
                        if (ID == "")
                        {
                            TempData["Error"] = e.Message;// + "Select more than one row then click on 'DeleteAll' button to perform delete action.";
                            break;
                        }
                        else if (e.Message != null)
                        {
                            String alertDelete = e.InnerException.InnerException.Message;
                            TempData["Error"] = e.InnerException.InnerException.Message.Substring(0, alertDelete.IndexOf('.'));
                            TempData["Error"] = alertDelete.Contains("The DELETE statement conflicted with the REFERENCE constraint ") ?
                                "Department already in use can't be deleted" : "Error occured";
                        }
                        else { TempData["Error"] = "Unknown"; }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != null) { TempData["Error"] = e.Message + ex.Message; }
                        break;
                    }
                }
            }
            return Json(new { success = isSuccess, error = @TempData["Error"] });
        }

        //public ActionResult GetNames()
        //{
        //    return Json(new { data = employeeManager.EmployeeNames() }, JsonRequestBehavior.AllowGet);
        //}

        #region Delete single record in database using action method Delete(int id)
        //public ActionResult Delete(int id)
        //{
        //    bool isSuccess = false;
        //    try
        //    {
        //        departmentManager.DeleteDepartment(id);
        //        isSuccess = true;
        //    }
        //    catch (Exception e)
        //    {
        //        if (e.Message != null) { TempData["Error"] = e.InnerException.InnerException.Message; }
        //        else { TempData["Error"] = "Unknown"; }
        //        return RedirectToAction("Index");
        //    }

        //    return Json(new { success = isSuccess });
        //}
        #endregion
    }
}