using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Form_Auth.Models;//access UserFields
using System.Web.Security;// access form authentication (cookies)

namespace Form_Auth.Controllers
{
    [AllowAnonymous]// accessible with login or logout, to override authorise filter add in global.asax
    public class AccountController : Controller
    {
        private OfficeContext db = new OfficeContext(); //update user name and password
        public ActionResult Login() // ModelClass- UserFields for creating view
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserFields userFields)
        {
            using (var userContext = new OfficeContext()) //made connection with DbContext and check if user exist in database on not
            {
                bool isValid = userContext.Users.Any(u => u.UserName == userFields.UserName && u.Password == userFields.Password);
                if (isValid)
                {
                    FormsAuthentication.SetAuthCookie(userFields.UserName, false); // Persistant cookie (true to create a persistant cookie (one that saved across browser sessions) otherwise false) works on remember checkbox 
                    return RedirectToAction("Index", "Office");
                }

                ModelState.AddModelError("", "Invalid username and password. Try again!!!!!");
            }
            return View();
        }
        public ActionResult Signup() // ModelClass- User [User.cs access from .edmx] for creating view
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User user)
        {
            using (var userContext = new OfficeContext()) //made connection with DbContext and save in database
            {
                userContext.Users.Add(user);
                userContext.SaveChanges();
                if (userContext != null) { TempData["Success"] = "Register successfully"; }
            }
            return RedirectToAction("Login");
        }

        public ActionResult Logout() // ModelClass- UserFields
        {
            FormsAuthentication.SignOut();
            //Session.Clear();
            //Session.RemoveAll();
            //Session.Abandon(); // it will clear the session at the end of request
            return RedirectToAction("Login");
        }

        public ActionResult Manage() // ModelClass- UserFields for creating view
        {
            return View();
        }
        [HttpPost]
        public ActionResult Manage(UserFields userFields)
        {
            User userUpdate = new User();
                using (var userContext = new OfficeContext()) //made connection with DbContext and check if user exist then update data in database
                {
                    userUpdate.UserName = userFields.UserName;
                    userUpdate.Password = userFields.Password;
                    userContext.SaveChanges();
                }
            return View();
        }
    }
}