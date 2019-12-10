using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEmpty.Models;// IStudentRepository
using CoreEmpty.ViewModels;// HomeDetails
using Microsoft.AspNetCore.Mvc;// :Controller

namespace CoreEmpty.Controllers
{
    public class HomeController:Controller
    {
        #region Constructor Injection - use the constructor to inject the service IStudentRepository
        private IStudentRepository _studentRepository;
        /*public HomeController(IStudentRepository studentRepository) // shortcut: ctor + tab twice [loose coupling]
        {
            _studentRepository = studentRepository;//run by Startup.cs - services.AddSingleton<IStudentRepository, MockStudentRepository>();
        }*/
        public HomeController() // shortcut: ctor + tab twice [tight coupling]
        {
            _studentRepository = new MockStudentRepository();// creating new instance of class using new keyword [for get student name]
        }
        #endregion

        public ViewResult Index()
        {
            var allStudents = _studentRepository.GetStudents();
            return View(allStudents);// allStudent is List type so view also in List type [@model IEnumerable<CoreEmpty.Models.Student>]
            //return Json(allStudents);
            //return "Using app.UseMvcWithDefaultRoute() \nhello from home controller index method.";
            //return _studentRepository.GetStudent(1).Name;// To get name of student [return string type data]
        }
        public ActionResult Details()
        {
            HomeDetails homeDetails = new HomeDetails()
            {
                Student = _studentRepository.GetStudent(1),
                PageTitle = "Student Detail"
            };
            return View(homeDetails);
            //Student student = _studentRepository.GetStudent(1);
            //return View(student);
            //return Json(student);
        }
        /*public JsonResult Index()
        {
            string msg = "JSON";
            return Json(new {msg, id = 1, name = "HOME" });
        }*/
    }
}