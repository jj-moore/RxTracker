using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RxTracker.Controllers
{
    public class DrugController : Controller
    {
        // GET: Drug
        public ActionResult Index()
        {
            return View();
        }

        // GET: Drug/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Drug/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drug/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Drug/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Drug/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Drug/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Drug/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}