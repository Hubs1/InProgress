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
            departmentManager.AddDepartment(departmentEntity);
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
            departmentManager.EditDepartment(id, departmentEntity);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool isSuccess = false;
            try
            {
                departmentManager.DeleteDepartment(id);
                isSuccess = true;
            }
            catch (Exception ex)
            {

            }

            return Json(new { success = isSuccess });
        }
    }
}
