using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EmsBAL;
using EmsDAL;
using EmsEntities;
using System.Linq.Dynamic;

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

        #region Department ServerSide(searching & sorting)
        [HttpGet]
        public ActionResult ServerSide()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetServerSide()
        {
            #region Server-side processing
            /* (https://datatables.net/manual/server-side) */
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            int sortColumnIndex = Convert.ToInt32(Request["order[0][column]"]);
            string sortColumName = Request["columns[" + sortColumnIndex + "][data]"];
            string sortDirection = Request["order[0][dir]"];
            #endregion

            IEnumerable<DepartmentEntities> departmentRecords = departmentManager.AllDepartments();
            int totalRows = departmentRecords.Count();

            //filter
            if (!string.IsNullOrEmpty(searchValue))
            {
                departmentRecords = departmentRecords.Where(d => d.Name.ToLower().Contains(searchValue.ToLower()) ||
                  d.Code.ToLower().Contains(searchValue.ToLower()) || d.EmployeeNames.ToLower().Contains(searchValue.ToLower())).ToList();
            }
            int afterFilter = departmentRecords.Count();

            //sorting
            //Use DLL dynamic in queue in order to add this [System.Linq.Dynamic] from manage NUGET package to performing SQl operations
            //departmentRecords = departmentRecords.OrderBy(sortColumnIndex + " " + sortDirection).ToList();//Toggle Sorting
            
            departmentRecords = departmentRecords.OrderBy(sortColumName + " " + sortDirection).ToList();//Perform sorting in dataTable

            //paging
            /* skip() skip 1st fewer records and
             * take() we select next few records
             * in 1st page skip 0 rows and take first 10 records and
             * 2nd page skip first 10 records and selected next 10 records
             */
            departmentRecords = departmentRecords.Skip(start).Take(length).ToList();

            // draw, recordsTotal,recordsFiltered [Returned Data]
            return Json(new { data = departmentRecords, draw = Request["draw"], recordsTotal = totalRows, recordsFiltered = afterFilter }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        [HttpGet]
        public ActionResult ServerSideCopy()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetServerSideCopy(string draw, int start = 0, int length = 10)
        {
            var sortColumnIndex = Convert.ToInt32(Request["order[0][column]"]);
            var searchString = Request["columns[" + sortColumnIndex + "][search][value]"];
            var sortDirection = Request["order[0][dir]"];
            var sortField = Request["columns[" + sortColumnIndex + "][name]"];
            if (sortField == string.Empty)
            {
                sortField = this.Request["columns[" + sortColumnIndex + "][data]"];
            }
            int rowsTotal = this.departmentManager.AllDepartments().ToList().Count;
            var departmentResult = this.departmentManager.DepartmentsServer(searchString, sortField, sortDirection, start, length);
            int rowsFilter = departmentResult.Count();

            return this.Json(
                    new
                    {
                        draw = draw,
                        recordsTotal = rowsTotal,
                        recordsFiltered = rowsFilter,
                        data = departmentResult
                    },
                JsonRequestBehavior.AllowGet);
        }

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