using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Form_Auth.Models;//access UserFields
using System.Web.Security;// access form authentication (cookies)

namespace Form_Auth.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login() // ModelClass- UserFields
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
        public ActionResult Signup() // ModelClass- User [User.cs access from .edmx]
        {
            return View();
        }
        [HttpPost]
        public ActionResult Signup(User user)
        {
            using(var userContext = new OfficeContext()) //made connection with DbContext and save in database
            {
                userContext.Users.Add(user);
                userContext.SaveChanges();
            }
            return RedirectToAction("Login");
        }
    }
}