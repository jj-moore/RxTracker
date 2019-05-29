using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RxTracker.Data;
using RxTracker.Models;
using RxTracker.ViewModels;
using RxTracker.ViewModels.Drug;

namespace RxTracker.Controllers
{
    public class DrugController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<MyUser> _userManager;

        public DrugController(ApplicationDbContext context, UserManager<MyUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Drug
        public ActionResult Index()
        {
            ListViewModel model = new ListViewModel
            {
                DrugList = _context.Drug
                    .Select(d => new DrugListItem
                    {
                        DrugId = d.DrugId,
                        Name = d.Name,
                        TradeName = d.TradeName
                    })
                    .OrderBy(d => d.Name)
                    .ToList()
            };

            return View(model);
        }

        // GET: Drug/Details/5
        public ActionResult Details(int id)
        {
            Drug drug = _context.Drug.Find(id);
            if (drug == null)
            {
                return NoContent();
            }
            DetailViewModel model = new DetailViewModel
            {
                Drug = drug,
                GenericFor = _context.Drug
                    .Select(d => new SelectHelper
                    {
                        Value = d.DrugId,
                        Text = string.IsNullOrEmpty(d.TradeName) ? d.Name : d.TradeName
                    })
                    .OrderBy(d => d.Text)
                    .ToList()
            };
            return PartialView("_DrugPartial", model);
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
        public ActionResult Edit(Drug drug)
        {
            try
            {
                Drug drugToEdit = _context.Drug.Find(drug.DrugId);
                if (drugToEdit == null)
                {
                    return View();
                }

                drugToEdit.Name = drug.Name;
                drugToEdit.TradeName = drug.TradeName;
                drugToEdit.Manufacturer = drug.Manufacturer;
                drugToEdit.GenericForId = drug.GenericForId;

                MyUser currentUser = _userManager.FindByNameAsync(this.User.Identity.Name).Result;
                drugToEdit.User = currentUser;

                _context.SaveChanges();
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