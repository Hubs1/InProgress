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
    public class DisplayController : Controller
    {
        // GET: Display
        public ActionResult Index()
        {
            return View();
        }

        // GET: Display/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Display/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Display/Create
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

        // GET: Display/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Display/Edit/5
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

        // GET: Display/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Display/Delete/5
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
