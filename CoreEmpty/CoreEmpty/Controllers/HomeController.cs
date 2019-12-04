using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreEmpty.Models;// IStudentRepository
using Microsoft.AspNetCore.Mvc;// :Controller
using Microsoft.Extensions.DependencyInjection;// IServiceCollection

namespace CoreEmpty.Controllers
{
    public class HomeController:Controller
    {
        #region Constructor Injection - use the constructor to inject the service IStudentRepository
        private IStudentRepository _studentRepository;
        /*public HomeController(IStudentRepository studentRepository) // shortcut: ctor + tab twice
        {
            _studentRepository = studentRepository;//run by Startup.cs - services.AddSingleton<IStudentRepository, MockStudentRepository>();
        }*/
        #endregion
        public HomeController() // shortcut: ctor + tab twice [tight coupling]
        {
            _studentRepository = new MockStudentRepository();// creating new instance of class using new keyword [for get student name]
        }
        public string Index()
        {
            //return "Using app.UseMvcWithDefaultRoute() \nhello from home controller index method.";
            return _studentRepository.GetStudent(1).Name;// To get name of student
        }
        /*public JsonResult Index()
        {
            string msg = "JSON";
            return Json(new {msg, id = 1, name = "HOME" });
        }*/
    }
}