﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using EmsBAL;
using EmsDAL;
using EmsDAL.Repository;
using EmsEntities;
using static EmsEntities.EmployeeEntities;// for access enum Job

namespace EmsMVC.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeManager employeeManager = new EmployeeManager();
        DepartmentManager departmentManager = new DepartmentManager();

        CountryManager countryManager = new CountryManager();

        //DropDownSelectList selectList=new DropDownSelectList();//EmsMVC\Models\DropDownSelectList.cs

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

        // GET: /Employee/Add
        public ActionResult Add()
        {
            EmployeeEntities employeeEntity = new EmployeeEntities();
            //employeeEntity.DepartmentList = new SelectList(departmentManager.AllDepartments(), "Id", "Name"); //Show all departments in dropdown from table
            employeeEntity.DepartmentList = new SelectList(departmentManager.ActiveDepartments(), "Id", "Name"); //Show active departments only.

            var enumType = from Job j in Enum.GetValues(typeof(Job))
                           select new { id = (int)j, name = j.ToString() };
            employeeEntity.JobList = new SelectList(enumType, "id", "name");

            var enums = from Address a in Enum.GetValues(typeof(Address))
                           select new { id = (int)a, name = a.ToString() };
            employeeEntity.AddressList = new SelectList(enums, "id", "name");

            return View(employeeEntity);
            //ViewBag.DepartmentList = new SelectList(departmentManager.AllDepartments(), "ID", "Name");
            //ViewBag.JobList = new SelectList(enumData, "id", "name");
            //ViewBag.AddressList = new SelectList(enumType, "id", "name");
            //ViewBag.Gender = employeeEntity.Gender.ToString();
        }
        // POST: Employee/Add
        [HttpPost]
        public ActionResult Add(EmployeeEntities employeeEntity)//, int id)
        {
            //ViewBag.EmployeeId = id;// Imp to set viewbag for top shared menu
            try
            {
                employeeManager.AddEmployee(employeeEntity);
                if (employeeEntity != null) { TempData["Success"] = "Employee added successfully"; } // for alert
                return RedirectToAction("Index");
            }
            catch (Exception e) // for error handling database action and show in bootstrap alert
            {
                if (e.Message != null) { TempData["Error"] = e.Message; }
                //if (e.Message != null) { TempData["Error"] = e.InnerException.InnerException.Message; }
                else { TempData["Error"] = "Unknown"; }

                return RedirectToAction("Index");
            }
        }

        public ActionResult Edit(int id)
        {
            EmployeeEntities employeeEntity = new EmployeeEntities();
            employeeEntity = employeeManager.GetEmployee(id);
            //employeeEntity.DepartmentList = new SelectList(departmentManager.AllDepartments(), "ID", "Name");Show all departments in dropdown from table
            employeeEntity.DepartmentList = new SelectList(departmentManager.AllDepartments().Where(d => d.Active == true), "Id", "Name");

            var enumType = from Job j in Enum.GetValues(typeof(Job))
                           select new { id = (int)j, name = j.ToString() };
            employeeEntity.JobList = new SelectList(enumType, "id", "name");

            var enums = from Address a in Enum.GetValues(typeof(Address))
                        select new { id = (int)a, name = a.ToString() };
            employeeEntity.AddressList = new SelectList(enums, "id", "name");
            
            return View(employeeEntity);//Get saved values and show on page

        }
        [HttpPost]
        public ActionResult Edit(int id, EmployeeEntities employeeEntity)
        {
            try
            {
                employeeManager.EditEmployee(id, employeeEntity);
                if (employeeEntity != null) { TempData["Update"] = "Employee updated successfully"; } // for alert
                return RedirectToAction("Index");
            }
            catch (Exception e) // for error handling database action and show in bootstrap alert
            {
                if (e.Message != null) { TempData["Error"] = e.InnerException.InnerException.Message; }
                else{ TempData["Error"] = "Unknown"; }
                    
                return RedirectToAction("Index");
            }
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
                if (ex.Message.Contains("UniqueConstraint")) { TempData["Error"] = ex.Message; }
                else { TempData["Error"] = "Unknown"; }
                return RedirectToAction("Index");
            }
            return Json(new { success = isSuccess });
        }
        #endregion

        /// <summary>
        /// Delete selected records using checkboxes
        /// </summary>
        [HttpPost]
        public ActionResult DeleteConfirm(string Ids)//,FormCollection formCollection)
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
                    employeeManager.DeleteEmployee(int.Parse(ID));
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("UniqueConstraint")) { TempData["Error"] = ex.Message; }
                    else { TempData["Error"] = "Unknown"; }
                    return RedirectToAction("Index");
                }
            }
            return Json(new { success = isSuccess });
        }
        public PartialViewResult PartialResult(int employeeId, int addressTypeId)
            {
            EmployeeEntities employeeEntities = new EmployeeEntities();

            if (employeeId > 0)
            {
                employeeEntities = employeeManager.GetEmployee(employeeId);                
            }
            employeeEntities.AddressType = addressTypeId;
            employeeEntities.CountryList = new SelectList(countryManager.Countries(), "Id", "Name");
            return PartialView("_AddressFields", employeeEntities);
        }
    }
}
