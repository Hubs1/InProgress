using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;//LoginEntities
using DAL;//ZiorEntities
using System.Web.Security;//FormsAuthentication

namespace MillimanZior.Controllers
{
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private ZiorEntities db = new ZiorEntities(); //update user name and password
        public ActionResult Login() // ModelClass- LoginEntities for creating view
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
                    FormsAuthentication.SetAuthCookie(loginEntities.Email, false); // Persistant cookie (true to create a persistant cookie (one that saved across browser sessions) otherwise false) works on remember checkbox 
                    return RedirectToAction("Index", "User");
                }

                ModelState.AddModelError("", "Invalid username and password. Try again!!!!!");
            }
            return View();
        }
        public ActionResult Signup() // ModelClass- User [AspNetUser.cs access from .edmx] for creating view
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(AspNetUser aspNetUser)
        {
            using (var userContext = new ZiorEntities()) //made connection with DbContext and save in database
            {
                userContext.AspNetUsers.Add(aspNetUser);
                userContext.SaveChanges();
                if (userContext != null) { TempData["Success"] = "Register successfully"; }
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout() // ModelClass- LoginEntities
        {
            FormsAuthentication.SignOut();
            //Session.Clear();
            //Session.RemoveAll();
            //Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login");
        }

        public ActionResult Manage() // ModelClass- LoginEntities for creating view
        {
            return View();
        }
        [HttpPost]
        public ActionResult Manage(LoginEntities loginEntities)
        {
            AspNetUser aspNetUserUpdate = new AspNetUser();
            using (var userContext = new ZiorEntities()) //made connection with DbContext and check if user exist then update data in database
            {
                aspNetUserUpdate.Email = loginEntities.Email;
                aspNetUserUpdate.Password = loginEntities.Password;
                userContext.SaveChanges();
            }
            return View();
        }
    }
}