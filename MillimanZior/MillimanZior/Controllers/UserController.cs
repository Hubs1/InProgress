using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using BAL;//UserManager
using DAL;//ZiorEntities
using Entities;//

namespace MillimanZior.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        UserManager userManager = new UserManager();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Details/5
        public ActionResult GetList()
        {
            return Json(new { data = userManager.GetUsers() }, JsonRequestBehavior.AllowGet);
        }

        // GET: User/Create
        public ActionResult Create()
        {
            UserEntities userEntities = new UserEntities();
            return View(userEntities);
        }

        // POST: User/Create
        [HttpPost]
        public ActionResult Create(UserEntities userEntities)
        {
            try
            {
                // TODO: Add insert logic here
                userManager.AddUser(userEntities);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: User/Edit/5
        public ActionResult Edit(int id)
        {
            UserEntities userEntities = new UserEntities();
            userEntities = userManager.GetUser(id);
            return View(userEntities);
        }

        // POST: User/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, UserEntities userEntities)
        {
            try
            {
                // TODO: Add update logic here
                userManager.EditUser(id, userEntities);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: User/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
        {
            bool isSuccess = false;
            try
            {
                // TODO: Add delete logic here
                userManager.DeleteUser(id);
                isSuccess = true;
            }
            catch
            {
                return View();
            }
            return Json(new { success = isSuccess });
        }

        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginEntities loginEntities)
        {
            using (var userContext = new ZiorEntities()) //made connection with DbContext and check if user exist in database on not
            {
                bool isValid = userContext.AspNetUsers.Any(user => user.Email == loginEntities.Email && user.Password == loginEntities.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(loginEntities.Email, false);
                    return RedirectToAction("Index", "User");
                }

                ModelState.AddModelError("", "Invalid username and password. Try again!!!!!");
            }
            return View();
        }
    }
}
