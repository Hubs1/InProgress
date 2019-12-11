using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MillimanZior.Controllers
{
    public class AgencyController : Controller
    {
        // GET: Agency
        public ActionResult Index()
        {
            return View();
        }

        // GET: Agency/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Agency/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Agency/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Agency/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Agency/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Agency/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Agency/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
